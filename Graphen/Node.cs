using System;
using System.Collections.Generic;
using System.Text;

namespace Graphen
{
    public class Node
    {
        public string Name;
        public List<Edge> Kanten;

        public Node(string name)
        {
            Name = name;
            Kanten = new List<Edge>();
        }
    }
}
