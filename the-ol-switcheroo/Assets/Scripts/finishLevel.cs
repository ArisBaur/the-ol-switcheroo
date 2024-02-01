using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishLevel : MonoBehaviour
{

    [SerializeField] private string nextLevel;
    [SerializeField] private LayerMask playerMask;
    private finishLevel[] goals;
    private finishLevel otherGoal;
    public bool isReached = false;
    

    private void Start()
    {
        goals = FindObjectsOfType<finishLevel>();

        // Find the other goal that is not the current one
        foreach (finishLevel goal in goals)
        {
            // Check if the goal is different from the current one
            if (goal != this)
            {
                otherGoal = goal;
                break;
            }
        }

        if (otherGoal == null)
        {
            Debug.LogError("Unable to find the other goal!");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;
        if ((playerMask & (1 << colliderLayer)) != 0)
        {
            isReached = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;
        if ((playerMask & (1 << colliderLayer)) != 0)
        {
            isReached = false;
        }
    }

    private void Update()
    {
        if (otherGoal.isReached && this.isReached)
        {
            Debug.Log("go on! lets goo");
            SceneManager.LoadScene(nextLevel);
        }
        else if(this.isReached)
        {
            Debug.Log("not yet");
        }

    }


}
