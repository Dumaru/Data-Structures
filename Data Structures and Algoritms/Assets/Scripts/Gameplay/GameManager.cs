﻿using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algoritms;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject targetToMove;
    TextMeshPro targetToMoveText;
    Vector3 distanceOffset = new Vector3(0.2f, 0f, 0.2f);
    HashSet<GameNode> gameNodes = new HashSet<GameNode>();
    [SerializeField]
    Stack<string> pathWaypoints = new Stack<string>();
    [SerializeField]
    GameNode start;
    [SerializeField]
    GameNode end;
    [SerializeField]
    GameNode current;
    GameNode next;
    bool movingTarget = false;
    bool alreadySearched = false;

    Camera cameraRef;
    float speed = 0.3f;
    LayerMask interactableLayer;
    bool startSetted;
    bool finishSetted;
    [SerializeField]
    GameObject startMarker;
    [SerializeField]
    GameObject endMarker;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        interactableLayer = LayerMask.NameToLayer("Interactable");
        targetToMove = (targetToMove == null ? GameObject.FindGameObjectWithTag("Target") : targetToMove);
        targetToMove.transform.position = start.gameObject.transform.position - distanceOffset;
        targetToMoveText = targetToMove.GetComponentInChildren<TextMeshPro>();
        cameraRef = Camera.main;
        audioSource = GetComponent<AudioSource>();
        ChangeMarkersPosition();
    }

    public void AddGameNode(GameNode gameNode)
    {
        gameNodes.Add(gameNode);
    }
    // Update is called once per frame
    void Update()
    {
        if (movingTarget)
        {
            targetToMove.transform.position = Vector3.Lerp(targetToMove.transform.position, next.gameObject.transform.position, Time.deltaTime * speed);
            // Debug.Log("Distance " + Vector3.Distance(targetToMove.transform.position, next.transform.position));
            if (Vector3.Distance(targetToMove.transform.position, next.transform.position) < 0.2)
            {
                SetTargetDestiny();
            }
        }

        CheckForInput();
    }

    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cameraRef.transform.position, cameraRef.transform.forward, out RaycastHit hit, interactableLayer))
            {
                if (hit.transform.gameObject.CompareTag("GameNode"))
                {
                    GameNode temp = hit.transform.gameObject.GetComponent<GameNode>();
                    if (startSetted && finishSetted)
                    {
                        start = temp;
                        finishSetted = false;
                    }
                    else if (startSetted && !finishSetted)
                    {
                        end = temp;
                        finishSetted = true;
                    }
                }
            }
        }
    }

    private void ChangeMarkersPosition()
    {
        startMarker.transform.position = start.transform.position + distanceOffset;
        endMarker.transform.position = end.transform.position + distanceOffset;
    }

    public void SetupAndFindPath()
    {
        audioSource.Play();

        Debug.Log("Call to find path");
        ChangeMarkersPosition();
        if (gameNodes.Count > 1)
        {
            Debug.Log("Call to find path with gameNodes");

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
            Debug.Log("Path => " + string.Join("->", path));
            // Put way points into a queue
            for (int i = 1; i <= path.Length; ++i)
            {
                pathWaypoints.Push(path[path.Length - i]);
            }
            current = start;
            targetToMove.transform.position = current.transform.position;
            SetTargetDestiny();
        }
        else
        {
            Debug.Log("The list of nodes is empty");
        }
    }

    public void SetTargetDestiny()
    {
        if (pathWaypoints.Count != 0)
        {
            String nextName = pathWaypoints.Pop();
            next = gameNodes.Where(node => (node as GameNode).Info.nodeName.ToString() == nextName).First();
            Debug.Log("The next " + next.gameObject.name);
            movingTarget = true;
            targetToMoveText.text = next.gameObject.name;
        }
        else
        {
            Debug.Log("Arrived to destination");
            audioSource.Play();
            movingTarget = false;
        }
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


    private void TestWeightedGraph()
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
