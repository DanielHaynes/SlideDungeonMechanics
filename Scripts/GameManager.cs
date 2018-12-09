using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    CardManager levelCardManager;
    ProgressManager levelProgressManager;
    UIManager levelUI;
    PlayerManager levelPlayerManager;
    HighScore highScore;

    [Range(1, 16)] [SerializeField] int startingCardNumber = 4;

    CardData nextCard;
    [SerializeField] int playerStartingHealth;
    
    bool playerCanMove = false;

    private void Awake() {
        levelPlayerManager = GameObject.FindObjectOfType<PlayerManager>();
        levelCardManager = GameObject.FindObjectOfType<CardManager>();
        levelProgressManager = GameObject.FindObjectOfType<ProgressManager>();
        levelUI = GameObject.FindObjectOfType<UIManager>();
        highScore = GameManager.FindObjectOfType<HighScore>();

        nextCard = new CardData(CardType.player);
    }

    private void Start() {
        //SaveLoad.ClearHighScores();
        playerCanMove = true;
        levelCardManager.FillGameBoard(startingCardNumber);
        UpdatePlayerScore(0);
        levelPlayerManager.CreatePlayer(playerStartingHealth);
        levelUI.CreatePlayerHealthBar(levelPlayerManager.GetPlayerHealth());
    }

    public bool CanPlayerMove() {
        if (playerCanMove == false) return false;
        if (levelCardManager.PlayerCanMove()) {
            return playerCanMove;
        }else {
            GameOver("Out of Moves!");
            return false;
        }
    }

    /// <summary>
    /// UpdatePlayerHealth is good for both healing and dealing damage to the player.
    /// </summary>
    /// <param name="health"></param>
    public void IncreasePlayerHealth(int increase) {
        levelPlayerManager.IncreasePlayerHealth(increase);
        levelUI.IncreasePlayerHealth(increase, false);
    }

    public void DecreasePlayerHealth(int decrease) {
        levelPlayerManager.DecreasePlayerHealth(decrease);
        levelUI.DecreasePlayerHealth(decrease);
    }

    public void IncreasePlayerMaxHealth(int health) {
        if (levelPlayerManager.IncreasePlayerMaxHealth(health)) {
            levelUI.IncreasePlayerHealth(levelPlayerManager.GetPlayerHealth(), true);
        }else {
            IncreasePlayerHealth(health);
        }
    }

    public bool CanPlayerSurvive(int damage) {
        if (levelPlayerManager.GetPlayerHealth() - damage <= 0) {
            return false;
        } else {
            return true;
        }
    }

    public void CreateNextCard() {
        CardData nextCardData = levelProgressManager.GetNextCard();
        nextCard = new CardData(nextCardData.cardType, nextCardData.cardValue);
        levelUI.SetNextCardImage(nextCardData);
    }

    public CardData GetNextCard() {
        CardData previousCard = new CardData(nextCard.cardType, nextCard.cardValue);
        CreateNextCard();
        return previousCard;
    }

    public int GetCurrentPlayerScore() {
        return levelPlayerManager.GetPlayerScore();
    }

    public void UpdatePlayerScore(int value) {
        levelUI.SetPlayerScore(levelPlayerManager.UpdatePlayerScore(value));
    }
    
    public void GameOver(string prompt) {
        playerCanMove = false;
        levelUI.PresentEndGame(prompt);
    }

    public void GameOverTrigger() {
        levelCardManager.TriggerEndGameScoring();
    }

    public void TriggerHighScore() {
        highScore.CreateDisplayHighscore(levelPlayerManager.GetPlayerScore());
    }
}