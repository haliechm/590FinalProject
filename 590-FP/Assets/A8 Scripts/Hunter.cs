using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{

    public GameObject winMessageObject;
    public GameObject scoreMessageObject;
    public TextMesh winMessage;
    public TextMesh scoreMessage;

    public Camera oculusCam;

    private int numberOfObjectsCollected;



 
    void Start()
    {

        GameObject hunter = GameObject.Find("First Player");
        numberOfObjectsCollected = 0;
        

        scoreMessage.text = numberOfObjectsCollected + " ";

        winMessage.text = "You Win!";
        winMessageObject.SetActive(false);
 
        
        
    }

    
    void Update() {



    RaycastHit hit;
        // if(Input.GetKeyDown("1")) {
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
