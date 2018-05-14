using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    private float playerTime;
    private float timeLeft;
    public TextMesh timerText;
    public Material victorySkyBox;
    public Material defeatSkyBox;


    void Start()
    {
        GlobalVariables.gameOver = false;
        timeLeft = GlobalVariables.timeToPlay;
    }

    public void DisplayScores(int gameScore, int totalScore)
    {
        timerText.text = "game score: " + gameScore + "\n total score: " + totalScore;
    }

    IEnumerator LoadAfterDelay(string levelName)
    {
        Debug.Log("Loading new scene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }


    void Update () {

        if (timeLeft < 0 )
        {
            GlobalVariables.gameOver = true;
            if (defeatSkyBox != null) RenderSettings.skybox = defeatSkyBox;
            timerText.text = "Tiden er ute!";
            StartCoroutine(LoadAfterDelay("GameOverScreen"));
        }
        else
        {
            timeLeft -= Time.deltaTime;
            GlobalVariables.timeNow = timeLeft;
            timerText.text = timeLeft.ToString("N2");
        }

    }
}
