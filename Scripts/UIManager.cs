using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Image nextCardImage;

    public void SetNextCardImage(CardData cardData) {
        nextCardImage.sprite = Images.instance.GetCardImage(cardData.cardType);
    }
}