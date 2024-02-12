using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private bool isLayerA;
    [SerializeField] private LayerMask playerMask;
    private bool playerInReach;
    private KeyCode activationKey;


    // Start is called before the first frame update
    void Start()
    {
        if (isLayerA)
        {
            activationKey = KeyCode.E;
        }
        else
        {
            activationKey = KeyCode.RightShift;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInReach && Input.GetKeyDown(activationKey))
        {
            Debug.Log("Button pressed");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;
        if ((playerMask & (1 << colliderLayer)) != 0)
        {
            playerInReach = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;
        if ((playerMask & (1 << colliderLayer)) != 0)
        {
            playerInReach = false;
        }
    }






}
