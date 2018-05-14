using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour {
    private GameObject movingObject;
    private Vector3 pos;
    // Use this for initialization
	void Start () {
        movingObject = gameObject;

	}
	
	// Update is called once per frame
	void Update () {
        MoveInZDirection(movingObject);

    }

    private void MoveInZDirection(GameObject o)
    {
        o.GetComponent<Transform>().position += Vector3.back * 0.001F;
    }
}
