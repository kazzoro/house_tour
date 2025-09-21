using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;      // Prefab for navigation arrows
    public Transform arrowParent;       // UI Canvas parent (under the player)

    public TourNode currentNode;
    public TourNode[] allnodes;
    
    void Start()
    {
        // Find the starting node
        allnodes = FindObjectsOfType<TourNode>();
        foreach (var node in allnodes)
        {
            if (node.starter_node)   // check for starter
            {
                MoveToNode(node);
                disable_icon(node);
                break;
            }
        }
    }

    public void MoveToNode(TourNode node)
    {
        if (node == null) return;

      

        // Move player to node position (X and Z only)
        Vector3 newPos = new Vector3(node.transform.position.x, transform.position.y, node.transform.position.z);
        transform.position = newPos;

        // Update 360 sphere texture
        node.EnterNode();

        // Update arrows
        UpdateArrows(node);

        // Remember current node
        currentNode = node;
        disable_icon(currentNode);
    }

    public void disable_icon(TourNode node_selected)
    {
        foreach (var node in allnodes)
        {
            if (node != node_selected) // skip the selected node
            {
                node.icon_true();
            }
            else
            {
                node.icon_false();
            }
        }
    }
    private void UpdateArrows(TourNode node)
    {
        // Destroy old arrows
        for (int i = arrowParent.childCount - 1; i >= 0; i--)
        {
            Destroy(arrowParent.GetChild(i).gameObject);
        }

        // Spawn arrows for each connected node
        foreach (TourNode connected in node.connectedNodes)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowParent);

            // Get Arrow_Rotation script
            arrow_rotation arrowRot = arrow.GetComponent<arrow_rotation>();
            if (arrowRot != null)
            {
                arrowRot.target = connected.gameObject.transform;
            }

          //   Hook button click
            Button btn = arrow.GetComponent<Button>();
            if (btn != null)
            {
                
                TourNode capturedNode = connected; // capture for closure
                btn.onClick.AddListener(() => MoveToNode(capturedNode));
                btn.onClick.AddListener(() => Debug.Log("touched")); 
            }
           
        }
    }
}
