
using System;

namespace Algoritms
{
    public class Vertex : IComparable<Vertex>
    {
        string vertexName;
        int numVertex;
        float distance;

        #region Properties
        public String VertextName => vertexName;
        public int NumVertex => numVertex;
        public float Distance => distance;
        #endregion
        public Vertex(string name)
        {
            this.vertexName = name;
            this.numVertex = -1;
        }

        public bool equals(Vertex n)
        {
            return vertexName.Equals(n.vertexName);
        }
        public void asigVert(int n)
        {
            numVertex = n;
        }

        public float getDistance()
        {
            return distance;
        }

        public void setDistance(float distance)
        {
            this.distance = distance;
        }

        public override string ToString()
        {
            return vertexName + " (" + numVertex + ")";
        }

        public int CompareTo(Vertex other)
        {
            if (this.distance < other.distance) return -1;
            else if (this.distance > other.distance) return 1;
            else return 0;
        }
    }
}