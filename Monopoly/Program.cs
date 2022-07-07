﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Logging;

namespace Monopoly
{
    class Program
    {
        static Logger log;
        static DirectoryInfo di;

        static void Main()
        {
            log = new Logger();
            log.debug = false;

            Analytics a = new Analytics();
            Analytics.instance = a;

            di = new DirectoryInfo(Directory.GetCurrentDirectory());
            string path = $"{di.FullName}\\monopoly_population.txt";

            if (Directory.Exists($"{di.FullName}\\PopStates") == false)
                Directory.CreateDirectory($"{di.FullName}\\PopStates");

            log.Write($"STARTING PROGRAM - {DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}", true);

            RNG.Initialise();

            NEAT.NetworkFactory.Initialise();

            NEAT.Mutation.Initialise();
            NEAT.Crossover.Initialise();
            NEAT.Population.Initialise();

            Tournament tournament = new Tournament();

            //SaveState(path, tournament);

            if (File.Exists(path))
            {
                tournament.Initialise();
                LoadState(path, ref tournament);
            }
            else
            {
                tournament.Initialise();
            }

            for (int i = 0; i < 1000; i++) // default - 1000
            {
                tournament.ExecuteTournament(i);
                NEAT.Population.instance.NewGeneration();
                SaveState(path, tournament);
            }
            
        }

        public static char DELIM_MAIN = ';';
        public static char DELIM_COMMA = ',';

        public static void SaveState(string target, Tournament tournament)
        {
            log.Write($"SAVING POPULATION - {DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}", true);
            PopulationState popState = new PopulationState();

            MONOPOLY.Tiles tiles = new MONOPOLY.Tiles();
            List<MONOPOLY.Tile> tileList = tiles.GetTiles();

            string build = "";
            string build2 = "";

            build += NEAT.Population.instance.GENERATION.ToString();
            build += DELIM_MAIN;
            build += tournament.championScore.ToString();
            build += DELIM_MAIN;

            popState.Generation = NEAT.Population.instance.GENERATION;
            popState.ChampionScore = (int)tournament.championScore;

            int markings = 0;

            //save markings
            for (int i = 0; i < NEAT.Mutation.instance.historical.Count; i++)
            {
                build += NEAT.Mutation.instance.historical[i].order;
                build += DELIM_COMMA;

                build += NEAT.Mutation.instance.historical[i].source;
                build += DELIM_COMMA;

                build += NEAT.Mutation.instance.historical[i].destination;

                if (i != NEAT.Mutation.instance.historical.Count - 1)
                {
                    build += DELIM_COMMA;
                }

                markings++;

                popState.Markings.Add(new Marking {
                    Order = NEAT.Mutation.instance.historical[i].order,
                    Source = NEAT.Mutation.instance.historical[i].source,
                    Destination = NEAT.Mutation.instance.historical[i].destination
                });
            }

            List<string> net_build = new List<string>();
            int net_count = -1;
            int gene_count = 0;

            build += DELIM_MAIN; 

            //save neworks, species by species
            for (int i = 0; i < NEAT.Population.instance.species.Count; i++)
            {
                Network network = new Network();
                network.Members = new List<Member>();

                net_build.Add("");
                net_count++;

                net_build[net_count] += NEAT.Population.instance.species[i].topFitness.ToString();
                net_build[net_count] += DELIM_COMMA;
                net_build[net_count] += NEAT.Population.instance.species[i].staleness.ToString();

                net_build[net_count] += "&";

                int members = NEAT.Population.instance.species[i].members.Count;

                network.TopFitness = (int)NEAT.Population.instance.species[i].topFitness;
                network.Staleness = NEAT.Population.instance.species[i].staleness;

                for (int j = 0; j < members; j++)
                {
                    Member member = new Member();
                    member.Vertices = new List<Vertice>();
                    member.Edges = new List<Edge>();

                    net_build.Add("");
                    net_count++;
                    gene_count++;

                    NEAT.Genotype genes = NEAT.Population.instance.species[i].members[j];

                    int vertices = genes.vertices.Count;

                    for (int k = 0; k < vertices; k++)
                    {
                        net_build[net_count] += genes.vertices[k].index.ToString();
                        net_build[net_count] += DELIM_COMMA;
                        net_build[net_count] += genes.vertices[k].type.ToString();
                        net_build[net_count] += DELIM_COMMA;

                        member.Vertices.Add(new Vertice
                        {
                            Index = genes.vertices[k].index,
                            Type = genes.vertices[k].type.ToString()
                        });
                    }

                    net_build[net_count] += '#';

                    int edges = genes.edges.Count;

                    for (int k = 0; k < edges; k++)
                    {
                        net_build[net_count] += genes.edges[k].source.ToString();
                        net_build[net_count] += DELIM_COMMA;
                        net_build[net_count] += genes.edges[k].destination.ToString();
                        net_build[net_count] += DELIM_COMMA;
                        net_build[net_count] += genes.edges[k].weight.ToString();
                        net_build[net_count] += DELIM_COMMA;
                        net_build[net_count] += genes.edges[k].enabled.ToString();
                        net_build[net_count] += DELIM_COMMA;
                        net_build[net_count] += genes.edges[k].innovation.ToString();
                        net_build[net_count] += DELIM_COMMA;

                        member.Edges.Add(new Edge
                        {
                            Source = genes.edges[k].source,
                            Destination = genes.edges[k].destination,
                            Weight = genes.edges[k].weight,
                            Enabled = genes.edges[k].enabled,
                            Innovation = genes.edges[k].innovation
                        });
                    }

                    if (j != members - 1)
                    {
                        net_build[net_count] += "n";
                    }

                    network.Members.Add(member);
                }

                if (i != NEAT.Population.instance.species.Count - 1)
                {
                    net_build[net_count] += "&";
                }

                popState.Networks.Add(network);

                for (int c = 0; c < 40; c++)
                {
                    popState.Analytics.Add(new Analytic
                        {
                            Ratio = Monopoly.Analytics.instance.ratio[c],
                            Name = tileList.First(t => t.Id == c).Name
                        }
                    );
                }
            }

            build2 += DELIM_MAIN;

            using (StreamWriter sw = new StreamWriter(target))
            {
                sw.Write(build);

                foreach (string b in net_build)
                {
                    sw.Write(b);
                }

                sw.Write(build2);
            }

            log.Write($"{markings} MARKINGS", true);

            string jsonObject = JsonSerializer.Serialize<PopulationState>(popState);
            string fileName = $"{di.FullName}\\PopStates\\PopulationState-{popState.Generation}.json";

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write(jsonObject);
            }
        }

        public static void LoadState(string location, ref Tournament tournament)
        {
            string load = "";

            using (StreamReader sr = new StreamReader(location))
            {
                load = sr.ReadToEnd();
            }

            string[] parts = load.Split(DELIM_MAIN);

            int gen = int.Parse(parts[0]);
            float score = float.Parse(parts[1]);

            NEAT.Population.instance.GENERATION = gen;
            tournament.championScore = score;

            string markingString = parts[2];
            string[] markingParts = markingString.Split(DELIM_COMMA);

            for (int i = 0; i < markingParts.GetLength(0); i += 3)
            {
                int order = int.Parse(markingParts[i]);
                int source = int.Parse(markingParts[i + 1]);
                int destination = int.Parse(markingParts[i + 2]);

                NEAT.Marking recreation = new NEAT.Marking();

                recreation.order = order;
                recreation.source = source;
                recreation.destination = destination;

                NEAT.Mutation.instance.historical.Add(recreation);
            }

            string networkString = parts[3];
            string[] speciesParts = networkString.Split('&');

            for (int x = 0; x < speciesParts.GetLength(0); x+=2)
            {
                string[] firstParts = speciesParts[x].Split(DELIM_COMMA);

                NEAT.Population.instance.species.Add(new NEAT.Species());
                NEAT.Population.instance.species[NEAT.Population.instance.species.Count - 1].topFitness = float.Parse(firstParts[0]);
                NEAT.Population.instance.species[NEAT.Population.instance.species.Count - 1].staleness = int.Parse(firstParts[1]);

                string[] networkParts = speciesParts[x+1].Split('n');

                for (int i = 0; i < networkParts.GetLength(0); i++)
                {
                    NEAT.Genotype genotype = new NEAT.Genotype();

                    string network = networkParts[i];
                    string[] nparts = network.Split('#');

                    string verts = nparts[0];
                    string[] vparts = verts.Split(',');

                    for (int j = 0; j < vparts.GetLength(0) - 1; j += 2)
                    {
                        int index = int.Parse(vparts[j]);
                        NEAT.VertexInfo.EType type = (NEAT.VertexInfo.EType)Enum.Parse(typeof(NEAT.VertexInfo.EType), vparts[j + 1]);

                        genotype.AddVertex(type, index);
                    }

                    string edges = nparts[1];
                    string[] eparts = edges.Split(',');

                    for (int j = 0; j < eparts.GetLength(0) - 1; j += 5)
                    {
                        int source = int.Parse(eparts[j]);
                        int destination = int.Parse(eparts[j + 1]);
                        float weight = float.Parse(eparts[j + 2]);
                        bool enabled = bool.Parse(eparts[j + 3]);
                        int innovation = int.Parse(eparts[j + 4]);

                        genotype.AddEdge(source, destination, weight, enabled, innovation);
                    }

                    NEAT.Population.instance.species[NEAT.Population.instance.species.Count - 1].members.Add(genotype);
                    NEAT.Population.instance.genetics.Add(genotype);
                }
            }

            NEAT.Population.instance.InscribePopulation();
        }
    }
}
