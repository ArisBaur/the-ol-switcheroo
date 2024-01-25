using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapPlayers : MonoBehaviour
{

    [SerializeField] private GameObject otherPlayer;
    private Rigidbody2D otherRb;
    private GameObject thisPlayer;
    private Rigidbody2D thisRb;

    [SerializeField] private KeyCode switchKey;

    // Start is called before the first frame update
    void Start()
    {
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();
        thisPlayer = gameObject;
        thisRb = thisPlayer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey)) {
            //create a "empty" rb for the transfer
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
}
