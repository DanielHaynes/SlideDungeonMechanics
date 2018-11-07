using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CardData is a subclass that can be used to instantiate data. 
/// Free of MonoBehaviour.
/// </summary>
public class CardData {
    public int cardValue { get; set; }
    public CardType cardType { get; set; }
    /// <summary>
    /// Sets CardData for cards in the deck, which will pass to data to the cards in cardmanager.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public CardData (CardType type, int value) {
        cardType = type;
        cardValue = value;
    }
}

public class Card : MonoBehaviour {
    //Card Display
    [SerializeField] private Image cardBGImage;
    [SerializeField] private Image cardImage;
    [SerializeField] private Text cardText;
    [SerializeField] private Text playerScoreText;

    //Card Data
    public CardType cardType;
    public SpecialType specialType;
    public int cardValue;
    public bool isActive = true;

    private Vector2 gridLocation;

    public void SetGridLocation(Vector2 location) {
        gridLocation = location;
    }

    public Vector2 GetGridLocation() {
        return gridLocation;
    }

    public bool SurviveDamage(int damage) {
        int tempValue = cardValue - damage;
        if (tempValue > 0) return true;
        return false;
    }

    public void TakeDamage(int damage) {
        cardValue -= damage;
        if (cardType == CardType.player) {
            GameManager.instance.UpdatePlayerHealth(cardValue);
        }
        SetCardDisplay();
    }

    public void AddValue (int value) {
        cardValue += value;
        if (cardType == CardType.player) {
            int tempMax = GameManager.instance.GetCurrentPlayerStatus().maxHealth;
            if (cardValue > tempMax) cardValue = tempMax;
            GameManager.instance.UpdatePlayerHealth(cardValue);
        }
        SetCardDisplay();
    }

    public void SetCardDisplay() {
        //if (cardType == CardType.player) {
        //    cardText.text = GameManager.instance.GetCurrentPlayerStatus().totalHealth.ToString() + "/" + GameManager.instance.GetCurrentPlayerStatus().maxHealth.ToString();
        //}else {
        //    cardText.text = cardValue.ToString();
        //}
        cardText.text = cardValue.ToString();
        playerScoreText.text = GameManager.instance.GetCurrentPlayerScore().ToString();
        if (cardType == CardType.player) {
            playerScoreText.enabled = true;
        }else {
            playerScoreText.enabled = false;
        }

        cardImage.sprite = Images.instance.GetCardImage(cardType);
        cardBGImage.sprite = Images.instance.GetCardBackground(cardType);
    }

    public void SetCardData(Card data) {
        cardType = data.cardType;
        cardValue = data.cardValue;
        SetCardDisplay();
    }

    public void SetCardData(CardType type) {
        cardType = type;
        SetCardDisplay();
    }

    public void SetCardData(int value) {
        cardValue = value;
        SetCardDisplay();
    }
    public void SetCardData(CardType type, int value) {
        cardType = type;
        cardValue = value;
        SetCardDisplay();
    }

    public void SetCardActiveState(bool state) {
        isActive = state;
        if (!isActive) {
            cardType = CardType.none;
            specialType = SpecialType.none;
            SetCardData(0);
        }
        gameObject.SetActive(isActive);
    }
   
    public CardType GetCardType() {
        return cardType;
    }

    public int GetCardValue() {
        return cardValue;
    }

    public bool IsCardActive() {
        return isActive;
    }
}