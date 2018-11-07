using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
    public int totalHealth;
    public int maxHealth;
    public PlayerData(int max, int total) {
        maxHealth = max;
        totalHealth = total;
    }
}

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    CardManager levelCardManager;
    DeckManager levelDeckManager;
    UIManager levelUI;
    bool DeckActive = true;

    [Range(1,16)] public int startingCardNumber = 4;

    PlayerData currentPlayerData;

    public int playerLevelScore { get; set; }

    public Card nextCard;
    

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        levelCardManager = GameObject.FindObjectOfType<CardManager>();
        levelDeckManager = GameObject.FindObjectOfType<DeckManager>();

        levelUI = GameObject.FindObjectOfType<UIManager>();
        currentPlayerData = new PlayerData(15,15);
    }

    public PlayerData GetCurrentPlayerStatus() {
        return currentPlayerData;
    }

    public void UpdatePlayerHealth(int health) {
        currentPlayerData.totalHealth = health;
    }

    public void DeckDepleted() {
        DeckActive = false;
    }

    public bool IsDeckActive() {
        return DeckActive;
    }

    private void Start() {
        levelCardManager.FillGameBoard(startingCardNumber);
        UpdatePlayerScore(0);
        CreateNextCard();
    }

    public void CreateNextCard() {
        CardData nextCardData = levelDeckManager.GetNextCard();
        nextCard.SetCardData(nextCardData.cardType, nextCardData.cardValue);
        levelUI.SetNextCardImage(levelDeckManager.GetNextCardForUI());
    }

    public Card GetNextCard() {
        Card temp = nextCard;
        CreateNextCard();
        return temp;
    }

    public int GetCurrentPlayerScore() {
        return playerLevelScore;
    }

    public void UpdatePlayerScore(int value) {
        //levelUI.UpdateScoreText(playerLevelScore += value);
        playerLevelScore += value;
    }
}