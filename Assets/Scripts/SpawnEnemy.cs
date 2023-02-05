using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject slime, goblin;
    public TypeOfCharacter enemy;
    float seg;
    //static float auxTime;

    // Start is called before the first frame update
    void Start()
    {
        seg = 0;
        if (enemy == TypeOfCharacter.goblin)
        {
            Instantiate(goblin, this.transform);
        }
        else if (enemy == TypeOfCharacter.slime)
        {
            Instantiate(slime, this.transform);
        }
    }

    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (seg >= 25)
            {
                seg = 0;
                switch (enemy)
                {
                    case TypeOfCharacter.goblin:
                        Instantiate(goblin, this.transform);
                        break;
                    case TypeOfCharacter.slime:
                        Instantiate(slime, this.transform);
                        break;
                }
            }

            seg += Time.deltaTime;
        }
    }
}
