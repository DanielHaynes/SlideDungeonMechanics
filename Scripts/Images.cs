using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Images : MonoBehaviour {
    public Sprite enemyCardImage;
    public Sprite weaponCardImage;
    public Sprite shieldCardImage;
    public Sprite coinCardImage;
    public Sprite playerCardImage;
    public Sprite potionCardImage;
    public Sprite cardBGImage;
    public Sprite cardBGImagePlayer;
    public static Images instance = null;

    private void Awake() {
        if (instance == null)  {
            instance = this;
        } else { 
            Destroy(gameObject);
        }
    }

    public Sprite GetCardBackground(CardType type) {
        if (type == CardType.player) {
            return cardBGImagePlayer;
        }else {
            return cardBGImage;
        }
    }

    public Sprite GetCardImage(CardType type) {
        switch (type) {
            case CardType.enemy: return enemyCardImage;
            case CardType.weapon: return weaponCardImage;
            case CardType.shield: return shieldCardImage;
            case CardType.coin: return coinCardImage;
            case CardType.player: return playerCardImage;
            case CardType.potion: return potionCardImage;
        }
        //Debug.Log("WARNING: Missing a cardtype image for " + type);
        return enemyCardImage;
    }
}