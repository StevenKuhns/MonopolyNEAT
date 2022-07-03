using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Logging;

namespace Monopoly
{
    public class Tournament
    {
        Logger log;

        public static int TOURNAMENT_SIZE = 256; // Number of players per tournament: default - 256
        public static int ROUND_SIZE = 2000; // Total number of rounds played (WORKERS * BATCH_SIZE): default - 2000
        
        public static int WORKERS = 2; // Number of threads generated to play games: default - 20
        public static int BATCH_SIZE = 2; // Number of games played per group of players: default -20

        public NEAT.Genotype champion = null;
        public float championScore = 0.0f;

        public List<NEAT.Phenotype> contestants;
        public List<NEAT.Genotype> contestants_g;

        public Tournament()
        {
            contestants = new List<NEAT.Phenotype>();
            contestants_g = new List<NEAT.Genotype>();
        }

        public void Initialise()
        {
            log = new Logger();
            log.debug = false;

            int INPUTS = 126;
            int OUTPUTS = 9;

            NEAT.Population.instance.GenerateBasePopulation(TOURNAMENT_SIZE, INPUTS, OUTPUTS);
        }

        public void ExecuteTournament(int t)
        {
            log.Write($"TOURNAMENT # {NEAT.Population.instance.GENERATION}", true);

            int tournamentId = t;
            int generationId = NEAT.Population.instance.GENERATION;

            contestants.Clear();
            contestants_g.Clear();

            for (int i = 0; i < TOURNAMENT_SIZE; i++)
            {
                NEAT.Population.instance.genetics[i].bracket = 0;
                NEAT.Population.instance.population[i].score = 0.0f;
                
                contestants.Add(NEAT.Population.instance.population[i]);
                contestants_g.Add(NEAT.Population.instance.genetics[i]);
            }

            while (contestants.Count > 1)
            {
                ExecuteTournamentRound(tournamentId, generationId);
            }

            for (int i = 0; i < TOURNAMENT_SIZE; i++)
            {
                float top = 0.0f;

                if (champion != null)
                {
                    top = champion.bracket;
                }

                float diff = NEAT.Population.instance.genetics[i].bracket - top;
                NEAT.Population.instance.genetics[i].fitness = championScore + diff * 5;
            }

            champion = contestants_g[0];
            championScore = contestants_g[0].fitness;
        }

        public void ExecuteTournamentRound(int tournament, int generation)
        {
            log.Write($"ROUND SIZE {contestants.Count}", true);

            List<NEAT.Phenotype> cs = new List<NEAT.Phenotype>();
            List<NEAT.Genotype> cs_g = new List<NEAT.Genotype>();

            RNG.instance.DoubleShuffle(contestants, contestants_g, ref cs, ref cs_g);

            for (int i = 0; i < TOURNAMENT_SIZE; i++)
            {
                NEAT.Population.instance.population[i].score = 0.0f;
            }

            contestants = cs;
            contestants_g = cs_g;

            for (int i = 0; i < contestants.Count; i += 4)
            {
                int played = 0;

                log.Write($"BRACKET ({( i / 4 )})", true);

                while (played < ROUND_SIZE)
                {
                    log.Write($"Initialised Workers", true);

                    Thread[] workers = new Thread[WORKERS];

                    for (int t = 0; t < WORKERS; t++)
                    {
                        ThreadLogger tl = new ThreadLogger
                        {
                            Tournament = tournament,
                            Generation = generation,
                            Contestant = i,
                            Group = played,
                            Thread = t
                        };

                        workers[t] = new Thread(() => PlayGameThread(this, i, tl));
                        workers[t].Start();
                    }

                    for (int t = 0; t < WORKERS; t++)
                    {
                        workers[t].Join();
                    }

                    played += WORKERS * BATCH_SIZE;

                    for (int c = 0; c < 40; c++)
                    {
                        log.Write($"index: {c}, {Monopoly.Analytics.instance.ratio[c]}", true);
                    }
                }

                int mi = 0;
                float ms = contestants[i].score;

                for (int j = 1; j < 4; j++)
                {
                    if (ms < contestants[i + j].score)
                    {
                        mi = j;
                        ms = contestants[i + j].score;
                    }
                }

                for (int j = 0; j < 4; j++)
                {
                    if (j == mi)
                    {
                        contestants_g[i + j].bracket++;
                        continue;
                    }

                    contestants[i + j] = null;
                }
            }

            for (int i = 0; i < contestants.Count; i++)
            {
                if (contestants[i] == null)
                {
                    contestants.RemoveAt(i);
                    contestants_g.RemoveAt(i);
                    i--;
                }
            }

            return;
        }

        public static void PlayGameThread(Tournament instance, int i, ThreadLogger tl)
        {
            Logger threadLog = new Logger(tl);
            threadLog.debug = true;

            for (int game = 0; game < BATCH_SIZE; game++)
            {
                threadLog.Write($"Starting thread for game {game}", false);
                NetworkAdapter adapter = new NetworkAdapter();
                MONOPOLY.Board board = new MONOPOLY.Board(adapter, threadLog);

                board.players[0].network = instance.contestants[i];
                board.players[1].network = instance.contestants[i + 1];
                board.players[2].network = instance.contestants[i + 2];
                board.players[3].network = instance.contestants[i + 3];

                board.players[0].adapter = adapter;
                board.players[1].adapter = adapter;
                board.players[2].adapter = adapter;
                board.players[3].adapter = adapter;

                board.players = RNG.instance.Shuffle(board.players);

                MONOPOLY.Board.EOutcome outcome = MONOPOLY.Board.EOutcome.ONGOING;

                while (outcome == MONOPOLY.Board.EOutcome.ONGOING)
                {
                    outcome = board.Step();
                }

                if (outcome == MONOPOLY.Board.EOutcome.WIN1)
                {
                    lock (board.players[0].network)
                    {
                        board.players[0].network.score += 1.0f;
                    }

                    for (int b = 0; b < board.players[0].items.Count; b++)
                    {
                        lock (Monopoly.Analytics.instance.wins)
                        {
                            Monopoly.Analytics.instance.MarkWin(board.players[0].items[b]);
                        }
                    }
                   
                }
                else if (outcome == MONOPOLY.Board.EOutcome.WIN2)
                {
                    lock (board.players[1].network)
                    {
                        board.players[1].network.score += 1.0f;
                    }

                    for (int b = 0; b < board.players[1].items.Count; b++)
                    {
                        lock (Monopoly.Analytics.instance.wins)
                        {
                            Monopoly.Analytics.instance.MarkWin(board.players[1].items[b]);
                        }
                    }
                }
                else if (outcome == MONOPOLY.Board.EOutcome.WIN3)
                {
                    lock (board.players[2].network)
                    {
                        board.players[2].network.score += 1.0f;
                    }

                    for (int b = 0; b < board.players[2].items.Count; b++)
                    {
                        lock (Monopoly.Analytics.instance.wins)
                        {
                            Monopoly.Analytics.instance.MarkWin(board.players[2].items[b]);
                        }
                    }
                }
                else if (outcome == MONOPOLY.Board.EOutcome.WIN4)
                {
                    lock (board.players[3].network)
                    {
                        board.players[3].network.score += 1.0f;
                    }

                    for (int b = 0; b < board.players[3].items.Count; b++)
                    {
                        lock (Monopoly.Analytics.instance.wins)
                        {
                            Monopoly.Analytics.instance.MarkWin(board.players[3].items[b]);
                        }
                    }
                }
                else if (outcome == MONOPOLY.Board.EOutcome.DRAW)
                {
                    lock (board.players)
                    {
                        board.players[0].network.score += 0.25f;
                        board.players[1].network.score += 0.25f;
                        board.players[2].network.score += 0.25f;
                        board.players[3].network.score += 0.25f;
                    }
                }
            }
        }
    }
}
