using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapInteractive : MonoBehaviour
{
    [SerializeField] private bool _isLayerA;
    [SerializeField][Min(0.1f)] private float duration;
    [SerializeField] private Vector2 movementVector;
    [SerializeField][Range(-360, 360)] private float rotateDegree;
    public bool isOpen { get; set; } = false;  //if its open
    public bool isClosing { get; set; } = true;// if it should close
    private bool isCoroutineRunning = false; //whether coroutin is active (prevents spamming)

    private Vector3 actualMovementVector;

    private Vector3 openPos;
    private Vector3 closePos;
    private Quaternion openRotation;
    private Quaternion closeRotation;

    private void Start()
    {
        openPos = transform.position + new Vector3(movementVector.x, movementVector.y, 0);
        closePos = transform.position;
        openRotation = transform.rotation * Quaternion.AngleAxis(rotateDegree, Vector3.forward);
        closeRotation = transform.rotation;
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
            StartCoroutine(MoveCoroutine(openPos, closePos, openRotation, closeRotation)); //pass current transform instead of using it in the function
            isOpen = false;
        }
        if (!isOpen && !isClosing)
        {   //open
            StartCoroutine(MoveCoroutine(closePos, openPos, closeRotation, openRotation)); //because otherwise the gate chases itself
            isOpen = true;
        }
    }


    IEnumerator MoveCoroutine(Vector3 A, Vector3 B, Quaternion a, Quaternion b)
    {
        isCoroutineRunning = true;

        float runningTime = 0f;
        Vector3 startingPos = transform.position;

        while (runningTime < duration)
        {
            //how long this iteration should take
            float t = runningTime / duration;

            //linear interpolation from A to B using timesteps t
            transform.position = Vector3.Lerp(A, B, t);
            transform.rotation = Quaternion.Lerp(a, b, t);

            //increase time
            runningTime += Time.deltaTime;

            //end this iteration and wait for the next one
            yield return null;
        }


        transform.position = B;
        transform.rotation = b;
        isCoroutineRunning = false;
    }




}
