using System.Linq;
using System;
using UnityEngine;

namespace Algoritms
{

    public class WeightedGraph
    {
        public const float INFINITY = float.MaxValue;
        private bool IsDirectedGraph = false;
        int numVerts;
        static int MaxVerts = 200;
        Vertex[] verts;
        float[,] weightMatrix;

        #region Properties
        public Vertex[] Vertexts => verts;
        public int NumVerts => numVerts;

        public float[,] WeightMatrix => weightMatrix;
        #endregion
        public WeightedGraph() : this(MaxVerts, false)
        {
        }
        public WeightedGraph(int maxVerts, bool isDirected)
        {
            this.numVerts = maxVerts;
            weightMatrix = new float[maxVerts, maxVerts];
            verts = new Vertex[maxVerts];
            for (int i = 0; i < maxVerts; i++)
                for (int j = 0; j < maxVerts; j++)
                    weightMatrix[i, j] = INFINITY;
            numVerts = 0;
            this.IsDirectedGraph = isDirected;
        }


        public void nuevoVertex(string nom)
        {
            bool esta = numVertex(nom) >= 0;
            if (!esta)
            {
                Vertex v = new Vertex(nom);
                v.asigVert(numVerts);
                verts[numVerts++] = v;
            }
        }

        public int numVertex(string vs)
        {
            Vertex v = new Vertex(vs);
            bool encontrado = false;
            int i = 0;
            for (; (i < numVerts) && !encontrado;)
            {
                encontrado = verts[i].equals(v);
                if (!encontrado)
                    i++;
            }
            // Llego al final sin encontrarlo o si lo encontro antes
            return (i < numVerts) ? i : -1;
        }

        public void nuevoArco(string a, string b, float w)
        {

            int va, vb;
            va = numVertex(a);
            vb = numVertex(b);
            if (va < 0 || vb < 0)
                throw new Exception("Vertex no existe");
            if (IsDirectedGraph)
            {
                weightMatrix[va, vb] = w;
            }
            else
            {
                weightMatrix[va, vb] = w;
                weightMatrix[vb, va] = w;
            }
        }

        public void nuevoArco(int va, int vb, int w)
        {
            if (va < 0 || vb < 0)
                throw new Exception("Vertex no existe");
            if (IsDirectedGraph)
            {
                weightMatrix[va, vb] = w;
            }
            else
            {
                weightMatrix[va, vb] = w;
                weightMatrix[vb, va] = w;
            }
        }

        public bool adyacente(string a, string b)
        {
            int va, vb;
            va = numVertex(a);
            vb = numVertex(b);
            if (va < 0 || vb < 0)
                throw new Exception("Vertex no existe");
            return weightMatrix[va, vb] != INFINITY;
        }

        public bool adyacente(int va, int vb)
        {
            if (va < 0 || vb < 0)
                throw new Exception("Vertex no existe");
            return weightMatrix[va, vb] != INFINITY;
        }

        public int numeroDeVertexs()
        {
            return numVerts;
        }
        public override string ToString()
        {
            string msg = "WeightedGraph\n";
            msg += string.Join(", ", this.verts.Select(x => x.ToString()).ToArray());
            msg += "\nWeights:\n" + string.Join(", ", this.weightMatrix.Cast<float>());
            return msg;
        }
    }
}