using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
[ExecuteInEditMode]
public class GameNode : MonoBehaviour
{
    public static int materialNumber = 0;
    [SerializeField]
    private NodeInfo nodeInfo;

    [SerializeField]
    private TextMeshPro nodeNameText;

    GameManager gameManager;
    Material customMaterial;
    [SerializeField]
    MeshRenderer meshRenderer;
    #region Properties
    public NodeInfo Info => nodeInfo;
    #endregion

    private void Awake()
    {
        materialNumber = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = nodeInfo?.nodeName.ToString();
        nodeNameText.text = nodeInfo?.nodeName.ToString();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddGameNode(this);
        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        materialNumber++;
        Color color = new Color32((byte)Random.Range(0, 255),
                                  (byte)Random.Range(0, 255),
                                  (byte)Random.Range(0, 255), 255);
        customMaterial = new Material(Shader.Find("Standard"));
        customMaterial.name = "Custom Material " + materialNumber;
        customMaterial.color = color;
        customMaterial.SetColor("_EmissionColor", color);
        customMaterial.EnableKeyword("_EMISSION");
        meshRenderer.material = customMaterial;
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
