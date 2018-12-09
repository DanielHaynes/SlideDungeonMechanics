using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Images : MonoBehaviour {
    public Sprite enemyCardImage;
    public Sprite completeBowCardImage;
    public Sprite bowCardImage;
    public Sprite quiverCardImage;
    public Sprite coinCardImage;
    public Sprite playerCardImage;
    public Sprite potionCardImage;
    public Sprite cardBGImageBlack;
    public Sprite cardBGImageWhite;
    public Sprite cardBGImageBeige;

    public Sprite GetCardBackground(CardType type, int value) {
        if (type == CardType.player) {
            return cardBGImageBlack;
        }else if (type == CardType.weapon){
            if (value ==2) {
                return cardBGImageWhite; 
            }else if (value ==1) {
                return cardBGImageBeige;
            }else {
                return cardBGImageBlack;
            }
        }else {
            return cardBGImageBlack;
        }
    }

    public Sprite GetCardImage(CardType type, int value) {
        switch (type) {
            case CardType.enemy: return enemyCardImage;
            case CardType.weapon: {
                switch(value) {
                        case 1: return quiverCardImage;
                        case 2: return bowCardImage;
                        case 3: return completeBowCardImage;
                        default: return completeBowCardImage;

                    }
            }
            case CardType.coin: return coinCardImage;
            case CardType.player: return playerCardImage;
            case CardType.potion: return potionCardImage;
        }
        return enemyCardImage;
    }
}