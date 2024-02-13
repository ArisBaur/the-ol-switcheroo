using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapLayerSetter : MonoBehaviour
{

    //check chatGPT

    [SerializeField] private bool isLayerA;
    [SerializeField] private string layerAName;
    [SerializeField] private string layerBName;
    [SerializeField] private Material layerAMaterial;
    [SerializeField] private Material layerBMaterial;

    private void Update()
    {
        int layerToSet = (isLayerA ? LayerMask.NameToLayer(layerAName) : LayerMask.NameToLayer(layerBName)) % 31;
        gameObject.layer = layerToSet;
        
        Material matToSet = isLayerA ? layerAMaterial : layerBMaterial;
        gameObject.GetComponent<SpriteRenderer>().material = matToSet;
    }

    public void ChangeLayer()
    {
        isLayerA = !isLayerA;
    }

}