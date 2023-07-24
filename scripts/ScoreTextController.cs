using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;
    void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1)
        {
            currentScoreText = null;
            if (PlayerPoints.GetCurrentScore() > PlayerPoints.GetHighScore())
            {
                PlayerPoints.SetHighScore(PlayerPoints.GetCurrentScore());
                SaveSystem.SaveScore();
            }
            PlayerPoints.ResetCurrentScore();
            PlayerScore playerScore = SaveSystem.LoadScore();
            if (playerScore != null)
            {
                highScoreText.text = playerScore.highScore.ToString();
                PlayerPoints.SetHighScore(playerScore.highScore);
            }
            else
            {
                highScoreText.text = "0";
            }
        }

        if(currentSceneIndex == 4)
        {
            PlayerPoints.ResetCurrentScore();
        }

        if(currentSceneIndex == 5 || currentSceneIndex == 6)
        {
            if (PlayerPoints.GetCurrentScore() > PlayerPoints.GetHighScore())
            {
                PlayerPoints.SetHighScore(PlayerPoints.GetCurrentScore());
                SaveSystem.SaveScore();
            }
            PlayerScore playerScore = SaveSystem.LoadScore();
            if (playerScore != null)
            {
                highScoreText.text = playerScore.highScore.ToString();
            }
            else
            {
                highScoreText.text = "0";
            }
            currentScoreText.text = PlayerPoints.GetCurrentScore().ToString();
        }
    }

}
