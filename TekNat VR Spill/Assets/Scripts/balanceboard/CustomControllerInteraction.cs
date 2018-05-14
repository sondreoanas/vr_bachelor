using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using VRTK;

public class CustomControllerInteraction : MonoBehaviour {
    VRTK_InteractTouch controller;
    private string controllerTag;
    public static bool controller1Touching = false;
    public static bool controller2Touching = false;

    // Use this for initialization
    void Start () {
        controllerTag = gameObject.tag;
        if(controllerTag == "Untagged")
        {
            throw new Exception("you will need to set controllertags on the controller. use: controller1 and controller2");
        }
        controller = gameObject.GetComponent<VRTK_InteractTouch>(); 
    }

    // Update is called once per frame
    void Update () {
        if (controller.GetTouchedObject())
        {
            if (controller.GetTouchedObject().tag == "box")
            {
                ControllerTouching(controllerTag);
            }
        }
        else
        {
            ControllerNotTouching(controllerTag);
        }
    }

    private void ControllerTouching(string tag)
    {
        if (tag == "controller1")
        {
            controller1Touching = true;
        }
        else if(tag == "controller2")
        {
            controller2Touching = true;
        }
    }

    private void ControllerNotTouching(string tag)
    {
        if (tag == "controller1")
        {
            controller1Touching = false;
        }
        else if (tag == "controller2")
        {
            controller2Touching = false;
        }
    }
}
