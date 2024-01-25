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
    private Vector3 currentPos;
    private float currentZoom;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraZoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;




    void Start()
    {
        thisCamera = gameObject;
        thisTf = thisPlayer.GetComponent<Transform>();
        thatTf = thatPlayer.GetComponent<Transform>();
    }

    
    void FixedUpdate()
    {
        // Calculate target midpoint between players
        float xPos = (thisTf.position.x + thatTf.position.x) /2f;
        Vector3 targetPoint = new Vector3(xPos, 1, -10);

        // zoom is 2\log_{2}\left(players distance\right)
        float targetZoom = 2f*Mathf.Log(Vector3.Distance(thisTf.position, thatTf.position), 2);
        //clamp the zoom
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);



        // smooth position change
        currentPos = Vector3.Lerp(currentPos, targetPoint, cameraMoveSpeed);
        transform.position = currentPos;
        // smooth zoom change
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, cameraZoomSpeed);
        thisCamera.GetComponent<Camera>().orthographicSize = currentZoom;

        
        

    }

}
