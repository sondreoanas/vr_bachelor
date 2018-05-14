using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCounter : MonoBehaviour {
    /* This script is used for displaying information about:
     *  1. How many boxes there are in the game scene
     *  2. How many boxes that have been placed on the balance board
     *  3. Information that the player should RELEAS THE BOXES WHEN BALANCED -> TO inform that the player has to release the boxes when all the boxes are on the balance board in order to finnish the level. 
     * */

    private TextMesh counter;
    private string information = null;

	// Use this for initialization
	void Start () {
        counter = gameObject.GetComponent<TextMesh>();
		
	}
	
	// Update is called once per frame
	void Update () {
        InformPlayer();
        if (information == null)
        {
            counter.text = "BOX COUNT: " + BalanceBoard.boxesOnBalanceBoard + "/" + BalanceBoard.totalNumberOfBoxes;
        }
        else
        {
            counter.text = information;
        }
    }

    private void InformPlayer()
    {
        if(BalanceBoard.boxesOnBalanceBoard == BalanceBoard.totalNumberOfBoxes)
        {
            information = "RELEASE BOXES WHEN BALANCED";
        }
        else
        {
            information = null;
        }
    }
}
