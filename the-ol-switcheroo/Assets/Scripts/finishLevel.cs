using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLevel : MonoBehaviour
{

    [SerializeField] private LayerMask playerMask;


    private void OnTriggerEnter2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;
        if ((playerMask & (1 << colliderLayer)) != 0)
        {
            Debug.Log("Proud Mother - credit: Steffi uwu");
        }
    }


}
