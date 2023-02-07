using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript sharedInstance;
    public GameObject jumpVFX;

    [SerializeField] float speed, jumpForce, shootForce;
    [SerializeField] bool canJump, canTakeBullet;
    public int hearts, auxHearts;
    public int bullets, auxBullets;
    [SerializeField] GameObject bulletPlayer;
    [SerializeField] Transform spawnBullet, spawn;

    Rigidbody2D playerRigid;
    Animator animator;
    [SerializeField] Transform pt, transformVFX;

    // Animations states
    const string IS_SHOOTING = "isShooting", IS_RUNNING = "isRunning", IS_GROUND = "isOnTheGround", IS_FALLING = "isFalling";
    int cons;
    float coolDownBullet;

    private void Awake()
    {
        if (sharedInstance == null)
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
        transformVFX = GameObject.Find("VFX").GetComponent<Transform>();
        canJump = true;
        cons = 0;
        coolDownBullet = 0;
        canTakeBullet = true;

        if (speed == 0)
        {
            speed = 10;
        }

        if (jumpForce == 0)
        {
            jumpForce = 5;
        }

        if (bullets == 0)
        {
            bullets = 4;
        }

        if (shootForce == 0)
        {
            shootForce = 15;
        }

        hearts = 3;
        auxHearts = hearts;

        auxBullets = bullets;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            Movement();
            Jump();
            Shoot();

            if (!canTakeBullet)
            {
                coolDownBullet += Time.deltaTime;
                if (coolDownBullet >= 3f)
                {
                    coolDownBullet = 0;
                    canTakeBullet = true;
                }
            }

            print("Alvvvvvv si jalalalalalalal");
        }
    }

    public void RestartPos()
    {
        //pt.transform.position = spawn.transform.position;
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
            Instantiate(jumpVFX, transformVFX.position, transformVFX.rotation);
            canJump = false;
            animator.SetBool(IS_GROUND, false);
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && bullets > 0)
        {
            animator.SetBool(IS_SHOOTING, true);
            // Here we instantiate the bullet when the player are shooting
            GameObject pellet = Instantiate(bulletPlayer, spawnBullet.position, spawnBullet.rotation);
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
            Transform pelletTf = pellet.GetComponent<Transform>();

            if (pt.localScale.x == 1)
            {
                rb.AddForce(spawnBullet.right * shootForce, ForceMode2D.Impulse);
            }
            else if(pt.localScale.x == -1)
            {
                pelletTf.localScale = new Vector3(-0.2820178f, 0.2820178f, 0.2820178f);
                rb.AddForce(spawnBullet.right * (-1) * shootForce, ForceMode2D.Impulse);
            }
            canTakeBullet = false;
            bullets--;
        }
        else
        {
            Debug.Log("No es posible disparar");
            animator.SetBool(IS_SHOOTING, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            animator.SetBool(IS_GROUND, true);
        }

        if (collision.gameObject.CompareTag("Bullet") && canTakeBullet)
        {
            bullets++;
            Destroy(collision.gameObject);
        }
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && canJump)
        {
            canJump = false;
            animator.SetBool(IS_GROUND, false);
            animator.SetBool(IS_FALLING, true);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "calcetas" || collision.gameObject.name == "flauta" || collision.gameObject.name == "collar")
        {
            Destroy(collision.gameObject);
            cons++;
            bullets += 2;
            if (cons >= 3)
            {
                GameManager.instance.GameOver("Thanks for Playing");
            }
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

    public int GetBullets()
    {
        return bullets;
    }

    public int GetAuxBullets()
    {
        return auxBullets;
    }

    public void SetBullets(int newBullets)
    {
        bullets = newBullets;
    }
}
