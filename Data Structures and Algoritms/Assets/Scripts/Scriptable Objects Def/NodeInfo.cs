using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArcInfo
{
    public NodeName adyacentName;
    public float cost;

    public ArcInfo(NodeName adyacentName, float cost)
    {
        this.adyacentName = adyacentName;
        this.cost = cost;
    }
}

[CreateAssetMenu(fileName = "NodeInfo", menuName = "ScriptableObjects/Node Info", order = 1)]
public class NodeInfo : ScriptableObject
{
    public NodeName nodeName;
    public List<ArcInfo> aydacents;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
