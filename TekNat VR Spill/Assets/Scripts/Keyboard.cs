using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {
    private string playerName;
    private GameObject textObject;
    private GameObject keyBoardObject;
    //private GameObject playerInformation; info om tid og navn før spillet starter
    private GameObject startButtonObject;
    private DBcurScoreCom DBRef;
    
	// Use this for initialization
	void Start () {
        GameObject DBRefObject = GameObject.FindWithTag("DBRef");
        if (DBRefObject != null)
        {
            DBRef = DBRefObject.GetComponent<DBcurScoreCom>();

        }
        textObject = GameObject.FindGameObjectWithTag("NameOfPlayer");
        keyBoardObject = GameObject.FindGameObjectWithTag("Keyboard");
        //playerInformation = GameObject.FindGameObjectWithTag("Navn_informasjon_tid");
        startButtonObject = GameObject.FindGameObjectWithTag("StartButton");
        startButtonObject.SetActive(false);
    }
    public void Enter_Onclick()
    {
        print(textObject.GetComponent<Text>().text);
        playerName = textObject.GetComponent<Text>().text;
        GlobalVariables.name = playerName;
        DBRef.addScore(0);
        //playerInformation.GetComponent<Text>().text = "Hei " + playerName + ", du har " + GlobalVariables.timeToPlay.ToString() + "sekunder på å komme så langt du kan. Trykk på startknappen når du er klar";
        //print(playerInformation.GetComponent<Text>().text);
        //playerInformation.SetActive(true);
        keyBoardObject.SetActive(false);
        startButtonObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

       // print(textObject.GetComponent<Text>().text);
    }
}
