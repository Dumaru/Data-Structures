using System.Collections.Generic;
using System;

namespace Algoritms
{
    /// <summary>
    /// Code from https://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c/listing1.aspx
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> data;
        #region Main Methods

        public PriorityQueue()
        {
            this.data = new List<T>();
        }
        public void Enqueue(T item)
        {
            data.Add(item);
            int ci = data.Count - 1;
            while (ci > 0)
            {
                int pi = (ci - 1) / 2;
                if (data[ci].CompareTo(data[pi]) >= 0)
                    break;
                T tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
                ci = pi;
            }
        }
        public T Dequeue()
        {
            // Assumes pq isn't empty
            int li = data.Count - 1;
            T frontItem = data[0];
            data[0] = data[li];
            data.RemoveAt(li);

            --li;
            int pi = 0;
            while (true)
            {
                // Chooses the child with greater priority 
                int ci = pi * 2 + 1;
                if (ci > li) break;
                int rc = ci + 1;
                if (rc <= li && data[rc].CompareTo(data[ci]) < 0)
                    ci = rc;
                // Swaps if the current index has greater priority
                if (data[pi].CompareTo(data[ci]) <= 0) break;
                T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp;
                pi = ci;
            }
            return frontItem;
        }

        public bool IsConsistent()
        {
            if (data.Count == 0) return true;
            int li = data.Count - 1; // last index
            for (int pi = 0; pi < data.Count; ++pi) // each parent index
            {
                int lci = 2 * pi + 1; // left child index
                int rci = 2 * pi + 2; // right child index
                if (lci <= li && data[pi].CompareTo(data[lci]) > 0) return false;
                if (rci <= li && data[pi].CompareTo(data[rci]) > 0) return false;
            }
            return true; // Passed all checks
        }
        #endregion

        #region Helper Methods

        public int Count()
        {
            return data.Count;
        }
        public bool IsEmpty()
        {
            return data.Count == 0;
        }

        public T Peek()
        {
            T frontItem = data[0];
            return frontItem;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < data.Count; ++i)
                s += data[i].ToString() + " ";
            s += "count = " + data.Count;
            return s;
        }
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Begin Priority Queue demo");
            Console.WriteLine("Creating priority queue of Employee items");
            PriorityQueue<Vertex> pq = new PriorityQueue<Vertex>();
            Vertex a = new Vertex("A");
            Vertex b = new Vertex("B");
            Vertex c = new Vertex("C");
            pq.Enqueue(a);
            pq.Enqueue(b);
            pq.Enqueue(c);

            Console.WriteLine("Priority Queue " + pq.ToString());
            // Demo code here 

            Console.WriteLine("End Priority Queue demo");
            Console.ReadLine();
        }
    }
}