using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //TODO: while in air you slow down to the left but contiue your speed when moving right ???
    private GameObject player;
    private Rigidbody2D rb;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    private float inputX;
    private float inputY;

    public float jumpforce;
    private bool isGrounded = false;
    public LayerMask groundMask;

    public float acceleration;
    public float speed;
    private Vector2 currentVelocity;

    private bool isFacingRight;


    void Start()
    {
        player = gameObject;
        rb = player.GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {
        //left right movement
        if (Input.GetKey(right))
        {
            inputX = 1;
        }
        else if (Input.GetKey(left))
        {
            inputX = -1;
        }
        else
        {
            inputX = 0;
        }


        //jumping
        if (Input.GetKey(jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }

    }


    private void FixedUpdate()
    {

        //player doesnt stop mid air
        if (isGrounded)
        {
            Vector2 targetVelocity = new Vector2(inputX, rb.velocity.y).normalized * speed; //get max vel
            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, acceleration); //accelerate
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x, rb.velocity.y); //set velocity

        }


        //flipping when turning around
        if (isFacingRight && inputX > 0f || !isFacingRight && inputX < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;

        if ((groundMask & (1 << colliderLayer)) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;

        if ((groundMask & (1 << colliderLayer)) != 0)
        {
            isGrounded = false;
        }
    }


}
