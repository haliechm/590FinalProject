using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{

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
            p.transform.position = transform.position+cam.transform.forward*2;
            
          
            Rigidbody rb = p.GetComponent<Rigidbody>();
            rb.velocity=cam.transform.forward*15;
            
        }

    }
}
