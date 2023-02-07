using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy2_0 : MonoBehaviour
{
    public float speed = 5;
    public float attackRange = 1;
    public float attackDuration = 1;
    public int damage = 1;

    private Transform player;
    private float attackTimer = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
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
                player.GetComponent<Health>().TakeDamage(damage);
                attackTimer = attackDuration;
            }
        }
    }
}
