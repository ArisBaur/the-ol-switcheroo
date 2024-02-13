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


    private void Start()
    {
        actualMovementVector = new Vector3(movementVector.x, movementVector.y, 0);
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
            StartCoroutine(MoveCoroutine(transform.position - actualMovementVector)); //pass current transform instead of using it in the function
            isOpen = false;
        }
        if (!isOpen && !isClosing)
        {   //open
            StartCoroutine(MoveCoroutine(transform.position + actualMovementVector)); //because otherwise the gate chases itself
            isOpen = true;
        }
    }


    IEnumerator MoveCoroutine(Vector3 endPos)
    {
        isCoroutineRunning = true;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the interpolation factor
            float t = elapsedTime / duration;

            // Interpolate the position gradually
            transform.position = Vector3.Lerp(transform.position, endPos, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position is reached
        transform.position = endPos;

        isCoroutineRunning = false;
    }

}
