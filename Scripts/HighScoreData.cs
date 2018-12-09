using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreData
{
    int highScore;
    string highScoreName;
    public HighScoreData(int score, string name) {
        highScore = score;
        highScoreName = name;
    }

    public int GetScoreValue() {
        return highScore;
    }

    public string GetScoreName() {
        return highScoreName;
    }
}