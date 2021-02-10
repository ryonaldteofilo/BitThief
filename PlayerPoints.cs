using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPoints : MonoBehaviour
{
    [SerializeField] float currentLevelScore;
    static float currentScore;
    static float highScore;
    [SerializeField] AudioClip collectItemSFX;
    float SFXvolume;
    [SerializeField] TextMeshProUGUI pointsText;
    Collectables targetCollectable;
    bool isNearCollectable;

    private void Start()
    {
        currentLevelScore = 0f;
        pointsText.text = currentLevelScore.ToString();
        SFXvolume = PlayerPrefsController.GetGameVolume();
    }

    private void Update()
    {
        if(isNearCollectable)
        { 
            if(Input.GetKeyDown(KeyCode.E))
            {
                AddPoints(targetCollectable.GetPoints());
                AudioSource.PlayClipAtPoint(collectItemSFX, Camera.main.transform.position, SFXvolume);
                Destroy(targetCollectable.gameObject);
                Debug.Log("totalpoints = " + currentLevelScore);
            }
        }
    }

    public void AddPoints(float points)
    {
        currentLevelScore += points;
        currentScore += points;
        pointsText.text = currentLevelScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        targetCollectable = other.GetComponent<Collectables>();
        if(targetCollectable != null)
        {
            isNearCollectable = true;
        }
        else
        {
            isNearCollectable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isNearCollectable = false;
        targetCollectable = null;
    }

    public float GetCurrentLevelScore()
    {
        return currentLevelScore;
    }

    public static float GetHighScore()
    {
        return highScore;
    }

    public static void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public static float GetCurrentScore()
    {
        return currentScore;
    }

    public static void SetHighScore(float inputScore)
    {
        highScore = inputScore;
    }

}
