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



/*  TABLE OF CONTENTS:
 * void Start:
 * - get gameObject and standart gravityscale(for jumping)
 * 
 * void Update:
 * - left-right movement input
 * - jumping + higher jumping
 * - handle flipping
 * 
 * void FixedUpdate:
 * - physics shit e.g:
 * - handle movement (if grounded)
 * 
 * OnTriggerEnter2D/Exit2D
 * - isGrounded check
 * 
 * 
 * 
 * 
 */



public class playerMovement : MonoBehaviour
{
    // my hoard of variables
    #region - my hoaaard
    private GameObject thisPlayer;
    private Rigidbody2D thisRb;
    [SerializeField] private bool playerType;
    private KeyCode left;
    private KeyCode right;
    private KeyCode jump;

    private float inputX;

    [SerializeField] private float jumpforce;
    [SerializeField] private float higherJumpModifier;
    private float standartJumpGravityScale;
    private bool isGrounded = false;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float speed;
    public Vector2 currentVelocity { get; set; }

    [SerializeField] public bool isFacingRight { get; set; }
    #endregion

    //executed at start of game
    void Start()
    {
        if (playerType)
        {
            left = KeyCode.A;
            right = KeyCode.D;
            jump = KeyCode.W;
        }
        else
        {
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            jump = KeyCode.UpArrow;
        }


        thisPlayer = gameObject;
        thisRb = thisPlayer.GetComponent<Rigidbody2D>();
        standartJumpGravityScale = thisRb.gravityScale;
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


        //flipping when turning around
        if (isFacingRight && inputX > 0f || !isFacingRight && inputX < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

    //executed at 60fps
    private void FixedUpdate()
    {

        //only when in contact with the ground can you change your velocity
        
        
        if (!isGrounded)
        {
            // if in air -> no control over velocity (the players can't fly duh)
            return;
        }

        //get target velocity
        Vector2 targetVelocity = new Vector2(inputX, thisRb.velocity.y).normalized * speed; //get max vel
        // acceleration
        if (inputX != 0) { currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration); }
        // deceleration
        else { currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, deceleration); }
        // set velocity
        thisRb.velocity = new Vector2(currentVelocity.x, thisRb.velocity.y); //set velocity

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
