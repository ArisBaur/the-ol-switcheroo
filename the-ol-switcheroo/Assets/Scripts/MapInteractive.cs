using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapInteractive : MonoBehaviour
{

    [SerializeField] private bool isRotate; //if it rotates or moves
    [SerializeField] private bool _isLayerA;
    [SerializeField] private float duration;
    [SerializeField] private Vector2 movementVector;
    public bool isOpen { get; set; } = false;  //if its open
    public bool isClosing { get; set; } = true;// if it should close
    private bool isCoroutineRunning = false; //whether coroutin is active (prevents spamming)

    Vector3 closePos;
    Vector3 openPos;

    private void Start()
    {
        Vector3 closePos = transform.position;
        Vector3 openPos = new Vector3(movementVector.x, movementVector.y, 0);
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
        if (isCoroutineRunning) { return; }
        if (isOpen && isClosing)
        {
            Debug.Log("Ich schließe mich");
            StartCoroutine(MoveCoroutine(transform.position, new Vector3(transform.position.x - movementVector.x, transform.position.y - movementVector.y, 0)));
            isOpen = false;
        }
        if (!isOpen && !isClosing)
        {
            Debug.Log("Ich öffne mich");
            StartCoroutine(MoveCoroutine(transform.position, new Vector3(transform.position.x + movementVector.x, transform.position.y + movementVector.y, 0)));
            isOpen = true;
        }
    }


    IEnumerator MoveCoroutine(Vector3 fromPos, Vector3 toPos)
    {
        isCoroutineRunning = true;

        float elapsedTime = 0f;
        float moveDuration = 1f; // Adjust as needed

        while (elapsedTime < moveDuration)
        {
            // Calculate the interpolation factor
            float t = elapsedTime / moveDuration;

            // Interpolate the position gradually
            transform.position = Vector3.Lerp(fromPos, toPos, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position is reached
        transform.position = toPos;

        isCoroutineRunning = false;
    }

}
