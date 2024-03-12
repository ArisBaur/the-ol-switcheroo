using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class spike : MonoBehaviour
{

    [SerializeField] private LayerMask playerMask;


    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //LayerMask colliderLayer = other.gameObject.layer;
        //if ((playerMask & (1 << colliderLayer)) != 0)
        //{
        //}
    }

}
