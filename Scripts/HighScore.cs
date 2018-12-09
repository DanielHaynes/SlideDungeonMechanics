using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(InputField))]
public class HighScore : MonoBehaviour
{
    const int HIGH_SCORE_LIMIT = 8;

    [SerializeField] private string HighScoreBoardPrompt = "Add Highscore";
    [SerializeField] private string HighScoreBoardName = "Highscores";
    [SerializeField] Text HighscoreBoardText;
    [SerializeField] [Range(1, 20)] private int inputCharacterLimit = 12;

    public InputField mainInputField;
    int newScoreToRecord;
    [SerializeField] Text[] highScoresText;

    private void Start() {
        HighscoreBoardText.text = HighScoreBoardPrompt;
        mainInputField.characterLimit = inputCharacterLimit;
        TouchScreenKeyboard.hideInput = true;
        transform.localPosition = new Vector2(-2000f, -2000f);
    }

    public void CreateDisplayHighscore(int score) {
        transform.localPosition = new Vector2(0f, 0f);
        SaveLoad.LoadHighScores();

        newScoreToRecord = score;
        foreach (Text t in highScoresText) {
            t.text = "";
        }
    }

    public void SubmitName() {
        print(mainInputField.text + " with a score of " + newScoreToRecord);
        HighScoreData newScore = new HighScoreData(newScoreToRecord, mainInputField.text);
        
        mainInputField.enabled = false;
        mainInputField.gameObject.SetActive(false);

        ApplyHighScore(newScore);
    }

    void ApplyHighScore(HighScoreData scoreData) {
        if (SaveLoad.highScores.Count < HIGH_SCORE_LIMIT) {
            SaveLoad.SaveHighScore(scoreData);
        } else {
            SaveLoad.highScores.Sort(CompareByEffectLayer);
            if (SaveLoad.highScores[SaveLoad.highScores.Count-1].GetScoreValue() < scoreData.GetScoreValue()) {
                SaveLoad.highScores.RemoveAt(SaveLoad.highScores.Count - 1);
                SaveLoad.SaveHighScore(scoreData);
            }
        }
        DisplayHighScores();
    }

    void DisplayHighScores() {
        HighscoreBoardText.text = HighScoreBoardName;
        SaveLoad.highScores.Sort(CompareByEffectLayer);

        int scoreCounter = 0;
        foreach (HighScoreData score in SaveLoad.highScores) {
            highScoresText[scoreCounter].text = score.GetScoreName() + " $" + score.GetScoreValue();
            scoreCounter++;
        }

        GameObject.FindObjectOfType<UIManager>().EnableGameRestart();
    }

    public static int CompareByEffectLayer(HighScoreData score1, HighScoreData score2) {
        if (score1.GetScoreValue() == score2.GetScoreValue()) {
            return score1.GetScoreValue().CompareTo(score2.GetScoreValue());
        } else {
            return score2.GetScoreValue() - score1.GetScoreValue();
        }
    }
}