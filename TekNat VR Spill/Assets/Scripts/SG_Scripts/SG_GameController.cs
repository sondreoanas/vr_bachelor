using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SG_GameController : MonoBehaviour {
    public ParticleSystem particles;
    public GameObject scoreboard;

    public void TestButton_OnClick()
    {
        if (particles.isEmitting) { particles.Stop(); }
        else { particles.Play(); }
    }

    public void StartGame_Onclick()
    {
        SceneManager.LoadScene(GlobalVariables.scenes[1]);
    }

	void Start () {
        scoreboard = GameObject.FindGameObjectWithTag("ScoreBoard");
        GlobalVariables.level = 1;
        GlobalVariables.BGLvl = 1;
        GlobalVariables.gameOver = false;
        GlobalVariables.score = 0;
        GlobalVariables.timeToPlay = 120;
        GlobalVariables.InsertSomePlayersOnScoreBoard();
        scoreboard.GetComponent<Text>().text = GlobalVariables.PlayerScoreToString();
	}
}
