using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumppad : MonoBehaviour
{


    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float jumpheight;



    private void OnTriggerEnter2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;
        if ((playerMask & (1 << colliderLayer)) != 0)
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpheight);
        }
    }


}
