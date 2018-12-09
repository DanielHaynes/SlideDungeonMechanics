using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

enum UI_GameState
{
    none,
    activeGame,
    endGameStart,
    endGame,
    highScore,
    gameOver
}

public class UIManager : MonoBehaviour {
    [SerializeField] Image nextCardImage;
    [SerializeField] Image nextCardBGImage;
    [SerializeField] Text scoreText;
    [SerializeField] Text endGameScoreText;
    [SerializeField] Text endGamePromptText;
    [SerializeField] Text endGamePromptText2;
    [SerializeField] HealthBar playerHealthBar;

    UI_GameState currentUIGameState;

    private void Start() {
        currentUIGameState = UI_GameState.activeGame;
        endGameScoreText.enabled = false;
        endGamePromptText.enabled = false;
        endGamePromptText2.enabled = false;
    }

    public void SetPlayerScore(int score) {
        scoreText.text = "$ " + score.ToString();
        endGameScoreText.text = "$" + score.ToString();
    }

    public void CreatePlayerHealthBar(int startingHealth) {
        playerHealthBar.SetHealthBarStart(startingHealth);
    }

    public void IncreasePlayerHealth(int health, bool changeMax) {
        playerHealthBar.IncreaseHealthBar(health, changeMax);
    }

    public void DecreasePlayerHealth(int health) {
        playerHealthBar.DecreaseHealthBar(health);
    }

    public void SetNextCardImage(CardData cardData) {
        nextCardImage.sprite = GameObject.FindObjectOfType<Images>().GetCardImage(cardData.cardType, cardData.cardValue);
        nextCardBGImage.sprite = GameObject.FindObjectOfType<Images>().GetCardBackground(cardData.cardType, cardData.cardValue);
    }

    public void PresentEndGame(string prompt = "Out of Moves!") {
        scoreText.enabled = false;
        currentUIGameState = UI_GameState.endGameStart;
        SetEndGamePrompt(prompt, true); 
    }
    
    void SetEndGamePrompt(string prompt, bool status) {
        endGamePromptText.text = prompt;
        endGamePromptText.enabled = status;
        endGamePromptText2.enabled = true;
        endGamePromptText2.text = "Tap to see score!";
    }

    /// <summary>
    /// Will toggle off the standard score text and enable the end game score.
    /// </summary>
    public void PresentEndGameScore() {
        SetEndGamePrompt("", false);
        endGameScoreText.enabled = true;
    }

    public void RestartGame() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnableHighScore() {
        currentUIGameState = UI_GameState.highScore;
        endGamePromptText2.enabled = true;
    }

    public void EnableGameRestart() {
        Invoke("TriggerRestart", 1f);
    }

    void TriggerRestart() {
        currentUIGameState = UI_GameState.gameOver;
    }

    private void Update() {
        if (currentUIGameState == UI_GameState.activeGame) return;
        if (currentUIGameState == UI_GameState.endGameStart) {
            if (Input.anyKeyDown) {
                PresentEndGameScore();
                endGamePromptText2.text = "Tap to save score!";
                GameObject.FindObjectOfType<GameManager>().GameOverTrigger();
                currentUIGameState = UI_GameState.endGame;
            }
        }
        if (currentUIGameState == UI_GameState.endGame) {
            if (Input.anyKeyDown) {
                GameObject.FindObjectOfType<CardManager>().SpeedUpEndGameScore();
                currentUIGameState = UI_GameState.none;
            }
        }

        if (currentUIGameState == UI_GameState.highScore) {
            if (Input.anyKeyDown) {
                currentUIGameState = UI_GameState.none;
                GameObject.FindObjectOfType<GameManager>().TriggerHighScore();
            }
        }

        if (currentUIGameState == UI_GameState.gameOver) {
            if (Input.anyKeyDown) RestartGame();
        }
    }
}