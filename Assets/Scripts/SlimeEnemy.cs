using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    Rigidbody2D enemyRb;
    Transform enemyTf;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyTf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
