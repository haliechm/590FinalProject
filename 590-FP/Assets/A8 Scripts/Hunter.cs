using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{

    public GameObject winMessageObject;
    public GameObject scoreMessageObject;
    public TextMesh numberOfLinesMessage;
    public TextMesh winMessage;
    public TextMesh scoreMessage;
    public GameObject line;

    public Camera oculusCam;

    private int numberOfObjectsCollected;
    private int numOfLines;






 
    void Start()
    {

        GameObject hunter = GameObject.Find("First Player");
        numberOfObjectsCollected = 0;
        numOfLines = 0;
        

        scoreMessage.text = numberOfObjectsCollected + " ";
        numberOfLinesMessage.text = "Number of Steps: " + numOfLines/20;

        winMessage.text = "You Win!";
        winMessageObject.SetActive(false);




        



     
 
        
        
    }

    
    void Update() {

        numberOfLinesMessage.text = "Number of Steps: " + numOfLines/20;






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

        if(Input.GetButton("Vertical")) {
            Instantiate(line, transform.position, transform.rotation);
            numOfLines++;
        }

          if(Input.GetButton("Horizontal")) {
            Instantiate(line, transform.position, transform.rotation);
            numOfLines++;
        }

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
         

            scoreMessage.text = numberOfObjectsCollected + " ";

            if(numberOfObjectsCollected == 10) {
                winMessage.text = "you win";
                // winMessageObject.setActive();
            }


        }

        
 
       

        }

       

         
        

}
