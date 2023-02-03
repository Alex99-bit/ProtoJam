using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int speed;

    Rigidbody2D playerRigid;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody2D>();

        if (speed == 0)
        {
            speed = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            Movement();
        }
    }

    void Movement()
    {
        // Player movement 
        playerRigid.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRigid.velocity.y);

        // for the animations
        if (Input.GetAxis("Horizontal") > 0)
        {
            // he goes to the right

        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            // he goes to the left
        }
        else
        {
            // is in idle
        }
    }
}
