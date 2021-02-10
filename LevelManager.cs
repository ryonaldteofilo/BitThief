using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int splashScreenWaitTime = 3;
    [SerializeField] int crossfadeTransitionTime = 1;
    [SerializeField] int circlewipeTransitionTime = 1;
    [SerializeField] Animator crossfade;
    [SerializeField] Animator circlewipe;
    static int prevLevelIndex;

    private void Start()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentBuildIndex == 0)
        {
            StartCoroutine(SplashtoStart());
        }
    }
    IEnumerator CircleWipeTransition(string sceneName)
    {
        circlewipe.SetTrigger("startTrigger");
        yield return new WaitForSeconds(circlewipeTransitionTime);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator CircleWipeTransitionWithIndex(int sceneIndex)
    {
        circlewipe.SetTrigger("startTrigger");
        yield return new WaitForSeconds(circlewipeTransitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator CrossfadeTransition(string sceneName)
    {
        crossfade.SetTrigger("startTrigger");
        yield return new WaitForSeconds(crossfadeTransitionTime);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator SplashtoStart()
    {
        yield return new WaitForSeconds(splashScreenWaitTime);
        SplashtoStartMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        StartCoroutine(CircleWipeTransition("Level 1"));
    }

    public void Settings()
    {
        StartCoroutine(CircleWipeTransition("Settings Scene"));
    }

    public void StartMenu()
    {
        StartCoroutine(CircleWipeTransition("Start Scene"));
    }

    public void LoseScreen()
    {
        StartCoroutine(CircleWipeTransition("Lose Scene"));
    }

    public void SuccessScreen()
    {
        prevLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(prevLevelIndex);
        if (prevLevelIndex == 9)
        {
            StartCoroutine(CircleWipeTransition("Finish Scene"));
        }
        else
        {
            StartCoroutine(CircleWipeTransition("Success Scene"));
        }
    }

    public void SplashtoStartMenu()
    {
        StartCoroutine(CrossfadeTransition("Start Scene"));
    }

    public void ControlsScreen()
    {
        StartCoroutine(CircleWipeTransition("Controls Scene"));
    }

    public void Continue()
    {
        int test = prevLevelIndex + 1; 
        StartCoroutine(CircleWipeTransitionWithIndex(test));
    }
}
