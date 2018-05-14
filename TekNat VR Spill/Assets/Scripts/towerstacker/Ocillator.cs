using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[DisallowMultipleComponent]
public class Ocillator : MonoBehaviour
{

    //Felter i Unity-Editor
    [SerializeField] Vector3 movementVector = new Vector3(5f, 5f, 5f);
    [SerializeField] float period = 2f;
    [SerializeField] int WinHeight = 6;
    [SerializeField] GameObject Box;
    [SerializeField] GameObject ReferenceBox;
    [SerializeField] GameObject Platform;
    [SerializeField] TextMesh text;
    [SerializeField] GameObject timerTextObject;
    [SerializeField] GameObject cameraAdjuster;
    [SerializeField] GameObject elevatorFloor;
    [SerializeField] GameObject tutorial;

    //Objekt og posisjon
    GameObject newBox;
    Rigidbody rb;
    float movementFactor;
    private Vector3 currentPosition;
    private Vector3 startPos;
    private float speed;
    private float heightThreshold = 3f;

    private float height = 1.5f;
    private int highscore;
    private float gameTimer = 100;
    List<GameObject> boxes = new List<GameObject>();
    private float maxHeight;
    private bool stable = false;
    private Vector3 heightIncrease = new Vector3(0, 2, 0);


    //Klokker
    private Stopwatch timer;
    private Stopwatch moveTimer;
    public Stopwatch progressTimer;
    private TimeSpan currentts;
    private TimeSpan previousts;
    private TimeSpan moveTime;

    //Levelparametere
    private static int counter = 0;

    //Starten av spillet
    private bool firstBox = true;

    private TimeSpan progressTime;
    private float movingBoxHeight;
    private int collisionCounter;
    private float cameraHeight = 6;
    private Vector3 newCameraHeight = new Vector3(0, 3, 0);
    private Vector3 newFloorHeight = new Vector3(0, 2, 0);
    private Vector3 destination;
    private Vector3 velocity = new Vector3(0, 0, 0);
    private Vector3 difference;
    private Vector3 floorDestination = new Vector3(0, 1, 0);

    private int currentTallest;

    private float smoothTime = 1f;
    private bool scored = false;
    private int bestHeight;

    private DBcurScoreCom DBRef;
    private bool destroyed = false;




    //Starter klokker og databasereferanse
    void Start()
    {

        GlobalVariables.level++;
        GameObject DBRefObject = GameObject.FindWithTag("DBRef");
        if (DBRefObject != null)
        {
            DBRef = DBRefObject.GetComponent<DBcurScoreCom>();
        }
        // ***
        //boxSound = GetComponent<AudioSource>();
        timer = new Stopwatch();
        moveTimer = new Stopwatch();
        timer.Start();
        startPos = transform.position;

    }


    // Update is called once per frame
    void Update()
    {
        Ocillation();
        Moving();
        BoxHeight();
        CameraAdjust();
        BestHeight();
        CurrentHeight();
    }

    //Sjekker om en boks er høyere enn tårnet.
    private void CurrentHeight()
    {
        currentTallest = 0;
        foreach(GameObject box in boxes)
        {
            if (box.transform.position.y > currentTallest)
            {
                currentTallest = (int)box.transform.position.y;
            }
           
        }
    }

    //Sjekker om tårnet har nådd målhøyden.
    private void BestHeight()
    {
       if(!destroyed)
            foreach (GameObject boxiterator in boxes)
            {
                if (boxiterator.transform.position.y > bestHeight && stable)
                {
                    bestHeight = (int)boxiterator.transform.position.y;
                    if (boxiterator.transform.position.y > WinHeight)
                    {
                        GlobalVariables.score += 100;
                        DBRef.addScore(GlobalVariables.score);
                        GlobalVariables.timeToPlay = GlobalVariables.timeNow;
                        SceneManager.LoadScene(GlobalVariables.scenes[GlobalVariables.level]);
                    }
                }
            }
            if (!firstBox)
            {
                text.text = "Høyde: " + (int)newBox.transform.position.y + "\n" + "Målhøyde: " + WinHeight;
            }
        
    }



    //Legger til poeng basert på høyden
    private void ScoreHeight()
    {
        if (!scored)
        {
            GlobalVariables.score += 20;
            DBRef.addScore(GlobalVariables.score);
            scored = true; 
        }
    }

    //Øker høyden på den bevegende boksen, slik at ikke tårnet blir stablet
    //høyere enn den boksen man slipper fra.
    private void BoxHeight()
    {
        if (speed < 0.1)
        {
            foreach (GameObject box in boxes)
            {
                if(box.transform.position.y >= Box.transform.position.y - height)
                {
                    startPos = new Vector3(startPos.x, (startPos.y + heightIncrease.y), startPos.z);
                    stable = false;
                    break;
                } else if(currentTallest < Box.transform.position.y - 4)
                {
                    startPos = new Vector3(startPos.x, (startPos.y - heightIncrease.y), startPos.z);
                    stable = false;
                    break;
                }
                if(box.transform.position.y < 0)
                {
                    Destroy(box);
                    boxes.Remove(box);
                    destroyed = true;
                }
                
              
            }
        }
    }

    //Flytter spillerkamera høyere slik at dersom en stabler høyt nok blir
    //ikke boksen man slipper fra for langt unna, bakken følger etter kameraet som ett
    //eget objekt
    private void CameraAdjust()
    {
        if (Box.transform.position.y > 8)
        {
            destination = (cameraAdjuster.transform.position + newCameraHeight);
            floorDestination = (elevatorFloor.transform.position + newFloorHeight);
            velocity = new Vector3(0, 0.2f, 0);
            difference = new Vector3(0, 1, 0);
            cameraAdjuster.transform.position = Vector3.SmoothDamp(cameraAdjuster.transform.position, destination, ref velocity, smoothTime);
            elevatorFloor.transform.position = Vector3.SmoothDamp(cameraAdjuster.transform.position, floorDestination, ref velocity, smoothTime);
        }
    }


    //Sjekker om bokser som faller enda faller
    private void Moving()
    {
        if (!firstBox)
        {
            TimeStart();
            StableCondition();
        }
    }

    //Sjekker fart/stabilitet av boks i lufta
    //Må bruke timer da en ny boks som blir sluppet starter i ro
    private void StableCondition()
    {
        if (!destroyed)
        {
            speed = rb.velocity.magnitude;
            if (speed < 0.08 && moveTime.Seconds >= 3)
            {
                stable = true;
                TimerReset();
            }
        }
        
    }

    //Resetter stopwatch etter fart og tid sjekk
    private void TimerReset()
    {
        moveTimer.Stop();
        moveTimer.Reset();
        moveTime = moveTimer.Elapsed;

    }

    //Starter tid og setter stopwatch tid for å unngå speed = 0 ved init av ny boks
    private void TimeStart()
    {
        moveTimer.Start();
        moveTime = moveTimer.Elapsed;
        if (moveTime.Seconds > 3)
        {
            moveTimer.Reset();

        }
    }

    //Periodisk bevegelse av boksen
    private void Ocillation()
    {

        if (period <= Mathf.Epsilon) { return; }
        float cycle = Time.time / period;

        const float tau = Mathf.PI * 2; // 6.2
        float rawSinWave = Mathf.Sin(cycle * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;

    }


    //Handler kollisjon med gulvet
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor")
        {
            gameObject.tag = "dead";
            collisionCounter++;
            GlobalVariables.score -= 50;
            
        }

        if (collision.gameObject.tag == "dead")
        {
            gameObject.tag = "dead";
        }
    }

    //Fjerner bokser ifra gulvet som ikke har truffet platformen
   

    //Gammel funksjon for GameOver. IKKE I BRUK
    private void GameOver()
    {
        GlobalVariables.level++;
        SceneManager.LoadScene(GlobalVariables.scenes[GlobalVariables.level]);
    }

    //Logikk for triggerpress, tanken her er at man ikke skal kunne trykke for mange ganger
    //for fort, både for spillflyten, men det kan oppstå bugs pga ett tastetrykk kan oppfattes som flere
    //Sjekker derfor tid mellom hvert trykk
    public void TriggerPressLogic()
    {
        firstBox = false;
        currentts = timer.Elapsed;
        if (previousts.Seconds == 0)
        {
            timer.Start();
            currentPosition = transform.position;
            DropBox();
        }

        else if (currentts.Seconds > previousts.Seconds + 0.5f)
        {
            if (currentts.Seconds > previousts.Seconds)
            {
                currentPosition = transform.position;
                DropBox();
            }
            else if (previousts.Seconds > currentts.Seconds)
            {
                currentPosition = transform.position;
                DropBox();
            }
        }
        previousts = currentts;

    }

    //Slipper en ny boks som en instans av referanseboksen ut ifra den bevegende boksen. Lagrer alle rigidbodies i en liste. Flere boolske variabler endrer state.
    private void DropBox()
    {
        stable = false; //Referansen til den sluppne boksen kan ikke være stabil i starten
        newBox = (GameObject)Instantiate(ReferenceBox, currentPosition, Quaternion.identity);   //Instansierer ny boks ifra posisjonen til den bevegende i boksen
        rb = newBox.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,-2f,0);
        firstBox = false;   //Vi har ingen referanse til en boks som har blitt sluppet i starten av spillet
        boxes.Add(newBox);  //Legger alle bokser inn i en liste slik at vi kan sjekke høyden til alle 
        destroyed = false;
        scored = false;

    }
}

