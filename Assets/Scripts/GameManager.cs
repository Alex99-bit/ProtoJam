using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singelton for manage the states of the game
    public static GameManager instance;
    public GameState currentGameState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetNewGameState(GameState.starGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SetNewGameState(GameState.inGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        SetNewGameState(GameState.gameOver);
    }

    public void SetNewGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.starGame:
                Time.timeScale = 0;
                break;

            case GameState.inGame:
                Time.timeScale = 1;
                break;

            case GameState.pause:
                Time.timeScale = 0;
                break;

            case GameState.gameOver:
                Time.timeScale = 0.6f;
                break;
        }

        currentGameState = newGameState;
    }
}

public enum GameState
{
    starGame,
    inGame,
    pause,
    gameOver
}