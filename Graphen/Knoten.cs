using System;
using System.Text;

namespace Graphen
{
    public class Knoten<T>
    {
        public T Value;

        public Knoten(Knoten<T> left, T value, Knoten<T> right)
        {
            Value = value;
            Right = right;
            Left = left;
        }

        public Knoten<T> Right
        {
            get; set;
        }

        public Knoten<T> Left
        {
            get; set;
        }
    }
}
