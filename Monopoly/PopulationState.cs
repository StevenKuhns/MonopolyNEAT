using System;
using System.Collections.Generic;

namespace Monopoly
{
    public class PopulationState
    {
        public int Generation { get; set; }
        public int ChampionScore { get; set; }
        public List<Marking> Markings { get; set; }
        public List<Network> Networks { get; set; }
        public List<Analytic> Analytics { get; set; }

        public PopulationState()
        {
            Markings = new List<Marking>();
            Networks = new List<Network>();
            Analytics = new List<Analytic>();
        }
    }

    public class Marking
    {
        public int Order { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
    }

    public class Network
    {
        public int TopFitness { get; set; }
        public int Staleness { get; set; }
        public List<Member> Members { get; set; }
    }

    public class Analytic
    {
        public float Ratio { get; set; }
        public string Name { get; set; }
    }

    public class Member
    {
        public List<Vertice> Vertices { get; set; }
        public List<Edge> Edges { get; set; }
    }

    public class Vertice
    {
        public int Index { get; set; }
        public string Type { get; set; }
    }

    public class Edge
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public double Weight { get; set; }
        public bool Enabled { get; set; }
        public int Innovation { get; set; }
    }
}