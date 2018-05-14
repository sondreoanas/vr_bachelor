using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalanceBoard : MonoBehaviour
{
    /* This script is used to determine if the player has balanced all the boxes onto the balance board, 
     * check that the angle of the balance board is within allowed values 
     * and check that the player is not touching the boxes before the next level can load. 
     * */

    public static int totalNumberOfBoxes = 0;
    private GameObject[] boxes;
    private bool IsAllBoxesOnBalanceBoard = false;
    public static int boxesOnBalanceBoard = 0;
    

    private static bool GameOver { get; set; }
    private Stopwatch stopwatch;
    private int timeToWin = 3;
    private bool freezePostionOfBoard = true; // Used because the physics engine wants to rotate the board even thought there are no boxes present on the board. 

    private Rigidbody rigidbodyOfBoard;

    private DBcurScoreCom DBRef;

    // Use this for initialization
    void Start()
    {
        GameObject DBRefObject = GameObject.FindWithTag("DBRef");
        if (DBRefObject != null)
        {
            DBRef = DBRefObject.GetComponent<DBcurScoreCom>();
        }
        stopwatch = new Stopwatch();
        stopwatch.Start();
        rigidbodyOfBoard = gameObject.GetComponent<Rigidbody>();
        freezePostionOfBoard = true;
        boxes = GameObject.FindGameObjectsWithTag("box");
        totalNumberOfBoxes = boxes.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(freezePostionOfBoard)
        {
            rigidbodyOfBoard.useGravity = false;
           
        }else{
            rigidbodyOfBoard.useGravity = true;
        }

        CheckBooleanValues();
    }
 

    private void CheckBooleanValues()
    {
        if (DidPlayerBalanceAllTheBoxesOntoTheBoard() && !IsControllerTouching() && !IsBoardCollidingWithDangerBox())
        {
            if (stopwatch.Elapsed.TotalSeconds > timeToWin)
            {
                LoadNextLevel();
            }
        }
        else
        {
            stopwatch.Reset();
            stopwatch.Start();
        }
    }

    private void LoadNextLevel()
    { 
        GlobalVariables.timeToPlay = GlobalVariables.timeNow;
        GlobalVariables.score += 100;
        DBRef.addScore(GlobalVariables.score);
        GlobalVariables.level++;
        UnityEngine.Debug.Log(GlobalVariables.scenes[GlobalVariables.level]);
        SceneManager.LoadScene(GlobalVariables.scenes[GlobalVariables.level]);
    }

    private bool IsControllerTouching()
    {
        if (CustomControllerInteraction.controller1Touching || CustomControllerInteraction.controller2Touching)
        {
            freezePostionOfBoard = false;
            return true;
        }
        return false;
    }

    private bool DidPlayerBalanceAllTheBoxesOntoTheBoard()
    {
        if(boxes != null)
        {
            int numberOfBoxes = boxes.Length;
            int numberOfBoxesTouchingBalanceBoard = 0;
            for (int i = 0; i < boxes.Length; i++)
            {
                Box box = boxes[i].gameObject.GetComponent<Box>();
                if (box.BoxTouchingBoard)
                {
                    numberOfBoxesTouchingBalanceBoard++;
                }
            }
            boxesOnBalanceBoard = numberOfBoxesTouchingBalanceBoard;
            if (numberOfBoxes == numberOfBoxesTouchingBalanceBoard)
            {
                print("alle boksene er på plass");
                return true;
            }
        }
        return false;
    }

    private bool IsBoardCollidingWithDangerBox()
    {
        if (DangerSensor.IsBallanceBoardCollidingWithDangerBox)
        {
            return true;
        }
        return false;
    }
}
