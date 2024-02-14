using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapInteractive : MonoBehaviour
{
    [SerializeField] private bool _isLayerA;
    [SerializeField] private float duration;
    [SerializeField] private Vector2 movementVector;
    [SerializeField][Range(0, 360)] private float rotateDegree;
    public bool isOpen { get; set; } = false;  //if its open
    public bool isClosing { get; set; } = true;// if it should close
    private bool isCoroutineRunning = false; //whether coroutin is active (prevents spamming)

    private Vector3 actualMovementVector;

    private Vector3 openPos;
    private Vector3 closePos;


    private void Start()
    {
        openPos = transform.position + new Vector3(movementVector.x, movementVector.y, 0);
        closePos = transform.position;
    }

    public void Open()
    {
        if (isOpen) { return; }
        isClosing = false;
    }

    public void Close()
    {
        if (!isOpen) { return; }
        isClosing = true;
    }

    private void FixedUpdate()
    {
        //prevents coroutines to start multiple times
        if (isCoroutineRunning) { return; }
        if (isOpen && isClosing)
        {   //close
            StartCoroutine(MoveCoroutine(openPos, closePos)); //pass current transform instead of using it in the function
            isOpen = false;
        }
        if (!isOpen && !isClosing)
        {   //open
            StartCoroutine(MoveCoroutine(closePos, openPos)); //because otherwise the gate chases itself
            isOpen = true;
        }
    }


    IEnumerator MoveCoroutine(Vector3 A, Vector3 B)
    {
        isCoroutineRunning = true;

        float runningTime = 0f;
        Vector3 startingPos = transform.position;

        while (runningTime < duration)
        {
            // how this iteration should take
            float t = SmoothStep(runningTime / duration);
            Debug.Log(t);

            // Interpolate position between fromPosition and toPosition using t
            transform.position = Vector3.Lerp(A, B, t);

            // Increment elapsed time
            runningTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position is reached
        transform.position = B;

        isCoroutineRunning = false;
    }



    float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }


}
