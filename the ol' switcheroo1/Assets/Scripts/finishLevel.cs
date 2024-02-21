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
    [HideInInspector]public bool isReached = false;
    private bool loadingNextLevel = false;
    private Coroutine nextLevelCoroutine;

    

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
        if (otherGoal.isReached && isReached)
        {
            // Check if the coroutine is already running
            if (!loadingNextLevel)
            {
                // Start the coroutine to load the next level
                nextLevelCoroutine = StartCoroutine(LoadNextLevel());
                loadingNextLevel = true;
                Debug.Log("Start transferring");
            }
        }
        else
        {
            // If the conditions are no longer met, cancel the coroutine if it's running
            if (loadingNextLevel)
            {
                StopCoroutine(nextLevelCoroutine);
                loadingNextLevel = false;
                Debug.Log("Stopped");
            }
        }
    }




    IEnumerator LoadNextLevel()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(nextLevel);
    }

}
