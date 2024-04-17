using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapLayerSetter : MonoBehaviour
{

    [SerializeField] private bool isLayerA;
    [SerializeField] private bool isMapNonInteractive;
    [SerializeField] private Material layerAMaterial;
    [SerializeField] private Material layerBMaterial;
    private string layerAName = "MapA";
    private string layerBName = "MapB";

    private void Update()
    {
        if (!isMapNonInteractive)
        {
            int layerToSet = (isLayerA ? LayerMask.NameToLayer(layerAName) % 31 : LayerMask.NameToLayer(layerBName)) % 31;
            gameObject.layer = layerToSet;
        }
        
        Material matToSet = isLayerA ? layerAMaterial : layerBMaterial;
        gameObject.GetComponent<SpriteRenderer>().material = matToSet;
    }

    public void ChangeLayer()
    {
        isLayerA = !isLayerA;
    }

}