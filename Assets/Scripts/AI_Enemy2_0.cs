using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy2_0 : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public float attackDuration;
    public int damage;

    private Transform player;
    private float attackTimer;
    Animator animator;

    const string IS_ATTACK = "isAttack", IS_WALKING = "isWalking";

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        
        if (speed == 0)
        {
            speed = 5;
        }

        if (attackRange == 0)
        {
            attackRange = 1;
        }

        if (attackDuration == 0)
        {
            attackDuration = 1;
        }

        if (damage == 0)
        {
            damage = 1;
        }

        attackTimer = 0;
    }

    void Update()
    {
        // Persigue al jugador si está lejos
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            animator.SetBool(IS_WALKING, true);
            animator.SetBool(IS_ATTACK, false);
        }
        else // Ataca al jugador si está cerca
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                //player.GetComponent<Health>().TakeDamage(damage);
                //PlayerScript.sharedInstance.SetHearts(PlayerScript.sharedInstance.GetHearts() - damage);
                PlayerScript.sharedInstance.hearts -= damage;
                attackTimer = attackDuration;
                animator.SetBool(IS_ATTACK, true);
            }
            animator.SetBool(IS_WALKING, false);
            animator.SetBool(IS_ATTACK, false);
        }
    }
}
