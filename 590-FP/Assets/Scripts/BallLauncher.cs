using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{


    public GameObject metalBall;
    public GameObject cam;
   
    void Start()
    {
       
        
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            // pause audio of BallLaunched
            // launch ball from player position

            // move metalBall position to player position
            // launch metal ball

            metalBall.transform.position = cam.transform.position;
            Rigidbody rb = metalBall.GetComponent<Rigidbody>();
            rb.velocity=cam.transform.forward*15;

            //!
            // GameObject p = Instantiate(metalBall) as GameObject;
        
            // p.transform.position = cam.transform.position+cam.transform.forward*1;
            
           
            // Rigidbody rb = p.GetComponent<Rigidbody>();
            // rb.velocity=cam.transform.forward*15;
            
        }

    }
}
