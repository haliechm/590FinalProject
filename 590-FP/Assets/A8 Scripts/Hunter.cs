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
                
         
            
            // Debug.Log("Did Hit");
            //  Debug.Log("Object hit: " + hit.collider.gameObject);
        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
       
        }

        if (Input.GetKeyDown(KeyCode.Return)) {

            // check to see hit object is coin object, if it is then add it to the first index of the array, etc
            // or can just do it have an array of 20, add whatever object to the array, then when counting go through the entire array
            Debug.Log("_____________Key Pressed: enter");
            // Debug.Log("Object hit: " + hit.collider.gameObject);

            GameObject hitObject = hit.collider.gameObject;
            CollectibleTreasure c = hitObject.GetComponent<CollectibleTreasure>();

            float distanceBetweenHunterAndSphere = (transform.position - hitObject.transform.position).magnitude;
            
            // if(c && distanceBetweenHunterAndSphere < 1.0f) {
                if(c) {
                numberOfObjectsCollected++;
                Destroy(hitObject);
                }
            // }

            scoreMessage.text = numberOfObjectsCollected + " ";

            if(numberOfObjectsCollected == 10) {
                winMessage.text = "you win";
                // winMessageObject.setActive();
            }
            

            // Treasure obj = (hit.collider.gameObject).GetComponent<Treasure>();
            // Debug.Log("Object's Point Value: " + obj.pointValue);

            // Debug.Log("here: " + obj.pointValue);

            // score = score + obj.pointValue;
            // // Debug.Log("score: " + score);
            // scoreMessage.text = "Halie\r\nScore: " + score;


            // switch(obj.pointValue) {
            //     case 1:
            //         // Debug.Log("coin was hit");
            //         inventory.collectibles[0] = coin;
            //         inventory.count[0] += 1;
            //         break;
            //     case 10:
            //         // Debug.Log("Treasure Chest was hit");
            //         inventory.collectibles[1] = treasureChest;
            //         inventory.count[1] += 1;
            //         break;
            //     case 100:
            //         // Debug.Log("Diamond was hit");
            //         inventory.collectibles[2] = diamond;
            //         inventory.count[2] += 1;
            //         break;
            //     default:
            //         // Debug.Log("DEFAULT"); 
            //         break;
            // }

            // if (obj.pointValue == coinPointValue) {
            //     Debug.Log("coin");
            // } else (obj.pointValue == treasureChestPointValue)
            //     Debug.Log("treasure chest");
            // } else (obj.pointValue == )
        // updateInventory();

        }

        // if (Input.GetKeyDown("2")) {
        //     Debug.Log("Key Pressed: 2");
        //     updateInventory();
         
        //     // inventoryMessageObject.setActive(inventoryMessageOn);
        //     inventoryMessageOn = !inventoryMessageOn;
        //     inventoryMessageObject.SetActive(inventoryMessageOn);
        // }

        // if(Input.GetKeyDown("3")) {
        //     Debug.Log("Key Pressed: 3");
        //     scoreMessageOn = !scoreMessageOn;
        //     scoreMessageObject.SetActive(scoreMessageOn);
        // }

        

       

        }

       

         
        

}
