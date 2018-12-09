using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour {
    CardManager levelCards;

    private void Start() {
        levelCards = GameObject.FindObjectOfType<CardManager>();
    }

    public CardData GetNextCard() {
        Card[,] cards = levelCards.GetCurrentCards();
        int enemyValue = 0;
        int coinValue = 0;
        int potionValue = 0;
        int weaponValue = 0;
        int weapon1 = 0;
        int weapon2 = 0;
        CardType assignType = CardType.enemy;
        int assignValue = 3;

        for (int row = 0; row < 4; row++) {
            for (int col = 0; col < 4; col++) {
                switch (cards[row, col].GetCardType()) {
                    case CardType.enemy:
                    enemyValue += cards[row, col].GetCardValue();
                    break;
                    case CardType.coin:
                    coinValue += cards[row, col].GetCardValue(); ;
                    break;
                    case CardType.potion:
                    potionValue += cards[row, col].GetCardValue(); ;
                    break;
                    case CardType.weapon:
                    weaponValue += cards[row, col].GetCardValue();
                    if (cards[row, col].GetCardValue() == 1) weapon1++;
                    if (cards[row, col].GetCardValue() == 2) weapon2++;
                    break;
                }
            }
        }

        if (weaponValue >= enemyValue * 2) {
           assignType = CardType.enemy;
        } else if (enemyValue >= weaponValue * 2) {
            assignType = CardType.weapon;
        } else {
            switch(Random.Range(0, 3)) {
                case 0:
                    assignType = CardType.weapon;
                    break;
                case 1:
                    assignType = CardType.enemy;
                    break;
                case 2:
                    assignType = CardType.potion;
                    break;
            }
        }

        if (assignType == CardType.weapon) {
            assignValue = (weapon1 > weapon2) ? 2 : 1;
            int switchAssignValue = Random.Range(0, 2);
            if (switchAssignValue == 1) assignValue = 3;
        } 

        return new CardData(assignType, assignValue);
    } 
}