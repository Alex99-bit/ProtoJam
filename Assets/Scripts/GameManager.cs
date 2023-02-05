using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singelton for manage the states of the game
    public static GameManager instance;


    public GameState currentGameState;
    public GameObject mainMenu, inGame, pause, options, gameOver, exeption;
    public GameObject heart1, heart2, heart3;
    public float contadorAlv;
    public TextMeshProUGUI gameOverText, bullets;

    bool auxExeption;
    float coolDownExept;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        SetNewGameState(GameState.starGame);
        contadorAlv = 0;
        exeption.SetActive(false);
        auxExeption = false;
        coolDownExept = 0;
        //bullets.text = "x" + PlayerScript.sharedInstance.GetBullets();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.inGame)
        {
            bullets.text = "x" + PlayerScript.sharedInstance.GetBullets();

            if (Input.GetButtonDown("Pause"))
            {
                SetNewGameState(GameState.pause);
            }

            switch (PlayerScript.sharedInstance.GetHearts())
            {
                case 3:
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                    heart3.SetActive(true);
                    break;
                case 2:
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                    heart3.SetActive(false);
                    break;
                case 1:
                    heart1.SetActive(true);
                    heart2.SetActive(false);
                    heart3.SetActive(false);
                    break;
            }

            if (PlayerScript.sharedInstance.GetHearts() <= 0)
            {
                heart1.SetActive(false);
                GameOver("Game Over");
            }
        }
        else if(currentGameState == GameState.pause)
        {
            if (Input.GetButtonDown("Pause"))
            {
                SetNewGameState(GameState.inGame);
            }
        }

        if (auxExeption && currentGameState == GameState.starGame)
        {
            coolDownExept += Time.deltaTime;
            if (coolDownExept >= 2)
            {
                auxExeption = false;
                exeption.SetActive(auxExeption);
                coolDownExept = 0;
            }
        }

        contadorAlv += Time.deltaTime;
    }

    public void ResetGame()
    {
        SetNewGameState(GameState.starGame);
        SetNewGameState(GameState.inGame);
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
        //SetNewGameState(GameState.options);
        exeption.SetActive(true);
        auxExeption = true;
    }

    public void GameOver(string newText)
    {
        gameOverText.text = newText;
        SetNewGameState(GameState.gameOver);
    }

    // Here selects the game state and how it behaves
    public void SetNewGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.starGame:
                Time.timeScale = 0;
                PlayerScript.sharedInstance.SetHearts(PlayerScript.sharedInstance.GetAuxHeart());
                PlayerScript.sharedInstance.SetBullets(PlayerScript.sharedInstance.GetAuxBullets());
                mainMenu.SetActive(true);
                inGame.SetActive(false);
                pause.SetActive(false);
                options.SetActive(false);
                gameOver.SetActive(false);
                exeption.SetActive(false);
                auxExeption = false;
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