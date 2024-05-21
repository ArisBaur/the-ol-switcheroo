using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class spike : MonoBehaviour
{

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private tries_counter triesCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        triesCounter.updateTries();
    }

}
