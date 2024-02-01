using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class CameraController : MonoBehaviour
{

    private GameObject thisCamera;
    [SerializeField] private GameObject thisPlayer;
    [SerializeField] private GameObject thatPlayer;
    private Transform thisTf;
    private Transform thatTf;
    private float currentZoom;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraZoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private Rigidbody2D rb;

    private bool noErrors = false;


    void Start()
    {
        thisCamera = gameObject;
        rb = thisCamera.GetComponent<Rigidbody2D>();
    
        if (thisPlayer == null) { Debug.Log("Eyo, Camera: PlayerA is missing UwU"); }
        if (thatPlayer == null) { Debug.Log("Eyo, Camera: PlayerB is missing UwU"); }
        thisTf = thisPlayer.GetComponent<Transform>();
        thatTf = thatPlayer.GetComponent<Transform>();

        //if those two checks pass -> no immediate error
        noErrors = true;

    }

    
    void FixedUpdate()
    {
        if (!noErrors)
        {
            return;
        }

        // Calculate target midpoint between players
        float xPos = (thisTf.position.x + thatTf.position.x) /2f;
        Vector3 targetPoint = new Vector3(xPos, 1, -10);

        // zoom is 2\log_{2}\left(players distance\right)
        float targetZoom = 2f*Mathf.Log(Vector3.Distance(thisTf.position, thatTf.position), 2);
        //clamp the zoom
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);


        // smooth camera pos
        Vector3 currentPos = rb.position;
        Vector3 deltaPos = targetPoint - currentPos;
        rb.AddForce(deltaPos * cameraMoveSpeed, ForceMode2D.Impulse);
        rb.velocity *= 0.9f;
        
        // smooth zoom change
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, cameraZoomSpeed);
        thisCamera.GetComponent<Camera>().orthographicSize = currentZoom;

    }

}
