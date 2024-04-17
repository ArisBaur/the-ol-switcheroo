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
    [SerializeField] private playerMovement playerA;
    [SerializeField] private playerMovement playerB;


    [SerializeField] private float waitTime1;
    [SerializeField] private float playerFadeTime;
    [SerializeField] private float waitTime2;




    private void Start()
    {
        waitTime2 += playerFadeTime;


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
            }
        }
        else
        {
            // If the conditions are no longer met, cancel the coroutine if it's running
            if (loadingNextLevel)
            {
                StopCoroutine(nextLevelCoroutine);
                loadingNextLevel = false;
            }
        }
    }




    IEnumerator LoadNextLevel()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(waitTime1);

        // Dissapear the players
        playerA.Dissapear(playerFadeTime);
        playerB.Dissapear(playerFadeTime);
        playerA.scriptDisabled = true;
        playerB.scriptDisabled = true;


        // Wait for 2 seconds (5 - 3 = 2 seconds)
        yield return new WaitForSeconds(waitTime2);

        // Load the next level
        SceneManager.LoadScene(nextLevel);
    }

}
