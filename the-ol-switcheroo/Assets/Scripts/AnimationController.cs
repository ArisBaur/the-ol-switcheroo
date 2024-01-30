using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private playerMovement playerMov;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMov = GetComponent<playerMovement>();
    }

    private void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x)/2);
        anim.SetBool("isJumping", !playerMov.isGrounded);
    }
}
