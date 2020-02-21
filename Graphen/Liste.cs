using System;
using System.Text;

namespace Graphen
{
    public class Liste<T>
    {
        Knoten<T> First;
        Knoten<T> Last;


        public int Count()
        {
            int Zähler = 0;
            var Erster = First;

            while (Erster != null)
            {
                Erster = Erster.Right;
                Zähler++;
            }
            return Zähler;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public void Add(T NewNode)
        {
            if (First == null)
            {
                var add = new Knoten<T>(null, NewNode, null);
                First = add;
                Last = add;
                return;
            }
            else
            {
                var add = new Knoten<T>(Last, NewNode, null);
                Last.Right = add;
                Last = add;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public void AddRange(Liste<T> AListe)
        {
            var Help = AListe.First;

            while (Help != null)
            {
                Add(Help.Value);
                Help = Help.Right;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public void Clear()
        {
            First = Last = null;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public bool Contains(T Wert)
        {

            bool IstEnthalten = false;
            var Start = First;

            while (Start.Right != null)
            {
                var ListenWert = Start.Value;

                if (ListenWert.Equals(Wert))
                {
                    IstEnthalten = true;
                    break;
                }
                Start = Start.Right;
            }

            return IstEnthalten;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public bool Exists(Predicate<T> predicate)
        {
            var Start = First;

            while (Start != null)
            {
                if (predicate(Start.Value))
                    return true;
                Start = Start.Right;
            }
            return false;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public T Find(Predicate<T> predicate)
        {
            var Start = First;
            while (Start != null)
            {
                if (predicate(Start.Value))
                    return Start.Value;
                Start = Start.Right;
            }
            return default;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public Liste<T> FindAll(Predicate<T> predicate)
        {
            var Start = First;
            Liste<T> TrefferListe = new Liste<T>();

            while (Start != null)
            {
                if (predicate(Start.Value))
                {
                    TrefferListe.Add(Start.Value);
                    Start = Start.Right;
                }
                else
                    Start = Start.Right;
            }
            TrefferListe.Display();
            return TrefferListe;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public int FindIndex(Predicate<T> predicate)
        {
            var Index = 0;
            var Start = First;

            while (Start != null)
            {
                if (predicate(Start.Value))
                    return Index;
                else
                {
                    Start = Start.Right;
                    Index++;
                }
            }
            return -1;
        }

        public int FindIndex(Predicate<T> predicate, int Index)
        {
            var Start = First;
            var Zähler = 0;

            while (Start != null)
            {
                if (Zähler == Index)
                {
                    while (Start != null)
                    {
                        if (predicate(Start.Value))
                            return Index;
                        else
                        {
                            Start = Start.Right;
                            Index++;
                        }
                    }
                }
                else
                {
                    Start = Start.Right;
                    Zähler++;
                }
            }
            return -1;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public T FindLast(Predicate<T> predicate)
        {
            var Ende = Last;

            while (Ende != null)
            {
                if (predicate(Ende.Value))
                    return Ende.Value;
                Ende = Ende.Left;
            }

            return default;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public int FindLastIndex(Predicate<T> predicate)
        {
            var Ende = Last;
            int Index = Count() - 1;

            while (Ende != null)
            {
                if (predicate(Ende.Value))
                    return Index;
                else
                    Index--;
                Ende = Ende.Left;
            }
            return -1;
        }

        public int FindLastIndex(Predicate<T> predicate, int Index)
        {
            var Zähler = Count() - 1;
            var Ende = Last;

            while (Zähler >= Index && Ende != null)
            {
                if (predicate(Ende.Value))
                {
                    return Zähler;
                }
                else
                {
                    Zähler--;
                    Ende = Ende.Left;
                }
            }
            return -1;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void ForEach(Action<T> action)
        {
            var Start = First;

            while (Start != null)
            {
                action(Start.Value);
                Start = Start.Right;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public int IndexOf(T Wert)
        {
            int Index = 0;
            var Start = First;

            while (Start != null)
            {
                if (Start.Value.Equals(Wert))
                    return Index;
                else
                {
                    Start = Start.Right;
                    Index++;
                }
            }
            return -1;
        }


        public int IndexOf(T Wert, int Index)
        {
            var Start = First;
            var hilfe = 0;

            while (hilfe < Index)
            {
                Start = Start.Right;
                hilfe++;
            }

            while (Index <= Count() - 1)
            {
                if (Start.Value.Equals(Wert))
                    return Index;
                else
                {
                    Start = Start.Right;
                    Index++;
                }
            }
            return -1;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public void Insert(int Index, T Item)
        {
            var Start = First;

            for (int i = 0; i < Index; i++)
            {
                Start = Start.Right;
            }

            if (Start == First)
            {
                var neuAdd = new Knoten<T>(null, Item, Start);

                Start.Left = neuAdd;
                First = neuAdd;
            }
            else if (Start == Last)
            {
                var neuAdd = new Knoten<T>(Start, Item, null);

                Start.Right = neuAdd;
                Last = neuAdd;
            }
            else
            {
                var neuAdd = new Knoten<T>(Start.Left, Item, Start);

                Start.Left.Right = neuAdd;
                Start.Left = neuAdd;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public int LastIndexOf(T Item)
        {
            int Index = Count() - 1;
            var Start = Last;

            while (Start != null)
            {
                if (Start.Value.Equals(Item))
                {
                    return Index;
                }
                else
                {
                    Start = Start.Left;
                    Index--;
                }
            }
            return -1;
        }

        public int LastIndexOf(T Item, int Begrenzer)
        {
            int Index = Count() - 1;
            var Start = Last;

            while (Start != null && Index >= Begrenzer)
            {
                if (Start.Value.Equals(Item))
                {
                    return Index;
                }
                else
                {
                    Start = Start.Left;
                    Index--;
                }
            }
            return -1;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public T[] ToArray()
        {
            T[] Ergebnis = new T[Count()];
            var Start = First;

            for (int i = 0; i < Count(); i++)
            {
                Ergebnis[i] = Start.Value;
                Start = Start.Right;
            }
            return Ergebnis;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public void Remove(T Item)
        {
            var Start = First;

            if (Start.Value.Equals(Item))
            {
                First = First.Right;
                return;
            }

            while (Start != null)
            {
                if (Start.Value.Equals(Item))
                {
                    Start.Left.Right = Start.Right;
                    if (Start.Right != null)
                    {
                        Start.Right.Left = Start.Left;
                    }
                    break;
                }
                else
                {
                    Start = Start.Right;
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public int RemoveAll(Predicate<T> predicate)
        {
            var Start = First;
            int Counter = 0;

            while (Start != null)
            {
                if (predicate(Start.Value))
                {
                    Start.Left.Right = Start.Right;
                    if (Start.Right != null)
                    {
                        Start.Right.Left = Start.Left;
                    }
                    Start = Start.Right;
                    Counter++;
                }
                else
                {
                    Start = Start.Right;
                }
            }
            return Counter;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public void RemoveAt(int Index)
        {
            var Counter = 0;
            var Start = First;

            if (Index == 0) // Für den Fall das Index 0 gelöscht werden soll
            {
                First = First.Right;
                return;
            }
            else if (Index == Count() - 1)
            {
                Last = Last.Left;
                Last.Right = null;
            }
            else if (Index < Count() - 1)
            {
                while (Counter <= Index)
                {
                    if (Counter == Index)
                    {
                        Start.Left.Right = Start.Right;
                        Start.Right.Left = Start.Left;
                    }
                    else
                        Start = Start.Right;

                    Counter++;
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public void Reverse()
        {
            var Start = First;

            while (Start != null)
            {
                var Vorgänger = Start.Left;
                var Nachfolger = Start.Right;

                Start.Left = Nachfolger;
                Start.Right = Vorgänger;

                Start = Start.Left;
            }
            var Zwischenspeicher = First;
            First = Last;
            Last = Zwischenspeicher;
        }

        public void Reverse2()
        {
            var Start = First;
            var Ende = Last;

            for (int i = 0; i < Count() / 2; i++)
            {
                var Zwischenspeicher = Start.Value;
                Start.Value = Ende.Value;
                Ende.Value = Zwischenspeicher;

                Start = Start.Right;
                Ende = Ende.Left;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public void Sort(Comparison<T> comparison)
        {
            for (int i = 0; i < Count(); i++)
            {
                var Knoten = First;

                while (Knoten.Right != null)
                {
                    if (comparison(Knoten.Value, Knoten.Right.Value) == -1)
                    {
                        var Zwischenspeicher = Knoten.Value;
                        Knoten.Value = Knoten.Right.Value;
                        Knoten.Right.Value = Zwischenspeicher;
                    }
                    Knoten = Knoten.Right;
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void Display()
        {
            Knoten<T> Start = First;

            if (Start == null)
            {
                Console.WriteLine("Die Liste ist aktuell Leer");
            }

            while (Start != null)
            {
                Console.WriteLine(Start.Value);
                Start = Start.Right;
            }
        }
    }
}
