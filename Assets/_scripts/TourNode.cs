using System.Collections.Generic;
using UnityEngine;

public class TourNode : MonoBehaviour
{
    [Header("360 Image for this Node")]
    public Cubemap panoramaImage;

    [Header("Connected Nodes")]
    public List<TourNode> connectedNodes = new List<TourNode>();

    [Header("starter")]
    public bool starter_node;

    [Header("mini map")]
    public GameObject mini_map_icon;
    private bool icon_status;
    // Internal reference to main sphere that displays 360 images
    private static MeshRenderer sphereRenderer;

    // Called when entering this node
    public void EnterNode()
    {
        if (sphereRenderer == null)
        {
            
            GameObject sphere = GameObject.FindGameObjectWithTag("PanoramaSphere");
            if (sphere != null)
                sphereRenderer = sphere.GetComponent<MeshRenderer>();
            Debug.Log(sphereRenderer.material.name);
        }

        if (sphereRenderer != null && panoramaImage != null)
        {
            Debug.Log(sphereRenderer.material.name);
            Material mat = sphereRenderer.material;
            
            mat.SetTexture("_Tex", panoramaImage);
        }

      
        Debug.Log("Entered node: " + gameObject.name);
    }

    public void icon_true()
    {
       
        mini_map_icon.SetActive(true);

    }
    public void icon_false()
    {

        mini_map_icon.SetActive(false);

    }
}
