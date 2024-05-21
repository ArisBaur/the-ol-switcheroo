using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    [SerializeField] private Vector2 middleOfMap;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float lerpValue;
    private Vector2 camPos;


    // Update is called once per frame
    void Update()
    {
        camPos = mainCam.transform.position;
        transform.position = Vector2.Lerp(middleOfMap, camPos, lerpValue);
    }
}
