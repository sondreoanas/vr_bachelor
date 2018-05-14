using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimerScript : MonoBehaviour {
    
    private static System.Timers.Timer gameCountdownTimer;
    private int RemainingGameTime = 59;
    public static bool TimeIsOut = false;
    private Stopwatch stopWatch;
    private TextMesh tm;
    // Use this for initialization
    void Start()
    {
        tm = gameObject.GetComponent<TextMesh>();
        stopWatch = new Stopwatch();
        stopWatch.Start();
        // Get the elapsed time as a TimeSpan value.
        TimeSpan ts = stopWatch.Elapsed;
    }
    // Update is called once per frame
    void Update()
    {
        TimeSpan ts = stopWatch.Elapsed;
        tm.text = getRemainingGameTime(ts.Seconds).ToString() + "s to go";
        //print("Tid igjen er : "+getRemainingGameTime(ts.Seconds)+"sec");
    }

    private int getRemainingGameTime(int SecondsPassed)
    {
        if (SecondsPassed < this.RemainingGameTime)
        {
            return this.RemainingGameTime - SecondsPassed;
        }
        else
        {
            stopWatch.Stop();
            TimeIsOut = true;
            return 0;
        }
    }
}
