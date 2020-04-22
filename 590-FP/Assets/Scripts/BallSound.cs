using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{

     public AudioClip roll;
     public AudioClip hitWall;
     AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter(Collision other){

         float impactVelocity = other.relativeVelocity.magnitude;

        if(other.gameObject.tag == "InvisibleWall"){
          audio.clip = hitWall;
          audio.Play();
        }
        if(other.gameObject.tag == "HardwoodFloor"){
          audio.volume = Mathf.Lerp(0.1f, 1.0f, impactVelocity/(1.0f-0.1f));
          audio.clip = roll;
          audio.Play();
        }
     }
}
