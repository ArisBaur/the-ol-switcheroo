using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLayerSetter : MonoBehaviour
{

    //check chatGPT

    [SerializeField] private bool isLayerA;
    [SerializeField] private string layerAName;
    [SerializeField] private string layerBName;

    private void Start()
    {
        int layerToSet = isLayerA ? LayerMask.NameToLayer(layerAName) : LayerMask.NameToLayer(layerBName);
        gameObject.layer = layerToSet;
    }
}
