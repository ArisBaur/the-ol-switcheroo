using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLayerSetter : MonoBehaviour
{

    //check chatGPT

    [SerializeField] private bool isLayerA;
    [SerializeField] private LayerMask LayerA;
    [SerializeField] private LayerMask LayerB;

    private void Start()
    {
        if (isLayerA)
        {
            gameObject.layer = LayerA;
        }
        else
        {
            gameObject.layer = LayerB;
        }
    }

}
