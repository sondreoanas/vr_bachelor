using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSensor : MonoBehaviour {
    /* Tenkt mekanikk -> Dette skriptet kobles til endesensorer / danger bokser på bordet slik at de detekterer om brett berører bord/ danger box.
     * Dette gjør at spillet kan avgjøre om balancebrettet er i godkjent posisjon. CollisionEnter og CollisionExit brukes for å avgjøre om balancebrett 
     * berører en danger box. (Danger box har DANGER skrift i spillet). Hvis balanceboard berører danger box så settes IsBallanceBoardCollidingWithDangerBox = true. 
     * IsBallanceBoardCollidingWithDangerBox blir hentet inn i hovedskriptet og sjekket i forbindelse med andre spillavgjørende variabler for å avgjøre om 
     * spilleren har fullført spillet/ klart å stable boksene innenfor de reglene som har blitt satt i spillet  */


    public static bool IsBallanceBoardCollidingWithDangerBox = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "balanceBoard")
        {
            IsBallanceBoardCollidingWithDangerBox = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "balanceBoard")
        {
            IsBallanceBoardCollidingWithDangerBox = false;
        }
    }
}
