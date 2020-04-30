using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{

     public AudioClip roll;
     public AudioClip WoodWall;
     public AudioClip MetalWall;
     public AudioClip BrickWall;
     public AudioClip CarpetWall;
     public AudioClip GlassWall;
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

        // Wall Hit Sound
        if(other.gameObject.tag == "WoodWall"){
          audio.clip = WoodWall;
          audio.Play(0);
        }else if(other.gameObject.tag == "MetalWall"){
          audio.clip = MetalWall;
          audio.Play(0);
        }else if(other.gameObject.tag == "BrickWall"){
          audio.clip = BrickWall;
          audio.Play(0);
        }else if(other.gameObject.tag == "CarpetWall"){
          audio.clip = CarpetWall;
          audio.Play(0);
        }else if(other.gameObject.tag == "GlassWall"){
          audio.clip = GlassWall;
          audio.Play(0);
        }



        // Rolling Sound
        if(other.gameObject.tag == "HardwoodFloor"){
            // linearly interpolates 
          audio.volume = Mathf.Lerp(0.1f, 1.0f, impactVelocity/(1.0f-0.1f));
          audio.clip = roll;
          audio.Play(0);
        }
     }
}
