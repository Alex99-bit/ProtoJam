using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] TypeOfCharacter charact;
    [SerializeField] float speed, jumpForce;
    [SerializeField] int life;
    [SerializeField] Transform playerTf;
    [SerializeField] Vector3 distancePlayer;
    [SerializeField] LayerMask enemyMask, ground;
    [SerializeField] GameObject feet;

    Animator animator;

    bool playerCloser, attack, follow, touchingGround;
    const string IS_ATTACK = "isAttack", IS_WALKING = "isWalking";

    Rigidbody2D enemyRb;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        playerTf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        playerCloser = false;

        StartCoroutine(Attack());

        if (speed == 0)
        {
            speed = 7;
        }

        if (life == 0)
        {
            life = 3;
        }

        switch (charact)
        {
            case TypeOfCharacter.goblin:
                speed += 5;
                life *= 1;
                break;

            case TypeOfCharacter.slime:
                speed *= 1;
                life += 2;

                if (jumpForce == 0)
                {
                    jumpForce = 10;
                }
                break;

            default:
                Debug.Log("You shouldn't be watching this: Check your switch(charact)");
                break;
        }
    }

    private void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            distancePlayer = playerTf.transform.position - this.transform.position;

            StartFolloWThePlayer();

            if (follow)
            {
                StopChar();
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (follow)
            {
                FollowPlayer();
                Jump();
            }
        }   
    }

    void FollowPlayer()
    {
        // Logic follow and attack the player
        if (!playerCloser)
        {
            transform.Translate(distancePlayer * Time.deltaTime * speed);
            animator.SetBool(IS_WALKING, true);
            attack = false;
        }
        else
        {
            transform.Translate(distancePlayer * 0);
            animator.SetBool(IS_WALKING, false);
            attack = true;
        }

        switch (charact)
        {
            case TypeOfCharacter.goblin:
                if (playerTf.position.x > transform.position.x)
                {
                    // The player is in the right
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                break;

            case TypeOfCharacter.slime:
                if (playerTf.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                break;
        }
    }

    void StopChar()
    {
        float aux;

        if (!Physics2D.Raycast(this.transform.position,distancePlayer, 2f, enemyMask))
        {
            if (!(Physics2D.Raycast(feet.transform.position, Vector2.right, 2.5f, ground)) || !(Physics2D.Raycast(feet.transform.position, Vector2.left, 2.5f, ground)))
            {
                if (playerTf.position.y > this.transform.position.y)
                {
                    aux = 2f;
                    if ((playerTf.position.y) <= (this.transform.position.y + aux))
                    {
                        playerCloser = true;
                    }
                    else
                    {
                        playerCloser = false;
                    }
                }
                else if (playerTf.position.y < this.transform.position.y)
                {
                    aux = -2f;
                    if (playerTf.position.y >= (this.transform.position.y + aux))
                    {
                        playerCloser = true;
                    }
                    else
                    {
                        playerCloser = false;
                    }
                }
                else
                {
                    aux = 0;
                    playerCloser = true;
                }
            }
            else
            {
                playerCloser = false;
            }
        }
        else
        {
            playerCloser = false;
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (GameManager.instance.currentGameState == GameState.inGame)
            {
                if (attack)
                {
                    animator.SetBool(IS_ATTACK, true);
                    PlayerScript.sharedInstance.SetHearts(PlayerScript.sharedInstance.GetHearts() - 1);
                }
                else 
                {
                    animator.SetBool(IS_ATTACK, false);
                }
            }
        }
    }

    void StartFolloWThePlayer()
    {
        if (Physics2D.Raycast(this.transform.position, distancePlayer, 10f, enemyMask))
        {
            follow = true;
        }
        else
        {
            follow = false;
            animator.SetBool(IS_WALKING, false);
        }
    }

    void Jump()
    {
        if (charact == TypeOfCharacter.slime)
        {
            if (touchingGround)
            {
                enemyRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                touchingGround = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            touchingGround = true;
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            life--;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

public enum TypeOfCharacter
{
    goblin,
    slime
}