using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singelton for manage the states of the game
    public static GameManager instance;
    public GameState currentGameState;
    public GameObject mainMenu, inGame, pause, options, gameOver;
    public float contadorAlv;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        SetNewGameState(GameState.starGame);
        contadorAlv = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.inGame)
        {
            if (Input.GetButton("Pause"))
            {
                SetNewGameState(GameState.pause);
            }
        }
        else if(currentGameState == GameState.pause)
        {
            if (Input.GetButton("Pause"))
            {
                SetNewGameState(GameState.inGame);
            }
        }

        contadorAlv += Time.deltaTime;
    }

    public void StartGame()
    {
        SetNewGameState(GameState.inGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        SetNewGameState(GameState.options);
    }

    public void GameOver()
    {
        SetNewGameState(GameState.gameOver);
    }

    // Here selects the game state and how it behaves
    public void SetNewGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.starGame:
                Time.timeScale = 0;
                mainMenu.SetActive(true);
                inGame.SetActive(false);
                pause.SetActive(false);
                options.SetActive(false);
                gameOver.SetActive(false);
                break;

            case GameState.inGame:
                Time.timeScale = 1;
                mainMenu.SetActive(false);
                inGame.SetActive(true);
                pause.SetActive(false);
                options.SetActive(false);
                gameOver.SetActive(false);
                break;

            case GameState.pause:
                Time.timeScale = 0;
                mainMenu.SetActive(false);
                inGame.SetActive(false);
                pause.SetActive(true);
                options.SetActive(false);
                gameOver.SetActive(false);
                break;

            case GameState.gameOver:
                Time.timeScale = 0.6f;
                mainMenu.SetActive(false);
                inGame.SetActive(false);
                pause.SetActive(false);
                options.SetActive(false);
                gameOver.SetActive(true);
                break;

            case GameState.options:
                Time.timeScale = 0;
                mainMenu.SetActive(false);
                inGame.SetActive(false);
                pause.SetActive(false);
                options.SetActive(true);
                gameOver.SetActive(false);
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
    gameOver,
    options
}