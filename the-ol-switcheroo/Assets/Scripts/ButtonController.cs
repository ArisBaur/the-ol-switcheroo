using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [SerializeField] MapInteractive[] openCloseGates;
    [SerializeField] mapLayerSetter[] layerGates;
    [SerializeField] private bool isLayerA;
    [SerializeField] private LayerMask playerMask;
    private bool playerInReach;
    private KeyCode activationKey;
    //private bool isPressed = false;

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

            foreach (var gate in openCloseGates)
            {
                if (gate.isOpen)
                {
                    gate.Close();
                }
                else
                {
                    gate.Open();
                }
            }
            foreach (var gate in layerGates)
            {
                gate.ChangeLayer();
            }
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
