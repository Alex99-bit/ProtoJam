using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] TypeOfCharacter charact;
    [SerializeField] float speed;

    Rigidbody2D enemyRb;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();

        switch (charact)
        {
            case TypeOfCharacter.goblin:
                if (speed == 0)
                {
                    speed = 7;
                }
                break;

            case TypeOfCharacter.slime:
                if (speed == 0)
                {
                    speed = 7;
                }
                speed += 5;
                break;

            default:
                Debug.Log("you shouldn't be watching this: Check your switch(charact)");
                break;
        }
    }

    private void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {

        }
    }

    enum TypeOfCharacter
    {
        goblin,
        slime
    }
}