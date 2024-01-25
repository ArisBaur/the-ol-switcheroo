using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/* TODO: work on swap players
 * wanted to put code from swap players.cs here
 * because i think i need to edit currentvelocity instead of rb.velocity
 * jumping with horzontal movement bug
 * 
 * 
*/



public class playerMovement : MonoBehaviour
{
    // my hoard of variables
    private GameObject thisPlayer;
    private Rigidbody2D thisRb;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode jump;

    private float inputX;

    [SerializeField] private float jumpforce;
    [SerializeField] private float higherJumpModifier;
    private float standartJumpGravityScale;
    private bool isGrounded = false;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float acceleration;
    [SerializeField] private float speed;
    public Vector2 currentVelocity;

    [SerializeField] private bool isFacingRight;

    private Rigidbody2D otherRb;
    [SerializeField] private GameObject otherPlayer;
    [SerializeField] private KeyCode switchKey;

    //executed at start of game
    void Start()
    {
        thisPlayer = gameObject;
        thisRb = thisPlayer.GetComponent<Rigidbody2D>();
        standartJumpGravityScale = thisRb.gravityScale;
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();
    }

    //executed every frame
    void Update()
    {
        //left right movement
        if (Input.GetKey(right)) { inputX = 1; }
        else if (Input.GetKey(left)) { inputX = -1; }
        else { inputX = 0; }

        //jumping
        if (Input.GetKey(jump) && isGrounded)
        {   //only change y velocity -> to jumping force
            thisRb.velocity = new Vector2(thisRb.velocity.x, jumpforce);
        }
        //longer jump, when holding space
        thisRb.gravityScale = standartJumpGravityScale;
        if (Input.GetKey(jump) && thisRb.velocity.y > 0f) //when flying up (vel.y > 0)
        {   //lower gravity
            thisRb.gravityScale = standartJumpGravityScale * higherJumpModifier;
        }

        //switching positions
        if (Input.GetKeyDown(switchKey))
        {
            //create a "empty" thisRb for the transfer
            // following this principle:
            // to switch a and b, using a seperate container c
            // a -> c, b -> a, c -> b
            //Vector2 tempPosition = thisRb.position;
            Vector2 tempVelocity = new Vector2(thisRb.velocity.x, 0);

            //thisRb.position = otherRb.position;
            thisRb.velocity = new Vector2(otherRb.velocity.x, 0);

            //otherRb.position = tempPosition;
            otherRb.velocity = new Vector2(tempVelocity.x, 0);

        }
    }

    //executed at 60fps
    private void FixedUpdate()
    {
        //only when in contact with the ground can you change your velocity
        if (isGrounded)
        {
            Vector2 targetVelocity = new Vector2(inputX, thisRb.velocity.y).normalized * speed; //get max vel
            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, acceleration); //accelerate
            thisPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x, thisRb.velocity.y); //set velocity
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

    //when touching ground
    private void OnTriggerEnter2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;
        if ((groundMask & (1 << colliderLayer)) != 0)
        //check if the layer of the collider is in the layermask which counts as ground
        //idk how it works tho ;-;
        //i tried but its a bit weird
        //ik that those are bitwise operations, thats it
        {
            isGrounded = true;
        }
    }

    //when no longer touching ground (falling/jumping)
    private void OnTriggerExit2D(Collider2D other)
    {
        LayerMask colliderLayer = other.gameObject.layer;
        if ((groundMask & (1 << colliderLayer)) != 0)
        {
            isGrounded = false;
        }
    }


}
