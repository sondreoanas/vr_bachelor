using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BG_Button : MonoBehaviour {

    private bool buttonClicked = false;
    private float playerTime;
    private int buttonValue;
    private Button button_1;
    private Button button_2;
    private Button button_3;
    private List<int> altListButton;
    private int num_blocks;
    public Material victorySkyBox;
    public Material defeatSkyBox;
    private DBcurScoreCom DBRef;

    private void Start()
    {
        GameObject DBRefObject = GameObject.FindWithTag("DBRef");
        if (DBRefObject != null)
        {
            DBRef = DBRefObject.GetComponent<DBcurScoreCom>();

        }
    }

    public void AltLoader(List<int> altList) {

        button_1 = GameObject.Find("Button_1").GetComponent<Button>();
        button_2 = GameObject.Find("Button_2").GetComponent<Button>();
        button_3 = GameObject.Find("Button_3").GetComponent<Button>();
        List<Button> butnList = new List<Button> { button_1, button_2, button_3 };

        num_blocks = altList[0];

        ListShuffeler.Shuffle(altList);
        altListButton = altList;

        for (int i = 0; i < 3; i++) butnList[i].GetComponentInChildren<Text>().text = altList[i].ToString();
    }

    //Called when one of the 3 button alternatives are called
    public void OnClick(string buttonName)
    {
        // EventSystem.current.currentSelectedGameObject returns null so a more static aproach is implemented ie. buttonName 
        // hardcoded in the buttons...
        //Debug.Log(EventSystem.current); // returns current eventsystem
        //Debug.Log(EventSystem.current.currentSelectedGameObject); // returns null for some reason

        if (!buttonClicked && !GlobalVariables.gameOver)
        {
            GlobalVariables.timeToPlay = GlobalVariables.timeNow;
            buttonClicked = true;

            if (buttonName == "Button_1") buttonValue = altListButton[0];
            if (buttonName == "Button_2") buttonValue = altListButton[1];
            if (buttonName == "Button_3") buttonValue = altListButton[2];

            if (buttonValue == num_blocks)
            {
                GlobalVariables.score += 100;
                DBRef.addScore(GlobalVariables.score);
                GlobalVariables.level++;
                GlobalVariables.BGLvl++;
                if (GlobalVariables.level < 11)
                {
                    SceneManager.LoadScene(GlobalVariables.scenes[GlobalVariables.level]);
                } else
                {
                    SceneManager.LoadScene("Box Guesser");
                }
            }
            else
            {
                GlobalVariables.level++;
                GlobalVariables.BGLvl++;
                if (GlobalVariables.level < 11)
                {
                    SceneManager.LoadScene(GlobalVariables.scenes[GlobalVariables.level]);
                } else
                {
                    SceneManager.LoadScene("Box Guesser");
                }
            }
        }
    }
}
