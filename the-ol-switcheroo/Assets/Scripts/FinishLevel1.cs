using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float nextLEvelDelay;

    private FinishLevel otherGoal;
    private bool isReached = false;

    private void Start()
    {
        FinishLevel[] goals = FindObjectsOfType<FinishLevel>();

        // Find the other goal that is not the current one
        foreach (FinishLevel goal in goals)
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
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(nextLEvelDelay);
        SceneManager.LoadScene(nextLevel);
    }
}
