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

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        
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
        }
        else // Ataca al jugador si está cerca
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                //player.GetComponent<Health>().TakeDamage(damage);
                PlayerScript.sharedInstance.hearts = PlayerScript.sharedInstance.hearts - damage;
                attackTimer = attackDuration;
            }
        }
    }
}
