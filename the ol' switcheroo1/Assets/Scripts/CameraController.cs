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
    [SerializeField] private float dampingFactor;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraZoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float cameraMultiplier;

    private Rigidbody2D rb;
    
    private bool noErrors = false;


    void Start()
    {
        thisCamera = gameObject;
        rb = thisCamera.GetComponent<Rigidbody2D>();
        currentZoom = thisCamera.GetComponent<Camera>().orthographicSize;
    
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


        // Calculate x and y pos
        float xPos = (thisTf.position.x + thatTf.position.x) / 2f;
        float yPos = (thisTf.position.y + thatTf.position.y) / 2f;
        Vector3 targetPoint = new Vector3(xPos, yPos, -10);


        // zoom is 2\log_{2}\left(players distance\right)
        //float targetZoom = 2f*Mathf.Log(Vector3.Distance(thisTf.position, thatTf.position), 2);
        float targetZoom = (thisTf.position - thatTf.position).magnitude;
        //clamp the zoom
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);


        // smooth camera pos
        Vector3 currentPos = rb.position;
        Vector3 deltaPos = targetPoint - currentPos;
        rb.AddForce(deltaPos * cameraMoveSpeed, ForceMode2D.Impulse);
        rb.velocity *= dampingFactor;

        // smooth zoom change
        currentZoom = Mathf.Lerp(currentZoom, targetZoom * cameraMultiplier, cameraZoomSpeed/1000);
        thisCamera.GetComponent<Camera>().orthographicSize = currentZoom;


        Debug.Log($"current Zoom: {currentZoom}, target Zoom: {targetZoom}");
    }

}
