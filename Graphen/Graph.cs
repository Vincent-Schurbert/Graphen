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

        public void AddKnoten(string Item)
        {
            Nodes.Add(new Node(Item));
            NodeCounter++;
        }

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

        //public Node<T> Finder(T Item)
        //{
        //    foreach (var node in Nodes)
        //    {

        //    }
        //}

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

        //public (List<Knoten<T>>, int) SearchWay(T StartPoint, T EndPoint)
        //{
        //    var Dauer = 0;
        //    List<Knoten<T>> UsedNodes = new List<Knoten<T>>();
        //    List<Knoten<T>> FastestPoints = new List<Knoten<T>>();

        //    while (StartPoint.Equals(Nodes))


        //    return (UsedNodes, Dauer);
        //}

        public List<Node> FindNeighbour(string StartNode)
        {
            List<Node> NeighbourList = new List<Node>();

            Node startknoten = null;
            foreach (var node in Nodes)
            {
                if (node.Name == StartNode)
                {
                    startknoten = node;
                }
            }

            if (startknoten != null)
            {
                foreach (var kante in startknoten.Kanten)
                {
                    if (kante.A == startknoten)
                        NeighbourList.Add(kante.B);
                    else
                        NeighbourList.Add(kante.A);
                }                
            }
            else
            {
                Console.WriteLine("Der Knoten existiert nicht");
            }
            return NeighbourList;
        }

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

        public void DisplayKante()
        {
            foreach (var edge in Edges)
            {
                Console.WriteLine($"Kante zwischen " + edge.A.Kanten + edge.B.Kanten + edge.Value);
            }
        }
    }
}
