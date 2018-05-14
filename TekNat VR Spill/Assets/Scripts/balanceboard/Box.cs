using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    /* Dette scriptet festes til alle boksene som skal balanserer på balanceboard. Alle boksene vil sjekkes om de har kollidert med 
     * balanceBoard. For hver gang en box kolliderer med balanceBoard så vil vi sette at akkurat denne boksen har kollidert med
     * balanceboard. BoxesTouchingBalnceBoard blir satt til false når spillet starter. Når en box kolliderer med balanceBoard så blir dens currentBoxOnBoard variable satt til true
     * En global variabel boxesTouchingBoard brukes til å detektere om alle boksene er lagt på balanceboard. Måten dette blir gjort på er at alle boksene kan endre denne variabelen 
     * basert på deres nåværende og forrige tilstand som vi lagrer i to forskjellige variabler: currentBoxOnBoard og boxOnBoardPrevious. Fordi BoxesTouchingBoard er statisk så 
     * kan den endres av alle boksene som er med i spillet. Målet med denne fremgangsmåten er å sette BoxesTouchingBoard til false så lenge en eller flere av boksene ikke ligger på balanceBoard.
     * koden skal også fungere selv om en boks blir lagt på balanceBoard og deretter tatt av balanceBoard igjen.*/
    

    public bool BoxTouchingBoard;
   
	// Use this for initialization
	void Start () {
        BoxTouchingBoard = false;
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "boxdetector")
        {
            BoxTouchingBoard = true;

        }
      
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "boxdetector")
        {
            BoxTouchingBoard = false;

        }
    }
}
