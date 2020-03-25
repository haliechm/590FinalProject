using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour {

public float speed;
public GameObject plane;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 4 , 0);
        orbitAround();
    }

    void orbitAround() {
        transform.RotateAround(plane.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
