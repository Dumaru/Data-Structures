using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algoritms;
public class GameManager : MonoBehaviour
{
    List<GameNode> gameNodes = new List<GameNode>();
    GameNode start;
    GameNode end;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("Sevilla")?.GetComponent<GameNode>();
        end = GameObject.Find("Barcelona")?.GetComponent<GameNode>();

        // TestPriorityQueue();
        TestWightedGraph();

    }

    private void TestWightedGraph()
    {
        WeightedGraph grafoValorado = new WeightedGraph(5, false);
        grafoValorado.nuevoVertex("A");
        grafoValorado.nuevoVertex("B");
        grafoValorado.nuevoVertex("C");
        grafoValorado.nuevoVertex("D");
        grafoValorado.nuevoVertex("E");
        grafoValorado.nuevoArco("A", "B", 1);
        grafoValorado.nuevoArco("B", "C", 2);
        grafoValorado.nuevoArco("B", "D", 2);
        grafoValorado.nuevoArco("A", "C", 1);
        grafoValorado.nuevoArco("D", "E", 10);
        Debug.Log(grafoValorado.ToString());
        Dijkstra caminoCorto = new Dijkstra();
        string[] camino = caminoCorto.FindPath(grafoValorado, "A", "E");
        Debug.Log("Camino :" + string.Join("-> ", camino));
    }

    public string ObtenerCaminoNombres(WeightedGraph grValorado, int[] caminoIds)
    {
        string msg = "";
        foreach (int id in caminoIds)
        {
            if (id != -1)
            {
                msg += "-> " + grValorado.Vertexts[id];
            }
        }
        return msg;
    }
    private void TestPriorityQueue()
    {
        Debug.Log("Begin Priority Queue demo");
        Debug.Log("Creating priority queue of Vertex items");
        PriorityQueue<Vertex> pq = new PriorityQueue<Vertex>();
        Vertex a = new Vertex("A");
        Vertex b = new Vertex("B");
        Vertex c = new Vertex("C");
        pq.Enqueue(a);
        pq.Enqueue(b);
        pq.Enqueue(c);

        Debug.Log("Priority Queue \n" + pq.ToString());
        // Demo code here 

        Debug.Log("End Priority Queue demo");
    }

    public void AddGameNode(GameNode gameNode)
    {
        gameNodes.Add(gameNode);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public string[] FindPath(AlgoritmName algoritmName)
    {
        string[] path = null;
        switch (algoritmName)
        {
            case AlgoritmName.Dijsktra:

                break;
            default:
                FindPath(AlgoritmName.Dijsktra);
                break;
        }
        return path;
    }
}
