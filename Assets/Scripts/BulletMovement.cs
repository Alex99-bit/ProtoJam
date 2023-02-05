using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform playerTF;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0)
        {
            speed = 10;
        }

        Dispara();
    }

    void Dispara()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2.5f);
    }
}
