using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphen
{
    class Program
    {
        static void Main(string[] args)
        {
            var KompletterGraph = new Graph();

            List<Node> historyListe = new List<Node>() { };

            //KompletterGraph.AddKnoten("Friedrichstraße");
            //KompletterGraph.AddKnoten("Hackescher Markt");
            //KompletterGraph.AddKnoten("Alexanderplatz");
            //KompletterGraph.AddKnoten("Brandenburger Tor");
            //KompletterGraph.AddKnoten("Potsdamer Platz");
            //KompletterGraph.AddKnoten("Bundestag");
            //KompletterGraph.AddKnoten("Hauptbahnhof");
            //KompletterGraph.AddKnoten("Bellevue");
            //KompletterGraph.AddKnoten("Oranienburger Tor");
            //KompletterGraph.AddKnoten("Naturkundemuseum");
            //KompletterGraph.AddKnoten("Oranienburgerstraße");
            //KompletterGraph.AddKnoten("Nordbahnhof");
            //KompletterGraph.AddKnoten("Französische Straße");

            //KompletterGraph.AddKante("Hackescher Markt", "Friedrichstraße", 2);
            //KompletterGraph.AddKante("Friedrichstraße", "Französische Straße", 1);
            //KompletterGraph.AddKante("Brandenburger Tor", "Friedrichstraße", 3);
            //KompletterGraph.AddKante("Friedrichstraße", "Hauptbahnhof", 5);
            //KompletterGraph.AddKante("Oranienburger Tor", "Friedrichstraße", 4);
            //KompletterGraph.AddKante("Friedrichstraße", "Oranienburgerstraße", 3);
            //KompletterGraph.AddKante("Alexanderplatz", "Hackescher Markt", 6);
            //KompletterGraph.AddKante("Alexanderplatz", "Nordbahnhof", 2);
            //KompletterGraph.AddKante("Französische Straße", "Alexanderplatz", 1);
            //KompletterGraph.AddKante("Französische Straße", "Potsdamer Platz", 10);
            //KompletterGraph.AddKante("Potsdamer Platz", "Brandenburger Tor", 10);
            //KompletterGraph.AddKante("Brandenburger Tor", "Bundestag", 10);
            //KompletterGraph.AddKante("Hauptbahnhof", "Bundestag", 10);
            //KompletterGraph.AddKante("Hauptbahnhof", "Bellevue", 10);
            //KompletterGraph.AddKante("Oranienburger Tor", "Nordbahnhof", 10);
            //KompletterGraph.AddKante("Nordbahnhof", "Oranienburgerstraße", 10);
            //KompletterGraph.AddKante("Naturkundemuseum", "Oranienburger Tor", 10);

            KompletterGraph.AddKnoten("E");
            KompletterGraph.AddKnoten("B");
            KompletterGraph.AddKnoten("D");
            KompletterGraph.AddKnoten("F");
            KompletterGraph.AddKnoten("H");
            KompletterGraph.AddKnoten("C");
            KompletterGraph.AddKnoten("G");
            KompletterGraph.AddKnoten("A");
            KompletterGraph.AddKnoten("L");
            KompletterGraph.AddKnoten("Z");
            KompletterGraph.AddKnoten("K");
            KompletterGraph.AddKnoten("Q");

            KompletterGraph.AddKante("B", "E", 3);
            KompletterGraph.AddKante("E", "F", 1);
            KompletterGraph.AddKante("D", "E", 5);
            KompletterGraph.AddKante("D", "C", 4);
            KompletterGraph.AddKante("C", "A", 2);
            KompletterGraph.AddKante("D", "G", 1);
            KompletterGraph.AddKante("E", "H", 7);
            KompletterGraph.AddKante("H", "G", 5);
            KompletterGraph.AddKante("H", "K", 4);
            KompletterGraph.AddKante("Q", "K", 2);
            KompletterGraph.AddKante("K", "Z", 1);
            KompletterGraph.AddKante("Z", "G", 3);



            //KompletterGraph.DisplayKnoten();

            //Console.WriteLine("------------------------");

            //var test = KompletterGraph.FindNeighbour("Priesterweg");

            //Console.WriteLine("------------------------");

            //KompletterGraph.DeleteKnoten("Südende", RemoveEdges: true);

            //Console.WriteLine("------------------------");

            //KompletterGraph.DisplayKnoten();

            //KompletterGraph.DeleteEdge("Lichtenrade", "Schichauweg");

            //Console.WriteLine("------------------------");

            //KompletterGraph.DeleteKnoten("Südende");

            //Console.WriteLine("------------------------");
            //KompletterGraph.DisplayKnoten();

            //Console.WriteLine("------------------------");

            //KompletterGraph.DeleteEdge("Lichtenrade", "Schichauweg");
            
            var start = KompletterGraph.Nodes.First(n => n.Name == "E");
            var ziel = KompletterGraph.Nodes.First(n => n.Name == "Z");

            KompletterGraph.FastestWay(start, ziel);

            KompletterGraph.DisplayKnoten();

        }
    }
}
