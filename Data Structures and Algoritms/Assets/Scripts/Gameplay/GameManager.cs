using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algoritms;
public class GameManager : MonoBehaviour
{
    HashSet<GameNode> gameNodes = new HashSet<GameNode>();
    Queue<string> pathWaypoints = new Queue<string>();
    [SerializeField]
    GameNode start;
    [SerializeField]
    GameNode end;
    [SerializeField]
    GameNode current;
    // Start is called before the first frame update
    void Start()
    {
        // TestPriorityQueue();
        TestWightedGraph();
    }

    public void AddGameNode(GameNode gameNode)
    {
        gameNodes.Add(gameNode);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetupAndFindPath()
    {
        // Setup graph with costs
        WeightedGraph weightedGraph = new WeightedGraph(gameNodes.Count, false);
        // Add vertexts to the graph
        foreach (GameNode gameNode in gameNodes)
        {
            weightedGraph.nuevoVertex(gameNode.Info.name);
        }
        // Set the relationships between vertexts
        foreach (GameNode gameNode in gameNodes)
        {
            string name = gameNode.Info.name;
            foreach (ArcInfo arcInfo in gameNode.Info.aydacents)
            {
                weightedGraph.nuevoArco(name, arcInfo.adyacentName.ToString(), arcInfo.cost);
            }
        }
        // Find the best path
        string[] path = FindPath(AlgoritmName.Dijsktra, weightedGraph, start.Info.name, end.Info.name);
        // Put way points into a queue
        for (int i = 0; i < path.Length; ++i)
        {
            pathWaypoints.Enqueue(path[path.Length - i]);
        }
        Debug.Log("Cola \n" + string.Join("-", pathWaypoints.ToArray()));
        current = start;
    }

    public string[] FindPath(AlgoritmName algoritmName, WeightedGraph weightedGraph, string start, string destiny)
    {
        string[] path = null;
        switch (algoritmName)
        {
            case AlgoritmName.Dijsktra:
                Dijkstra dijkstra = new Dijkstra();
                path = dijkstra.FindPath(weightedGraph, start, destiny);
                break;
            default:
                throw new Exception("No parameters specified");
        }
        return path;
    }

    public void RenderLine()
    {
        // Peek all the nodes from the queue and render a line

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

}
