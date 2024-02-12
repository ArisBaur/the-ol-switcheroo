using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTest : MonoBehaviour
{

    [SerializeField] private ButtonController button;
    private bool isActive;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isActive = button.isPressed;

    }


    private void FixedUpdate()
    {
        if (isActive && isOpen)
        {
            transform.Rotate(0, 0, 90);
            isOpen = false;
        }
        if (isActive && !isOpen)
        {
            transform.Rotate(0, 0, -90);
            isOpen = true;
        }
    }

}
