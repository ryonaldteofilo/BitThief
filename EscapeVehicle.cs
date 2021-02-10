using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscapeVehicle : MonoBehaviour
{
    [SerializeField] float minimumPoints = 0;
    [SerializeField] TextMeshProUGUI minimumPointsText;
    bool checkForInput = false;

    private void Start()
    {
        minimumPointsText.text = minimumPoints.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && checkForInput)
        {
            PlayerPoints playerPoints = FindObjectOfType<PlayerPoints>();
            float totalPoints = playerPoints.GetCurrentLevelScore();
            if (totalPoints >= minimumPoints)
            {
                    LevelManager levelManager = FindObjectOfType<LevelManager>();
                    levelManager.SuccessScreen();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPoints playerPoints = collision.GetComponent<PlayerPoints>();
        if (playerPoints)
        {
            checkForInput = true;
        }
        else checkForInput = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        checkForInput = false;
    }
}
