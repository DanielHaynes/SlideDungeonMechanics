using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public Sprite healthBar;
	public Sprite flashBar;
	int numberOfLives;
    int currentLifePoints;
	float startWidth = 465.5f;
	float startHeight = 61f;
	float startXPos = -522.8f;
	float startYPos = -129f;
	GameObject[] heartBars;

    public void SetHealthBarStart(int startValue) {
        numberOfLives = startValue;
        currentLifePoints = numberOfLives - 1;//currentLifePoints used for array index
        CreateHealthBar();
    }
	
    void CreateHealthBar() {
		heartBars = new GameObject[numberOfLives];

		float widthSize = startWidth / numberOfLives;
		for (int i = 0; i < numberOfLives; i++) {
			GameObject temp = new GameObject();
			temp.AddComponent<Image>();
			temp.GetComponent<Image>().sprite = healthBar;
			temp.GetComponent<RectTransform>().sizeDelta = new Vector2(widthSize, startHeight);
			temp.GetComponent<RectTransform>().SetParent(transform);
			temp.GetComponent<RectTransform>().pivot = new Vector2 (0f,0f);
			temp.transform.localScale = new Vector3(1f,1f,1f);
			temp.transform.localPosition = new Vector2 ((widthSize * i) + startXPos, startYPos);
			heartBars[i] = temp;
		}
	}

    void ResetBar() {
        foreach (GameObject g in heartBars) {
            Destroy(g);
        }
    }
    
    public void IncreaseHealthBar(int changeAmount, bool changeMax) {
        if ((currentLifePoints + changeAmount > numberOfLives) && changeMax) {
            ResetBar();
            numberOfLives = currentLifePoints + changeAmount;
            currentLifePoints = numberOfLives;
            CreateHealthBar();
        } else {
            for (int i = 0; i < changeAmount; i++) {
                if (currentLifePoints < numberOfLives - 1) {
                    currentLifePoints++;
                    heartBars[currentLifePoints].SetActive(true);
                }
            }
        }
    }

	public void DecreaseHealthBar(int changeAmount) {
        for (int i = 0; i < Mathf.Abs(changeAmount); i++) {
            if (currentLifePoints >= 0) {
                heartBars[currentLifePoints].SetActive(false);
                currentLifePoints--;
            } else {
                currentLifePoints = 0;
            }
        }
	}

	public int ReturnHealth() {
		return numberOfLives;
	}
}