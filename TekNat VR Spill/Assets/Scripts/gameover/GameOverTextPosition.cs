using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTextPosition : MonoBehaviour {
    private GameObject mainCamera;
    private System.Diagnostics.Stopwatch stopwatch;
    private Vector3 setPos;
    private Quaternion setRot;
    private int showGameOverScreenTime = 7;


    // Use this for initialization
    void Start () {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        GlobalVariables.level = 0;
	}
	
	// Update is called once per frame
	void Update () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            setPos = mainCamera.GetComponent<Transform>().position;
            setRot = mainCamera.GetComponent<Transform>().rotation;
            gameObject.GetComponent<Transform>().position = setPos;
            gameObject.GetComponent<Transform>().rotation = setRot;
        }
        if(stopwatch.Elapsed.Seconds >= showGameOverScreenTime)
        {
            GlobalVariables.InsertNewPlayerScore();
            SceneManager.LoadScene(GlobalVariables.scenes[0]);
            stopwatch.Stop();
            stopwatch.Reset();
        }
        
    }
}
