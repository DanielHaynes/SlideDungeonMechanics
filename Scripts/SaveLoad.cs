using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {
    public static List<HighScoreData> highScores = new List<HighScoreData>();

    //it's static so we can call it from anywhere
    public static void SaveHighScore(HighScoreData newHighScore) {
        SaveLoad.highScores.Add(newHighScore);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/highscores.gd"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.highScores);
        file.Close();
    }
    /// <summary>
    /// WARNING!!
    /// Should be called by a direct user request only
    /// </summary>
    public static void ClearHighScores() {
        SaveLoad.highScores.Clear();
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/highscores.gd"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.highScores);
        file.Close();
    }
	
	public static void LoadHighScores() {
		if (File.Exists(Application.persistentDataPath + "/highscores.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/highscores.gd", FileMode.Open);
            SaveLoad.highScores = (List<HighScoreData>)bf.Deserialize(file);
            file.Close();
		}
	}
}
