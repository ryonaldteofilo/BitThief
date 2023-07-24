using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static BinaryFormatter binaryFormatter = new BinaryFormatter();
    static string path = Application.persistentDataPath + "/score.test";

    public static void SaveScore()
    {
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerScore playerScore = new PlayerScore();
        binaryFormatter.Serialize(stream, playerScore);
        stream.Close();
    }

    public static PlayerScore LoadScore()
    {
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerScore playerScore = binaryFormatter.Deserialize(stream) as PlayerScore;
            stream.Close();
            return playerScore;
        }
        else
        {
            Debug.LogError("No Saved File Found");
            return null;
        }
    }
}
