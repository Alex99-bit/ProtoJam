using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript sharedInstance;

    [SerializeField] float speed, jumpForce;
    [SerializeField] bool canJump;
    [SerializeField] int hearts, auxHearts;

    Rigidbody2D playerRigid;
    Animator animator;
    Transform pt;

    // Animations states
    const string IS_ALIVE = "isAlive", IS_RUNNING = "isRunning", IS_GROUND = "isOnTheGround";

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody2D>();
        pt = GetComponent<Transform>();
        canJump = true;

        if (speed == 0)
        {
            speed = 10;
        }

        if (jumpForce == 0)
        {
            jumpForce = 5;
        }

        hearts = 3;
        auxHearts = hearts;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            Movement();
            Jump();

            print("Alvvvvvv si jalalalalalalal");
        }
    }

    void Movement()
    {
        // Player movement 
        playerRigid.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRigid.velocity.y);
        print("Si jalalalalalala por dooooooos");

        // for the animations
        if (Input.GetAxis("Horizontal") > 0)
        {
            // he goes to the right
            animator.SetBool(IS_RUNNING, true);
            pt.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            // he goes to the left
            animator.SetBool(IS_RUNNING, true);
            pt.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // is in idle
            animator.SetBool(IS_RUNNING, false);
        }
    }

    void Jump()
    {
        if (canJump && Input.GetButton("Jump"))
        {
            playerRigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            animator.SetBool(IS_GROUND, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            animator.SetBool(IS_GROUND, true);
        }
    }

    public int GetHearts()
    {
        return hearts;
    }

    public void SetHearts(int newHeart)
    {
        hearts = newHeart;
    }

    public int GetAuxHeart()
    {
        return auxHearts;
    }
}
