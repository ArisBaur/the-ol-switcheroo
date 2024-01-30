using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapPlayers : MonoBehaviour
{

    [SerializeField] private GameObject thisPlayer;
    [SerializeField] private GameObject thatPlayer;
    private Rigidbody2D thisRb;
    private Rigidbody2D thatRb;
    private playerMovement thisPm;
    private playerMovement thatPm;
    private Transform thisTf;
    private Transform thatTf;

    [SerializeField] private KeyCode switchKey;

    // Start is called before the first frame update
    void Start()
    {
        thisRb = thisPlayer.GetComponent<Rigidbody2D>();
        thatRb = thatPlayer.GetComponent<Rigidbody2D>();
        thisPm = thisPlayer.GetComponent<playerMovement>();
        thatPm = thatPlayer.GetComponent<playerMovement>();
        thisTf = thisPlayer.GetComponent<Transform>();
        thatTf = thatPlayer.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey)) {
            // create a "empty" rb for the transfer
            // following this principle:
            // to switch a and b, using a seperate container c
            // a -> c, b -> a, c -> b
            // this goes for the position, current rb xy velocity, movement velocity of the player (for the ground) and the flip
            Vector2 tempPosition = thisRb.position;
            Vector2 tempVelocity = thisRb.velocity;
            Vector2 tempCurrentVelocity = thisPm.currentVelocity;
            Vector3 tempLocalScale = thisTf.localScale;
            bool tempisFacingRight = thisPm.isFacingRight;

            thisRb.position = thatRb.position;
            thisRb.velocity = thatRb.velocity;
            thisPm.currentVelocity = thatPm.currentVelocity;
            thisTf.localScale = thatTf.localScale;
            thisPm.isFacingRight = thatPm.isFacingRight;

            thatRb.position = tempPosition;
            thatRb.velocity = tempVelocity;
            thatPm.currentVelocity = tempCurrentVelocity;
            thatTf.localScale = tempLocalScale;
            thatPm.isFacingRight = tempisFacingRight;
        }
    }
}
