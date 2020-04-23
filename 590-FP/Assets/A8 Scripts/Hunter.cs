using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{


// WHEN MAKING ANY WALLS (INVISIBLE OR NOT) MAKE SURE TO SET TAG TO "INVISIBLEWALL" TO MAKE SOUNDS WORK
// WHEN MAKING ANY FLOORS MAKE SURE TO SET TAG TO "HARDWOOD" FLOOR


// numberOfLines is message on top left
// scoreMessage is message on top right
// winMessage appears afer you collect 5 objects
// startMessage is the message at beginning telling to read to the sign and hit the s key to start

    public GameObject winMessageObject;
    public GameObject scoreMessageObject;
    public GameObject numberOfLinesObject;
    public GameObject startMessageObject;
    public TextMesh numberOfLinesMessage;
    public TextMesh winMessage;
    public TextMesh scoreMessage;
    public GameObject line;

    public Camera oculusCam;

    private int numberOfObjectsCollected;
    private int numOfLines;
    private float timeElapsed;

// some hacky variables
    private bool notDisplayedYet;
    private bool started;


 
    void Start()
    {

        GameObject hunter = GameObject.Find("First Player");
        numberOfObjectsCollected = 0;
        numOfLines = 0;
        

        scoreMessage.text = "# of Objects Collected: " + numberOfObjectsCollected;
        numberOfLinesMessage.text = "Number of Steps: " + numOfLines/20 + "\nTime Elapsed: " + timeElapsed.ToString("N1");

        winMessage.text = "You Win!";
        winMessageObject.SetActive(false);

        timeElapsed += Time.deltaTime;
        notDisplayedYet = true;
        started = false;


        
        
    }

    
    void Update() {

// SETTING CURSOR TO NOT VISIBLE BECAUSE BALL SHOOTS BASED ON WHERE YOU LOOK, NOT WHERE YOUR CURSOR IS
    Cursor.visible = false;
      

// HIT THE S KEY TO START (WON'T KEEP TRACK OF STEPS OR START TIME UNTIL USER HAS READ THE SIGN)
    if (Input.GetKeyDown(KeyCode.S)) {
        started = true;
        startMessageObject.SetActive(false);
    }


    if(started) {
        timeElapsed += Time.deltaTime;
        numberOfLinesMessage.text = "Number of Steps: " + numOfLines/20 + "\nTime Elapsed: " + timeElapsed.ToString("N1");

       


// CHECK TO SEE IF USER HAS COLLECTED 10 OBJECTS
    if (numberOfObjectsCollected >= 5 && notDisplayedYet) {
        winMessage.text = "10 Objects Collected\nFinal # of Steps Taken: " + numOfLines/20 + "\nFinal Time Elapsed: " + timeElapsed.ToString("N1");
        winMessageObject.SetActive(true);
        notDisplayedYet = false;
        numberOfLinesObject.SetActive(false);
        scoreMessageObject.SetActive(false);
    }



// RAYCAST TO COLLECT COLLECTIBLES 
    RaycastHit hit;
        if (Physics.Raycast(oculusCam.transform.position, oculusCam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity )) {

            GameObject hitObject = hit.collider.gameObject;
            CollectibleTreasure c = hitObject.GetComponent<CollectibleTreasure>();

             float distanceBetweenHunterAndSphere = (transform.position - hitObject.transform.position).magnitude;
         
            if(c && distanceBetweenHunterAndSphere < 10.0f) {
              Debug.DrawRay(oculusCam.transform.position, oculusCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            } else {
                Debug.DrawRay(oculusCam.transform.position, oculusCam.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
                
   
        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
       
        }

// MAKES THE LINE THAT FOLLOWS THE USER AS THEY WALK
        if(Input.GetButton("Vertical")) {
            Instantiate(line, transform.position, transform.rotation);
            numOfLines++;
        }

          if(Input.GetButton("Horizontal")) {
            Instantiate(line, transform.position, transform.rotation);
            numOfLines++;
        }

// HIT ENTER KEY TO COLLECT COLLECTILBE
        if (Input.GetKeyDown(KeyCode.Return)) {

            Debug.Log("_____________Key Pressed: enter");
            // Debug.Log("Object hit: " + hit.collider.gameObject);

            GameObject hitObject = hit.collider.gameObject;
            CollectibleTreasure c = hitObject.GetComponent<CollectibleTreasure>();

            float distanceBetweenHunterAndSphere = (transform.position - hitObject.transform.position).magnitude;
  
                if(c) {
                numberOfObjectsCollected++;
                Destroy(hitObject);
                }


         scoreMessage.text =  "# of Objects Collected: " + numberOfObjectsCollected;

            

         


        }
        }

    }




   

}
