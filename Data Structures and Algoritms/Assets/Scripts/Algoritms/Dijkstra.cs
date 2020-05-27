using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Algoritms
{

    public class Dijkstra
    {
        public const float INFINITY = float.MaxValue;
        public const int MAX = 200;
        float[] distancia = new float[MAX]; // distancia[ u ] distancia de vértice inicial al vertice
        bool[] visitado = new bool[MAX]; // para vértices visitados
        int[] previo = new int[MAX]; // para la impresion de caminos
        PriorityQueue<Vertex> cola = new PriorityQueue<Vertex>();
        int numVertices;
        List<int> caminoBack = new List<int>();
        int backIndex = 0;
        public Dijkstra()
        {

        }

        public int[] FindPath(WeightedGraph weightedGraph, int start, int destiny)
        {
            int[] camino = null;
            this.numVertices = weightedGraph.NumVerts;
            init(this.numVertices);
            cola.Enqueue(weightedGraph.Vertexts[start]); // Insertamos el vértice inicial en la Cola de Prioridad
            distancia[start] = 0; // Este paso es importante, inicializamos la distancia del inicial como 0
                                  // Recorrido
            while (!cola.IsEmpty())
            { // Mientras cola no este vacia
                Vertex actual = cola.Dequeue();// Obtengo de la cola el nodo con menor peso, en un comienzo será el inicial
                if (visitado[actual.NumVertex])
                    continue; // Si el vértice actual ya fue visitado entonces sigo sacando elementos de la
                              // cola
                              // No ha sido visitado
                visitado[actual.NumVertex] = true; // Marco como visitado el vértice actual
                                                   // Veo sus adyacentes, ya que lo visite
                for (int i = 0; i < this.numVertices; ++i)
                {
                    if (weightedGraph.WeightMatrix[actual.NumVertex, i] != INFINITY)
                    {
                        Vertex adyacente = weightedGraph.Vertexts[i];
                        // peso de la arista que une actual con adyacente ( actual , adyacente )
                        float peso = weightedGraph.WeightMatrix[actual.NumVertex, i];
                        if (!visitado[adyacente.NumVertex])
                        { // //si el vertice adyacente no fue visitado
                          // Comprueba distancia para relajar
                            relajacion(actual, adyacente, peso); // realizamos el paso de relajacion
                        }
                    }
                }
            }
            caminoBack.Add(destiny);
            backIndex++;
            camino = recuperarCamino(destiny);
            List<int> caminoList = camino.ToList();
            caminoList.Reverse();
            return caminoList.ToArray();
        }
        public string[] FindPath(WeightedGraph weightedGraph, string startName, string destinyName)
        {
            int start = weightedGraph.numVertex(startName);
            int destiny = weightedGraph.numVertex(destinyName);
            if (start == -1 || destiny == -1)
                throw new Exception("Vertex no existe");
            int[] camino = null;
            this.numVertices = weightedGraph.NumVerts;
            init(this.numVertices);
            cola.Enqueue(weightedGraph.Vertexts[start]); // Insertamos el vértice inicial en la Cola de Prioridad
            distancia[start] = 0; // Este paso es importante, inicializamos la distancia del inicial como 0
                                  // Recorrido
            while (!cola.IsEmpty())
            { // Mientras cola no este vacia
                Vertex actual = cola.Dequeue();// Obtengo de la cola el nodo con menor peso, en un comienzo será el inicial
                if (visitado[actual.NumVertex])
                    continue; // Si el vértice actual ya fue visitado entonces sigo sacando elementos de la
                              // cola
                              // No ha sido visitado
                visitado[actual.NumVertex] = true; // Marco como visitado el vértice actual
                                                   // Veo sus adyacentes, ya que lo visite
                for (int i = 0; i < this.numVertices; ++i)
                {
                    if (weightedGraph.WeightMatrix[actual.NumVertex, i] != INFINITY)
                    {
                        Vertex adyacente = weightedGraph.Vertexts[i];
                        // peso de la arista que une actual con adyacente ( actual , adyacente )
                        float peso = weightedGraph.WeightMatrix[actual.NumVertex, i];
                        if (!visitado[adyacente.NumVertex])
                        { // //si el vertice adyacente no fue visitado
                          // Comprueba distancia para relajar
                            relajacion(actual, adyacente, peso); // realizamos el paso de relajacion
                        }
                    }
                }
            }
            caminoBack.Add(destiny);
            backIndex++;
            camino = recuperarCamino(destiny);
            List<int> caminoList = camino.ToList();
            caminoList.Reverse();
            return ObtenerCaminoNombres(weightedGraph, caminoList.ToArray());
        }

        public string[] ObtenerCaminoNombres(WeightedGraph grValorado, int[] caminoIds)
        {
            List<string> camino = new List<string>();
            foreach (int id in caminoIds)
            {
                if (id != -1)
                {
                    camino.Add(grValorado.Vertexts[id].VertextName);
                }
            }
            return camino.ToArray();
        }

        public int[] recuperarCamino(int destino)
        {
            caminoBack.Add(previo[destino]);
            if (previo[destino] != -1)
                recuperarCamino(previo[destino]);

            return caminoBack.ToArray();
        }

        // función de inicialización
        public void init(int numVert)
        {
            distancia = new float[numVert];
            previo = new int[numVert];
            visitado = new bool[numVert];
            for (int i = 0; i < numVertices; ++i)
            {
                distancia[i] = INFINITY; // inicializamos todas las distancias con valor infinito
                visitado[i] = false; // inicializamos todos los vértices como no visitado
                previo[i] = -1; // inicializamos el previo del vértice i con -1
            }
        }

        // Paso de relajacion
        public void relajacion(Vertex actual, Vertex adyacente, float peso)
        {
            // Si la distancia del origen al vertice actual + peso de su arista
            // es menor a la distancia del origen al vertice adyacente
            if (distancia[actual.NumVertex] + peso < distancia[adyacente.NumVertex])
            {
                distancia[adyacente.NumVertex] = distancia[actual.NumVertex] + peso; // relajamos el vertice actualizando
                                                                                     // la distancia
                previo[adyacente.NumVertex] = actual.NumVertex; // a su vez actualizamos el vertice previo
                                                                // No es necesario
                adyacente.setDistance(distancia[actual.NumVertex] + peso);
                cola.Enqueue(adyacente); // agregamos adyacente a la cola de prioridad
            }
        }
    }
}