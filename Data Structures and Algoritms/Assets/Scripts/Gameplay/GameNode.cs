using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
[ExecuteInEditMode]
public class GameNode : MonoBehaviour
{
    [SerializeField]
    private NodeInfo nodeInfo;

    [SerializeField]
    private TextMeshPro nodeNameText;

    #region Properties
    public NodeInfo Info => nodeInfo;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = nodeInfo?.nodeName.ToString();
        nodeNameText.text = nodeInfo?.nodeName.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string[] GetAdyacents()
    {
        ArcInfo[] adyacents = nodeInfo.aydacents.ToArray();
        List<string> nodeNames = new List<string>();
        foreach (ArcInfo arcInfo in adyacents)
        {
            nodeNames.Add(arcInfo.adyacentName.ToString());
        }
        return nodeNames.ToArray();
    }
}
