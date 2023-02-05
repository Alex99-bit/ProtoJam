using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] TypeOfCharacter charact;
    [SerializeField] float speed;
    [SerializeField] int life;
    [SerializeField] Transform playerTf;
    [SerializeField] Vector3 distancePlayer;
    [SerializeField] LayerMask enemyMask, ground;
    [SerializeField] GameObject feet;

    Animator animator;

    bool playerCloser, attack, follow;
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
            FollowPlayer();
            distancePlayer = playerTf.transform.position - this.transform.position;

            if (follow)
            {
                StopChar();
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
                }
                else
                {
                    animator.SetBool(IS_ATTACK, false);
                }
            }
        }
    }

    enum TypeOfCharacter
    {
        goblin,
        slime
    }
}