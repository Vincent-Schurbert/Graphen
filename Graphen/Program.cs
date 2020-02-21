using System;

namespace Graphen
{
    class Program
    {
        static void Main(string[] args)
        {
            var KompletterGraph = new Graph();

            KompletterGraph.AddKnoten("Lichtenrade");
            KompletterGraph.AddKnoten("Schichauweg");
            KompletterGraph.AddKnoten("Buckower Chaussee");
            KompletterGraph.AddKnoten("Marienfelde");
            KompletterGraph.AddKnoten("Attilastraße");
            KompletterGraph.AddKnoten("Priesterweg");
            KompletterGraph.AddKnoten("Südkreuz");
            KompletterGraph.AddKnoten("Südende");
            KompletterGraph.AddKnoten("Lankwitz");
            KompletterGraph.AddKnoten("Schöneberg");
            KompletterGraph.AddKnoten("Blankenfelde");

            KompletterGraph.AddKante("Lichtenrade", "Schichauweg", 2);
            KompletterGraph.AddKante("Schichauweg", "Buckower Chaussee", 1);
            KompletterGraph.AddKante("Buckower Chaussee", "Marienfelde", 3);
            KompletterGraph.AddKante("Marienfelde", "Attilastraße", 5);
            KompletterGraph.AddKante("Attilastraße", "Priesterweg", 4);
            KompletterGraph.AddKante("Priesterweg", "Südkreuz", 3);
            KompletterGraph.AddKante("Südende", "Priesterweg", 6);
            KompletterGraph.AddKante("Südende", "Lankwitz", 2);
            KompletterGraph.AddKante("Südkreuz", "Schöneberg", 1);
            KompletterGraph.AddKante("Lichtenrade", "Blankenfelde", 10);


            KompletterGraph.DisplayKnoten();

            Console.WriteLine("------------------------");

            var test = KompletterGraph.FindNeighbour("Priesterweg");

            Console.WriteLine("------------------------");

            //KompletterGraph.DeleteKnoten("Südende", RemoveEdges: true);

            Console.WriteLine("------------------------");

            KompletterGraph.DisplayKnoten();


           // KompletterGraph.DisplayKante();

            //KompletterGraph.DeleteEdge("Lichtenrade", "Schichauweg");

            //Console.WriteLine("------------------------");

            //KompletterGraph.DeleteKnoten("Südende");

            //Console.WriteLine("------------------------");
            //KompletterGraph.DisplayKnoten();

            //Console.WriteLine("------------------------");


            //KompletterGraph.DeleteEdge("Lichtenrade", "Schichauweg");


            //Console.WriteLine("------------------------");

            //KompletterGraph.DisplayKante();

        }
    }
}
