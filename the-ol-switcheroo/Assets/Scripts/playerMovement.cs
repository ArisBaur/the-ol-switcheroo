using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private GameObject player;
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    private int inputX;
    private int inputY;

    public float acceleration;
    private Vector2 velocity;


    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {

        inputX = 0;
        inputY = 0;

        if(Input.GetKeyUp(up))
        {
            inputY = 1;
        }
        if(Input.GetKeyUp(left))
        {
            inputX = -1;
        }
        if (Input.GetKeyUp(down))
        {
            inputY = -1;
        }
        if(Input.GetKeyUp(right))
        {
            inputX = 1;
        }
    }


    private void FixedUpdate()
    {
        if (inputX < 0)
        {
            player.GetComponent<Rigidbody2D>().position += new Vector2(-1, 0);
        }
        if (inputX > 0)
        {
            player.GetComponent<Rigidbody2D>().position += new Vector2(1, 0);
        }
        if (inputY < 0)
        {
            player.GetComponent<Rigidbody2D>().position += new Vector2(0, -1);
        }
        if (inputY > 0)
        {
            player.GetComponent<Rigidbody2D>().position += new Vector2(0, 1);
        }
    }

}
