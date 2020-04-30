using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {

    // WHEN MAKING ANY FLOORS MAKE SURE TO SET TAG TO "HARDWOOD" FLOOR

    // numberOfLines is message on top left
    // scoreMessage is message on top right
    // winMessage appears afer you collect 5 objects
    // startMessage is the message at beginning telling to read to the sign and hit the s key to start



    // to calculate number of steps:
    // first find average fps throughout entire game (every frame find the sum fps, and also add 1 to counter variable numOfFrames)
    // when collect last item, divide sum fps by numOfFrames to find average fps
    // when collect last item, multiply avg fps by (1/25) and multiply by (0.4/1), which will give Avg #frames/step
    // when collect last item, multiply #steps by (1/ avg#frames/step) to find the total number of steps
    // update function is called every frame

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

    // private double numOfSteps;

    // some hacky variables
    private bool notDisplayedYet;
    private bool started;

    // private double sumOfFPS;
    // private double numOfFrames;


    // // https://answers.unity.com/questions/1494589/how-to-measure-the-overall-distance-travelled.html
    // // TRYING TO FIGURE OUR TOTAL DISTANCE TRAVELLED BY USER
    // private Vector3 lastPosition ;
    // private float totalDistance ;

    private DistanceCalc calc;
    

    public Bag bag;

    void Start () {

        GameObject hunter = GameObject.Find ("First Player");
        numberOfObjectsCollected = 0;
        numOfLines = 0;

        scoreMessage.text = "Halie, Meris, Bea, LJ\n# of Objects Collected: " + numberOfObjectsCollected;
        numberOfLinesMessage.text = "Time Elapsed: " + timeElapsed.ToString ("N1");

        winMessage.text = "You Win!";
        winMessageObject.SetActive (false);

        timeElapsed += Time.deltaTime;
        notDisplayedYet = true;
        started = false;

        // numOfSteps = 0;

        // sumOfFPS = 0;
        // numOfFrames = 0;


        // lastPosition = transform.position;
        // totalDistance = 0;


        GameObject cea = GameObject.Find("CenterEyeAnchor");
        calc = cea.GetComponent<DistanceCalc>();
        


    }

    void Update () {



        // TRYING TO FIGURE OUT DISTANCE TRAVELLED
        //   float dist = Vector3.Distance(lastPosition, transform.position) ;
        // totalDistance += dist;
        // lastPosition = transform.position;


        // SETTING CURSOR TO NOT VISIBLE BECAUSE BALL SHOOTS BASED ON WHERE YOU LOOK, NOT WHERE YOUR CURSOR IS
        Cursor.visible = false;

        // HIT THE S KEY TO START (WON'T KEEP TRACK OF STEPS OR START TIME UNTIL USER HAS READ THE SIGN AND HIT S)
        if (Input.GetKeyDown (KeyCode.S)) {
            started = true;
            startMessageObject.SetActive (false);
        }

        if (started) {
            // KEEPING TRACK OF FPS VALUES FOR END CALCULATION
            // double fps = 1.0/Time.deltaTime;
            // sumOfFPS = sumOfFPS + fps;
            // numOfFrames = numOfFrames + 1;

            timeElapsed += Time.deltaTime;
            numberOfLinesMessage.text = /*"Number of Steps: " + numOfSteps +*/ "Time Elapsed: " + timeElapsed.ToString ("N1");

            // CHECK TO SEE IF USER HAS COLLECTED 5 OBJECTS
            if (numberOfObjectsCollected >= 5 && notDisplayedYet) {

                //  Debug.Log("____________Total distance travelled:" + totalDistance/10);

                // double averageFPS = sumOfFPS / numOfFrames;
                // double avgFramesPerStep = averageFPS * 5 * (0.762);
               

                // numOfLines counted the number of times user was hitting arrow key during a frame
                // numOfSteps = numOfLines * (1 / avgFramesPerStep);
                

                // Debug.Log("___________________AvgFPS" + averageFPS);
                // Debug.Log("___________________numOfFrames" + numOfFrames);
                // Debug.Log("________numOfSteps" + numOfSteps);
                // Debug.Log("___________________AvgFramePerStep " + avgFramesPerStep);
                // Debug.Log("_____________numOfLines" + numOfLines);



                // winMessage.text = "5 Objects Collected\nFinal # of Steps Taken: " + numOfLines / 15 + "\nFinal Time Elapsed: " + timeElapsed.ToString ("N1");
                //numOfSteps.ToString("N1")
                float totalDistanceTravelled = calc.totalDistance;
                winMessage.text = "5 Objects Collected\nTotal Distance Travelled: " + totalDistanceTravelled.ToString("N3") + " m" + "\nFinal Time Elapsed: " + timeElapsed.ToString ("N1");
                winMessageObject.SetActive (true);
                notDisplayedYet = false;
                numberOfLinesObject.SetActive (false);
                scoreMessageObject.SetActive (false);
            }

            // RAYCAST TO COLLECT COLLECTIBLES 
            RaycastHit hit;
            if (Physics.Raycast (oculusCam.transform.position, oculusCam.transform.TransformDirection (Vector3.forward), out hit, Mathf.Infinity)) {

                GameObject hitObject = hit.collider.gameObject;
                CollectibleTreasure c = hitObject.GetComponent<CollectibleTreasure> ();

                float distanceBetweenHunterAndSphere = (transform.position - hitObject.transform.position).magnitude;

                if (c && distanceBetweenHunterAndSphere < 10.0f) {
                    Debug.DrawRay (oculusCam.transform.position, oculusCam.transform.TransformDirection (Vector3.forward) * hit.distance, Color.red);
                } else {
                    Debug.DrawRay (oculusCam.transform.position, oculusCam.transform.TransformDirection (Vector3.forward) * 1000, Color.white);
                }

            } else {
                Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * 1000, Color.white);

            }

            // MAKES THE LINE THAT FOLLOWS THE USER AS THEY WALK
            if (Input.GetButton ("Vertical")) {
                Instantiate (line, transform.position, transform.rotation);
                numOfLines++;

                

                // numOfSteps = numOfLines * spf * .2 * (1/0.4);
            }

            if (Input.GetButton ("Horizontal")) {
                Instantiate (line, transform.position, transform.rotation);
                numOfLines++;
            }

            // HIT ENTER KEY TO COLLECT COLLECTILBE
            if (Input.GetKeyDown (KeyCode.Return)) {

                Debug.Log ("_____________Key Pressed: enter");
                // Debug.Log("Object hit: " + hit.collider.gameObject);

                // GameObject hitObject = hit.collider.gameObject;
                // CollectibleTreasure c = hitObject.GetComponent<CollectibleTreasure> ();

                // float distanceBetweenHunterAndSphere = (transform.position - hitObject.transform.position).magnitude;

                // if (c) {
                //     numberOfObjectsCollected++;
                //     Destroy (hitObject);
                // }

                foreach (CollectibleTreasure audioSphere in bag.AudioSpheres) {
                    if(audioSphere == null) continue;
                    float distance = Vector3.Distance (transform.position, audioSphere.transform.position);
                    if (distance < 3) {
                        Destroy (audioSphere.gameObject); //might need to lower distance to maybe less than 1
                        numberOfObjectsCollected++;
                }
                scoreMessage.text = "Halie, Meris, Bea, LJ\n# of Objects Collected: " + numberOfObjectsCollected;
            }
        }
    }}
}