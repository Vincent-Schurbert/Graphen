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

        //List<List<Node>> Solutions = new List<List<Node>>();
        //Stack<Node> Stack = new Stack<Node>();
        //HashSet<Node> Visited = new HashSet<Node>();
        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        //------------------------------------------------------------------------------------------------------------
        public void AddKnoten(string Item)
        {
            Nodes.Add(new Node(Item));
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

        public bool Exists(string name) // Überprüft ob ein Element in der Liste existiert
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
        public List<List<Node>> SearchWay2(string startpoint, string endpoint) // Iterative Lösungen von Jens
        {
            var startNode = Nodes.First(node => node.Name == startpoint);
            var ways = new List<List<Node>> { new List<Node> { startNode } };
            List<Edge> edges = new List<Edge>();
            var newWays = ways;

            bool newWayFound = true;
            while (newWayFound)
            {
                newWays = newWays.SelectMany(w => GetNewWays(w)).ToList();
                newWayFound = newWays.Any();
                ways.AddRange(newWays);
            }

            var result = ways.Where(w => w.Last().Name == endpoint).ToList();

            return result;
        }

        public List<List<Node>> GetNewWays(List<Node> way) // sucht immer die Wege von einer Liste von Knoten 
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

        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Um Rekursion zu verstehen, muss man Rekursion verstehen ;        /// </summary>
        /// <param name="start">Der Startknoten</param>
        /// <param name="ziel">Der Zielknoten</param>
        /// <param name="history">Der bisher gegangene Weg</param>
        /// <returns>Lösungswege</returns>
        public List<List<Node>> SearchWaysRecursive(Node start, Node ziel, List<Node> history)
        {
            Console.WriteLine(new string('-', history.Count) + "Starte " + start.Name);
            var solutions = new List<List<Node>>();
            //Alle Nachbarn holen, die noch nicht in der History sind
            var neighborNodes =
                start.Kanten.Select(e => e.A)
                .Union(start.Kanten.Select(e => e.B))
                .Where(e => !history.Contains(e))
                .ToArray();
            foreach (var neighbor in neighborNodes)
            {
                if (neighbor == ziel)
                {
                    //ist der Nachbar das Ziel, speichern wir das
                    var solution = new List<Node>(history) { neighbor };
                    solutions.Add(solution);
                }
                else
                {
                    //ist es nicht das Ziel, dann erweitern wir die History und gehen ein Level tiefer
                    //die Lösungen aus dieser Ebene adden wir zu den solutions
                    var nextHistory = new List<Node>(history) { neighbor };
                    solutions.AddRange(SearchWaysRecursive(neighbor, ziel, nextHistory));
                }
            }
            Console.WriteLine(new string('-', history.Count) + "Beende " + start.Name);
            return solutions; // gibt nicht nur die Lösungen für Start bis Ziel aus sondern auch Lösungen die über den Start punkt führen 
        }

        //------------------------------------------------------------------------------------------------------------
        public (List<List<Node>>, List<Node>, List<int>) FastestWay(Node start, Node ziel)
        {
            var history = new List<Node>();
            var gefilterteSolutions = new List<List<Node>>();

            var ungefilterteSolutions = SearchWaysRecursive(start, ziel, history);

            foreach (var item in ungefilterteSolutions)  
            {
                //Überprüfen ob das erste Element der Liste mit dem Startpunkt übereinstimmt
                if (item.ToArray()[0] == start)
                    gefilterteSolutions.Add(item);
            }

            var shortestWay = new List<Node>();
            var valueWays = new List<int>();
            var Zähler = 0;
            int sum = 0;

            foreach (var item in gefilterteSolutions)
            {
                foreach (var node in item)
                {
                    var listOfEdges = node.Kanten;

                    for (int i = Zähler; i < item.Count() - 1;)
                    {
                        for (int h = 0; h < listOfEdges.Count(); h++)
                        {
                            var knot = listOfEdges.ToArray()[h];
                            if (knot.A == item.ToArray()[i + 1] || knot.B == item.ToArray()[i + 1])
                            {
                                Zähler++;
                                sum = sum + knot.Value;
                                break;
                            }
                        }
                        break;

                    }
                }

                for (int i = 0; i < valueWays.Count(); i++)
                {
                    if (sum < valueWays.ToArray()[i])
                    {
                        if (i == valueWays.Count() - 1) 
                        {
                            //immer den billigsten Weg in die Liste schreiben und davor die Liste leeren 
                            shortestWay.Clear();
                            foreach (var dings in item)
                            {
                                shortestWay.Add(dings);
                            }
                        }
                    }
                    else
                        break;
                }
                valueWays.Add(sum);
                sum = 0;
                Zähler = 0;
            }

            return (gefilterteSolutions, shortestWay, valueWays);
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
        //------------------------------------------------------------------------------------------------------------
        public void DisplayKnoten()
        {
            var Counter = 0;
            foreach (var node in Nodes)
            {
                if (node.Kanten.Count != 0)
                {
                    Console.WriteLine($"Knotenpunkt " + Counter + " " + node.Name);
                    Counter++;
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------

        public List<List<Node>> StackVersuch(Node start, Node finish)
        {
            Stack<List<Node>> stack = new Stack<List<Node>>();


            var current = FindPossibleWays(start, );

            stack.Push(current);

            while (stack.Count() > 0)
            {

            }

            return null;
        }

        public List<List<Node>> FindPossibleWays(Node current, List<List<Node>> StackContainer)
        {
            foreach (var item in StackContainer)
            {
                var nachbarn = FindNeighbour(current.Name);

                foreach (var value in nachbarn.ToArray())
                {
                    if (current == item.ToArray()[item.Count()])
                    {
                        item.Add(nachbarn.ToArray()[0]);
                        nachbarn.Remove(nachbarn.ToArray()[0]);
                        break;
                    }
                    break;
                }
            }
            return StackContainer;
        }

        public List<List<Node>> StackToList(Stack<Liste<Node>> stack)
        {
            var listeAusStack = new List<List<Node>>();

            foreach (var item in stack.ToArray())
            {
                foreach (var value in item)
                {
                    listeAusStack.Add(value);
                }

            }

            return listeAusStack;
        }



        //public List<List<Node>> GangAlgo(Node start, Node target)
        //{
        //    List<Node> temp = new List<Node>();
        //    Stack.Push(start);
        //    Node current = start;
        //    var startNachbarn = nachBaren(start);
        //    while (Stack.Count > 0)
        //    {
        //        var tempNachbar = nachBaren(current);

        //        while (tempNachbar.Count != 0)
        //        {
        //            current = tempNachbar.Where(n => !temp.Contains(n)).ToArray()[0];

        //            Stack.Push(current);
        //            if (current == target)
        //            { 
        //                if (!listExistiert(Stack.ToList()))
        //                    Solutions.Add(Stack.ToList());
        //                Stack.Pop();
        //                current = Stack.Peek();
        //                while (Stack.Contains(startNachbarn.ToArray()[0]))
        //                {
        //                    if (tempNachbar.Count != tempCount(temp, current)) // statt nur wenn ein nachfolgender nachbar besteht müssen wir den buchstaben so oft ins temp reinschreiben wie er nachbarn hat und darauf überprüfen 
        //                    {
        //                        temp.Add(current);
        //                        if (tempNachbar.Count != tempCount(temp, current) || temp.Contains(startNachbarn.ToArray()[0]))
        //                        {
        //                            break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Stack.Pop();
        //                        current = Stack.Peek();
        //                        tempNachbar = nachBaren(current);
        //                    }
        //                }
        //                break;
        //            }
        //            tempNachbar = nachBaren(current);
        //        }
        //        if (tempNachbar.Count == 0 || temp.Contains(startNachbarn.ToArray()[0]) && tempNachbar.Count == tempCount(temp, current))
        //        {
        //            Stack.Pop();
        //            Visited.Add(current);

        //            if (startNachbarn.Contains(current))
        //            {
        //                startNachbarn.Remove(current);
        //                temp.Clear();
        //            }

        //            current = Stack.Peek();
        //        }
        //    }
        //    return Solutions;
        //}
        //public List<Node> nachBaren(Node aktuell)
        //{
        //    var tempnachbarn = FindNeighbour(aktuell.Name)
        //                      .Where(e => !Stack.Contains(e))
        //                      .Where(e => !Visited.Contains(e))
        //                      .ToList();
        //    return tempnachbarn;
        //}
        //public int tempCount(List<Node> gesamteListe, Node element)
        //{
        //    int Zähler = 0;
        //    foreach (var item in gesamteListe)
        //    {
        //        if (item == element)
        //            Zähler++;
        //    }
        //    return Zähler;
        //}
        //public bool listExistiert(List<Node> zuPrüfen)
        //{
        //    var _zuPrüfen = zuPrüfen.ToArray();
        //    if (Solutions.Count == 0)
        //    {
        //        return false;
        //    }
        //    foreach (var lösung in Solutions)
        //    {
        //        var _lösung = lösung.ToArray();
        //        for (int i = 0; i < zuPrüfen.Count; i++)
        //        {
        //            if (_zuPrüfen[i] == _lösung[i])
        //            {
        //                if (i == zuPrüfen.Count - 1)
        //                    return true;
        //            }
        //            else
        //                break;
        //        }
        //    }
        //    return false;
        //}


    }
}
