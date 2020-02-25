using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphen
{
    public class Graph
    {
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges;
        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public int NodeCounter = 0;
        public int EdgeCounter = 0;

        public Node Pointer;

        //------------------------------------------------------------------------------------------------------------

        public void AddKnoten(string Item)
        {
            Nodes.Add(new Node(Item));
            NodeCounter++;
        }

        //------------------------------------------------------------------------------------------------------------

        public void AddKante(string Stadtname1, string Stadtname2, int kosten)
        {
            var k1 = Nodes.Find(t => t.Name == Stadtname1);
            var k2 = Nodes.Find(t => t.Name == Stadtname2);

            if (k1 != null && k2 != null && k1 != k2)
            {
                var kante = new Edge { A = k1, Value = kosten, B = k2 };
                k1.Kanten.Add(kante);
                k2.Kanten.Add(kante);
                Edges.Add(kante);
                return;
            }
            else if (k1 != null || k2 != null)
            {
                throw new Exception("Mindestens einer der Knoten existiert nicht");
            }
            else
            {
                throw new Exception("Es darf nicht zweimal der slebe Knoten angegeben werden");
            }
        }

        //------------------------------------------------------------------------------------------------------------

        //public Node<T> Finder(T Item)
        //{
        //    foreach (var node in Nodes)
        //    {

        //    }
        //}

        //------------------------------------------------------------------------------------------------------------

        public bool Exists(string name)
        {
            foreach (var node in Nodes)
            {
                if (node.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        //------------------------------------------------------------------------------------------------------------

        public void DeleteEdge(string name1, string name2)
        {
            foreach (var edge in Edges)
            {
                if (edge.A.Name == name1 && edge.B.Name == name2)
                {
                    Edges.Remove(edge);

                    foreach (var node in Nodes)
                    {
                        if (edge.A.Equals(node.Kanten) || edge.B.Equals(node.Kanten))
                        {
                            node.Kanten.Remove(edge);
                        }
                    }
                    return;
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------
        public void SearchWay2(string startpoint, string endpoint)
        {
            var startNode = Nodes.First(node => node.Name == startpoint);
            var ways = new List<List<Node>> { new List<Node>{ startNode } };
            var newWays = ways;

            bool newWayFound = true;
            while (newWayFound)
            {                
                newWays = newWays.SelectMany(w => GetNewWays(w)).ToList();
                newWayFound = newWays.Any();
                ways.AddRange(newWays);
            }

            var result = ways.Where(w => w.Last().Name == endpoint).ToArray();
        }

        public List<List<Node>> GetNewWays(List<Node> way)
        {
            var ways = new List<List<Node>>();
            var startNode = way.Last();
            var andereKnoten = startNode.Kanten
                .Select(k => k.A)
                .Concat(startNode.Kanten.Select(k => k.B))
                .Distinct()
                .Where(k => !way.Contains(k))
                .ToArray();
            
            foreach (var knoten in andereKnoten)
            {
                var newWay = way.ToList();
                newWay.Add(knoten);
                ways.Add(newWay);
            }

            return ways;
        }

        public (List<List<Node>>, int) SearchWay(string startpoint, string endpoint)
        {
            Stack<Node> stack = new Stack<Node>();
            HashSet<Node> seen = new HashSet<Node>();
            List<List<Node>> Ergebnis = new List<List<Node>>();

            var dauer = 0;

            Node anfangsknoten = null;
            Node aktuellerknoten = null;
            Node zielknoten = null;
            foreach (var node in Nodes)
            {
                if (node.Name == startpoint)
                {
                    aktuellerknoten = node;
                    anfangsknoten = node;
                }
                if (node.Name == endpoint)
                {
                    zielknoten = node;
                }
            }

            stack.Push(aktuellerknoten);
            seen.Add(aktuellerknoten);

            while (stack.Count != 0)
            {
                var tempnachbarn = FindNeighbour(aktuellerknoten.Name)
                    .Where(e => !stack.Contains(e))
                    .Where(e => !seen.Contains(e))
                    .ToList();

                while (tempnachbarn.Count > 0)
                {

                    for (int i = 0; i < tempnachbarn.Count(); i++)
                    {
                        if (!seen.Contains(tempnachbarn.ToArray()[0]))
                        {
                            stack.Push(tempnachbarn.ToArray()[0]);
                            aktuellerknoten = stack.Peek();
                            tempnachbarn = FindNeighbour(aktuellerknoten.Name)
                                .Where(e => !stack.Contains(e))
                                .Where(e => !seen.Contains(e))
                                .ToList();
                            break;
                        }
                        else if (seen.Contains(tempnachbarn.ToArray()[0]))
                        {
                            tempnachbarn.RemoveAt(0);
                        }
                    }

                    //if (aktuellerknoten == zielknoten)
                    //{
                    //    var erg = stack.ToList();
                    //    Ergebnis.Add(erg);
                    //    seen.Add(aktuellerknoten);
                    //    stack.Pop();
                    //    stack.Push(anfangsknoten);
                    //    aktuellerknoten = stack.Peek();
                    //    tempnachbarn = FindNeighbour(aktuellerknoten.Name)
                    //        .Where(e => !stack.Contains(e))
                    //        .Where(e => !seen.Contains(e))
                    //        .ToList();

                        //while (stack.Count > 0)
                        //{
                        //    if (tempnachbarn.Count == 0)
                        //    {
                        //        stack.Pop();
                        //        aktuellerknoten = stack.Peek();
                        //        tempnachbarn = FindNeighbour(aktuellerknoten.Name)
                        //            .Where(e => !stack.Contains(e))
                        //            .Where(e => !seen.Contains(e))
                        //            .ToList();
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //}
                    //}
                }

                if (tempnachbarn.Count == 0)
                {
                    var erg = stack.ToList();
                    Ergebnis.Add(erg);
                    stack.Pop();
                    seen.Add(aktuellerknoten);
                    if (stack.Count == 0)
                        break;
                    aktuellerknoten = stack.Peek();
                    tempnachbarn = FindNeighbour(aktuellerknoten.Name)
                        .Where(e => !stack.Contains(e))
                        .Where(e => !seen.Contains(e))
                        .ToList();
                }
            }

            var result = Ergebnis.Where(w => w.First().Name == endpoint).ToArray();

            return (Ergebnis, dauer);
        }
        //------------------------------------------------------------------------------------------------------------
        public List<Node> FindNeighbour(string startnode)
        {
            List<Node> neighbourlist = new List<Node>();

            Node startknoten = null;
            foreach (var node in Nodes)
            {
                if (node.Name == startnode)
                {
                    startknoten = node;
                }
            }

            if (startknoten != null)
            {
                foreach (var kante in startknoten.Kanten)
                {
                    if (kante.A == startknoten)
                        neighbourlist.Add(kante.B);
                    else
                        neighbourlist.Add(kante.A);
                }
            }
            else
            {
                Console.WriteLine("Der Knoten existiert nicht");
            }
            return neighbourlist;
        }
        //-------------------------------------------------------------------------------------------------------
        public void DeleteKnoten(string DeleteNode, bool RemoveEdges = false)
        {
            if (Exists(DeleteNode))
            {
                foreach (var node in Nodes.ToArray())
                {
                    if (node.Kanten.Count != 0 && node.Name == DeleteNode)
                    {
                        if (RemoveEdges == true)
                        {
                            foreach (var edge in node.Kanten.ToArray())
                            {
                                RemoveEdge(edge);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Der Knoten kann erst gelöscht werden wenn " +
                                "er keine Kanten mehr hat, aktuell hat er " + node.Kanten.Count + " Kanten");
                        }
                    }
                    else if (node.Kanten.Count == 0)
                    {
                        Nodes.Remove(node);
                    }
                }
            }
            else
            {
                Console.WriteLine("Der Knoten existiert nicht");
            }
        }

        //------------------------------------------------------------------------------------------------------------

        private void RemoveEdge(Edge edge)
        {
            edge.A.Kanten.Remove(edge);
            edge.B.Kanten.Remove(edge);
        }

        public void DisplayKnoten()
        {
            var Counter = 0;
            foreach (var node in Nodes)
            {
                if (node.Kanten.Count != 0)
                {
                    Console.WriteLine($"Knotenpunkt " + Counter + " " + node.Name
                        );
                    Counter++;
                }
            }
        }

    }
}
