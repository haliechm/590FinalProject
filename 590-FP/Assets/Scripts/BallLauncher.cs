using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{


// how to make ball not go past wall when directly up against wall
// how to propagate audio off of ball?


// add lines to show where user walked
// just have one ball and then change position of it 

    GameObject metalBall;
    public GameObject cam;
   
    void Start()
    {
        // go to the prefab MetalBall under Assests->Resources to change what MetalBall is/does
        metalBall = Resources.Load("MetalBall") as GameObject;
        
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            GameObject p = Instantiate(metalBall) as GameObject;
            // p.transform.position = transform.position+cam.transform.forward*2;
            p.transform.position = cam.transform.position+cam.transform.forward*1;
            
            // p.transform.position = new Vector3(cam.transform.position.x*2, cam.transform.position.y, cam.transform.position.z*2);
            Rigidbody rb = p.GetComponent<Rigidbody>();
            rb.velocity=cam.transform.forward*15;
            
        }

    }
}
