using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;






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
    //general
    public bool scriptDisabled { get; set; }
    private GameObject thisPlayer;
    private Rigidbody2D thisRb;
    [SerializeField] private bool isPlayerA;

    //input
    private KeyCode left;
    private KeyCode right;
    private KeyCode jump;
    private float inputX;


    //jumping
    [SerializeField] private float jumpforce;
    [SerializeField] private float higherJumpModifier;
    private float standartJumpGravityScale;
    public bool isGrounded { get; set; }
    [SerializeField] private LayerMask groundMask;


    //x movement
    [SerializeField] private float acceleration;
    [SerializeField] private float airControll;
    [SerializeField] private float speed;
    public float currentSpeed;


    //swapping
    public bool isFacingRight { get; set; }


    //animation
    private Animator anim;

    //shader
    private int shaderThreshold = Shader.PropertyToID("_stepThreshold");
    private Material material;
    #endregion


    void Start()
    {
        //set corresponding input keys
        if (isPlayerA)
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

        //get components
        thisPlayer = gameObject;
        thisRb = thisPlayer.GetComponent<Rigidbody2D>();
        standartJumpGravityScale = thisRb.gravityScale;
        anim = thisPlayer.GetComponent<Animator>();
        material = thisPlayer.GetComponent<SpriteRenderer>().material;
    }

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

        //set animation variables -> to change the different animations
        anim.SetFloat("speed", Mathf.Abs(thisRb.velocity.x) / 2); //divided by 2 -> looks better? idk why tho
        anim.SetBool("isJumping", (thisRb.velocity.y > 0f && !isGrounded)); //if ur jumping up, ur y vel is positive, also u aint grounded
        anim.SetBool("isFalling", (thisRb.velocity.y < 0f && !isGrounded)); //vice versa

    }


    private void FixedUpdate()
    {
        if (scriptDisabled) { return; }

        //if grounded -> full controll
        if (isGrounded)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed * inputX, acceleration);
        }
        //air controll is slightly less
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed * inputX, acceleration * airControll);
        }

        thisRb.velocity = new Vector2(currentSpeed, thisRb.velocity.y);

    }

    //when touching ground
    private void OnTriggerEnter2D(Collider2D other)
    {
        //https://www.youtube.com/watch?v=VsmgZmsPV6w
        LayerMask colliderLayer = other.gameObject.layer;
        if ((groundMask & (1 << colliderLayer)) != 0)
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



    public void Dissapear(float duration)
    {
        StartCoroutine(DissapearCoroutine(0f, duration));
    }
    public void Appear(float duration)
    {
        StartCoroutine(DissapearCoroutine(1f, duration));
    }


    IEnumerator DissapearCoroutine(float targetVisibility, float duration)
    {
        float elapsedTime = 0f;
        float currentVisibility = material.GetFloat(shaderThreshold);
        Debug.Log($"current visibility: {currentVisibility}");

        while (elapsedTime < duration)
        {
            // Calculate the interpolation parameter based on elapsed time and duration
            float t = elapsedTime / duration;

            // Interpolate visibility between current and target values
            float visibility = Mathf.Lerp(currentVisibility, targetVisibility, t);

            // Update the material's visibility
            material.SetFloat(shaderThreshold, visibility);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final visibility is set to the target value
        material.SetFloat(shaderThreshold, targetVisibility);

        Debug.Log("Into the abyss!");
    }




}
