using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

    private Text scoreText;
    private int score;
    // Use this for initialization

    void Start () {
        score = GlobalVariables.score;
        scoreText = GetComponent<Text>();
        scoreText.text = "DU FIKK " + score.ToString()+ " POENG";
        print(scoreText.text);
	}
	
	// Update is called once per frame
	void Update () {
    
    }
}
