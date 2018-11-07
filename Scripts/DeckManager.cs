using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {
    public CardData[] deck;
    int deckIndex = 0;
    public int startingDeckSize = 25;

    private void Awake() {
        CreateDeck();
        ShuffleDeck();
    }

    void CreateDeck() {
        deck = new CardData[startingDeckSize];
        for (int index = 0; index < startingDeckSize; index++) {
            int randomInt = Random.Range(0, 200);

            CardType assignType;
            if (randomInt > 79)  {
                assignType = CardType.enemy;
            } else if (randomInt > 59) {
                assignType = CardType.weapon;
            } else if (randomInt > 39) {
                assignType = CardType.coin;
            } else if (randomInt > 19) {
                assignType = CardType.potion;
            } else {
                assignType = CardType.shield;
            }
            deck[index] = new CardData(assignType, Random.Range(2, 10));
        }
    }

    public void ShuffleDeck() {
        for (var index = deck.Length - 1; index > 0; index--) {
            int randomIndex = Random.Range(0, index);
            CardData tmp = deck[index];
            deck[index] = deck[randomIndex];
            deck[randomIndex] = tmp;
        }
    }
    
    public CardData GetNextCardForUI() {
        if (deck[deckIndex] != null) {
            return deck[deckIndex];
        }else {
            return null;
        }
    }

    public CardData GetNextCard() {
        if (deckIndex <= startingDeckSize - 1) {
            return deck[deckIndex++];
        } else {
            CardData temp = new CardData(CardType.none, 0);
            GameManager.instance.DeckDepleted();
            return temp;
        }
    }
}