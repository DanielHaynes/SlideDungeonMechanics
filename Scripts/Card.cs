using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class CardData {
    public int cardValue { get; set; }
    public CardType cardType { get; set; }
    
    public CardData(CardType type) {
        cardType = type;
        cardValue = 0;
    }

    public CardData (CardType type, int value) {
        cardType = type;
        cardValue = value;
    }
}

public class Card : MonoBehaviour {
    [SerializeField] Image cardBGImage;
    [SerializeField] Image cardImage;
    [SerializeField] Text cardValueText;
    [SerializeField] Text endGameScoreText;
    [SerializeField] Text cardNameText;
    [SerializeField] CardType cardType;
    [SerializeField] SpecialType specialType;
    [SerializeField] int cardValue;
    [SerializeField] bool isActive = true;

    private int endGameScore;
    private Vector2 gridLocation;

    private void Start() {
        endGameScoreText.text = "";
    }

    public void TriggerCardEffect() {
        print(cardType);
    }

    public void SetGridLocation(Vector2 location) {
        gridLocation = location;
    }

    public Vector2 GetGridLocation() {
        return gridLocation;
    }

    public bool SurviveDamage(int damage) {
        return GameObject.FindObjectOfType<GameManager>().CanPlayerSurvive(damage);
    }

    public void TakeDamage(int damage) {
        GameObject.FindObjectOfType<GameManager>().DecreasePlayerHealth(damage);
    }

    public void AddValue (int value) {
        cardValue += value;
        cardValueText.text = cardValue.ToString();
        if (cardType == CardType.player) {
            GameObject.FindObjectOfType<GameManager>().IncreasePlayerHealth(value);
        }
        SetCardDisplay();
    }

    public void RemoveCardName() {
        cardNameText.text = "";
    }

    public void SetCardDisplay() {
        cardImage.rectTransform.sizeDelta = new Vector2(128f, 128f);
        cardImage.sprite = GameObject.FindObjectOfType<Images>().GetCardImage(cardType, cardValue);
        cardBGImage.sprite = GameObject.FindObjectOfType<Images>().GetCardBackground(cardType, cardValue);
        
        if (cardType != CardType.player) {
            cardValueText.text = cardValue.ToString();
        } else {
            cardValueText.text = "";
        }
        cardNameText.color = Color.white;
        cardValueText.color = Color.white;
        switch (cardType) {
            case CardType.player:
                cardNameText.text = "hero";
                cardNameText.fontSize = 85;
                cardImage.rectTransform.sizeDelta = new Vector2(256f, 256f);
            break;
            case CardType.weapon:
                cardNameText.text = "sword";
                cardNameText.fontSize = 75;
                if (cardValue < 3) {
                    cardNameText.color = Color.black;
                    cardValueText.color = Color.black;
                    if (cardValue == 2) cardNameText.text = "anvil";
                    if (cardValue == 1) cardNameText.text = "ingot";
                }
            break;
            case CardType.potion:
                cardNameText.text = "potion";
                cardNameText.fontSize = 65;
            break;
            case CardType.coin:
                cardNameText.text = "loot";
                cardNameText.fontSize = 85;
            break;
            case CardType.enemy:
                cardNameText.text = "skull";
                cardNameText.fontSize = 75;
            break;
        }
    }
    
    public void SetCardData(Card data) {
        cardType = data.cardType;
        cardValue = data.cardValue;
        cardValueText.text = cardValue.ToString();
        SetCardDisplay();
    }

    public void SetCardData(CardData data) {
        cardType = data.cardType;
        cardValue = data.cardValue;
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

    public void DisplayEndGameScore() {
        if (cardValue < 3 || cardType == CardType.player) return;
        int value = GetEndGameScoreTotal();
        if (cardType == CardType.enemy) {
            endGameScoreText.text = "-" + Mathf.Abs(value).ToString();
        } else {
            endGameScoreText.text = "+" + value.ToString();
        }
        endGameScoreText.enabled = true;
    }

    public void EndGameScoreTextEmphasis() {
        if (cardValue < 3 || cardType == CardType.player) return;
        endGameScoreText.transform.DOScale(new Vector3(1.25f, 1.25f, 1f), 0.2f);
        if (cardType == CardType.enemy) {
            endGameScoreText.color = Color.red;
        } else {
            endGameScoreText.color = Color.green;
        }
    }

    public int GetEndGameScoreTotal() {
        if (cardValue < 3 || cardType == CardType.player) return 0;
        if (cardType == CardType.enemy) {
            return -1 * (int)Mathf.Pow(3, Mathf.Log((cardValue / 3), 2) + 1);
        } else {
            return (int)Mathf.Pow(3, Mathf.Log((cardValue / 3), 2) + 1);
        }
    }
}
