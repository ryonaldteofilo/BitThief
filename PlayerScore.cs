using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScore
{
    public float highScore;

    public PlayerScore()
    {
        highScore = PlayerPoints.GetHighScore();
    }
}
