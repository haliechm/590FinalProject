using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttachmentRule{KeepRelative,KeepWorld,SnapToTarget}

public class Player : MonoBehaviour
{

    
 
    // Position Storage Variables
    // Vector3 posOffset = new Vector3 ();
    // Vector3 tempPos = new Vector3 ();

    private bool justStarted;
    private GameObject winMessageObject;
    private GameObject inventoryMessageObject;
    public TextMesh winMessage;
    public TextMesh inventoryMessage;
    public TextMesh scoreMessage;
    private int numThingsCollected;

// questions:
// do we make pivot an actual game object?
// how much to accelerate all wrong

    public Camera oculusCam;
    public GameObject leftPointerObject;
    public GameObject rightPointerObject;
    public LayerMask collectiblesMask;
    Things thingIGrabbed;
    Vector3 previousPointerPos;


    // NEW FOR A7
    Vector3 prevForwardVector;
    float prevYawRelativeToCenter;
    public GameObject VRTrackingOrigin;

    public GameObject pivot;





    void Start()
    {




        justStarted = true;
        numThingsCollected = 0;
        winMessage = GameObject.Find("Win Message").GetComponent<TextMesh>();
        winMessage.gameObject.SetActive(false);
        scoreMessage = GameObject.Find("Score Message").GetComponent<TextMesh>();


        // NEW FOR A7

        // Q: I set VRTrackingOrigin to GameObject Tracking Space (should it instead by center eye?)
        prevForwardVector = oculusCam.transform.forward;
        prevYawRelativeToCenter = angleBetweenVectors(oculusCam.transform.forward, VRTrackingOrigin.transform.position-oculusCam.transform.position);


    }

    // FUNCTIONS FOR A7
    public float d(Vector3 A, Vector3 B, Vector3 C) {
        return (A.x-B.x)*(C.y-B.y)-(A.y-B.y)*(C.x-B.x);
    }

    public float angleBetweenVectors(Vector3 A, Vector3 B) {
        return (Mathf.Acos(Vector3.Dot(Vector3.Normalize(A), Vector3.Normalize(B)))) * (180/Mathf.PI);
    }

 

    
    void Update()
    {



    // NEW CODE FOR A7

    float howMuchUserRotated = angleBetweenVectors(prevForwardVector, oculusCam.transform.forward);
    float directionUserRotated = (d(oculusCam.transform.position+prevForwardVector, oculusCam.transform.position, oculusCam.transform.position+oculusCam.transform.forward) < 0) ? 1 : -1;
    float deltaYawRelativeToCenter = prevYawRelativeToCenter - angleBetweenVectors(oculusCam.transform.forward, VRTrackingOrigin.transform.position-oculusCam.transform.position);

    float distanceFromCenter = oculusCam.transform.localPosition.magnitude;
    print("distanceFromCenter"+distanceFromCenter);
    // Q: I'm thinking something it scaling wrong here b/c of parenting
    //float longestDimensionOfPE = VRTrackingOrigin.transform.localScale.x > VRTrackingOrigin.transform.localScale.z ?
         //VRTrackingOrigin.transform.localScale.x : VRTrackingOrigin.transform.localScale.z;
    float longestDimensionOfPE=1f;
    float howMuchToAccelerate = ((deltaYawRelativeToCenter < 0) ? -0.13f : 0.30f) * howMuchUserRotated * directionUserRotated 
        * Mathf.Clamp(distanceFromCenter/longestDimensionOfPE/2, 0.0f, 1.0f);


    // Q: I just made pivot a empty GameObject in the scene, is that alright?
    // Q: how to make vr tracking origin child of pivot parent
    // make VRTrackingOrigin child of a pivot parent located at oculusCam.position
    print("howmuchaccel "+howMuchToAccelerate);
    if (Mathf.Abs(howMuchToAccelerate)>=0.0001f){
        //pivot.transform.parent=null;
        //pivot.transform.position = oculusCam.transform.position;
        //this.transform.parent = pivot.transform;
        
        // Q: is pivot right?
        // pivot.transform.rotation.yaw += howMuchToAccelerate;
        // pivot.transform.rotation += howMuchToAccelerate
        //pivot.transform.eulerAngles=new Vector3(pivot.transform.eulerAngles.x, pivot.transform.eulerAngles.y+(float)howMuchToAccelerate, pivot.transform.eulerAngles.z);
        this.transform.RotateAround(oculusCam.transform.position,new Vector3(0,1,0),howMuchToAccelerate);
        print("pivot transform "+pivot.transform.position+", "+oculusCam.transform.position);
        // unparent pivot
        // Q: do we need to make pivot child null as well?
        this.transform.parent = null;
    }
    prevForwardVector = oculusCam.transform.forward;
    prevYawRelativeToCenter = angleBetweenVectors(oculusCam.transform.forward, VRTrackingOrigin.transform.position - oculusCam.transform.position);
























        if (justStarted) {
            winMessage.gameObject.SetActive(false);
            justStarted = false;
        }
        
        if (numThingsCollected >= 5) {
            
            winMessage.gameObject.SetActive(true);
        }
        scoreMessage.text = "Halie\n# of Items Collected: " + numThingsCollected;
        

    if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)) {

       

        Collider[] overlappingThings = Physics.OverlapSphere(rightPointerObject.transform.position, 0.01f, collectiblesMask);
    
         if (overlappingThings.Length>0) {
             attachGameObjectToAChildGameObject(overlappingThings[0].gameObject, rightPointerObject, AttachmentRule.KeepWorld, AttachmentRule.KeepWorld, AttachmentRule.KeepWorld, true);
             thingIGrabbed = overlappingThings[0].gameObject.GetComponent<Things>();
        
         }
    } else if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)) {
        letGo();
    } else if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger)) {
   
    Collider[] overlappingThings = Physics.OverlapSphere(leftPointerObject.transform.position, 0.01f, collectiblesMask);

         if (overlappingThings.Length>0) {
            attachGameObjectToAChildGameObject(overlappingThings[0].gameObject, leftPointerObject, AttachmentRule.KeepWorld, AttachmentRule.KeepWorld, AttachmentRule.KeepWorld, true);
            thingIGrabbed = overlappingThings[0].gameObject.GetComponent<Things>();
      
         }
    } else if(OVRInput.GetUp(OVRInput.RawButton.LHandTrigger)) {
    letGo();
    }

   
}

    void letGo(){
        if (thingIGrabbed){
      

            if(rightPointerObject.gameObject.transform.position.y < oculusCam.transform.position.y - 0.2 
            && rightPointerObject.gameObject.transform.position.y > oculusCam.transform.position.y - 0.6
            && rightPointerObject.gameObject.transform.position.x < oculusCam.transform.position.x + 0.3
            && rightPointerObject.gameObject.transform.position.x > oculusCam.transform.position.x - 0.3
            && rightPointerObject.gameObject.transform.position.z < oculusCam.transform.position.z + 0.3
            && rightPointerObject.gameObject.transform.position.z > oculusCam.transform.position.z - 0.3) {
                    
                  
                detachGameObject(thingIGrabbed.gameObject,AttachmentRule.KeepWorld,AttachmentRule.KeepWorld,AttachmentRule.KeepWorld);
                simulatePhysics(thingIGrabbed.gameObject,Vector3.zero,true);
            
                numThingsCollected = numThingsCollected + 1;
                Destroy(thingIGrabbed.gameObject);
                thingIGrabbed=null;

                } else {
               
                detachGameObject(thingIGrabbed.gameObject,AttachmentRule.KeepWorld,AttachmentRule.KeepWorld,AttachmentRule.KeepWorld);
                simulatePhysics(thingIGrabbed.gameObject,Vector3.zero,true);
                thingIGrabbed=null;
                }
        }
    }

    public void attachGameObjectToAChildGameObject(GameObject GOToAttach, GameObject newParent, AttachmentRule locationRule, AttachmentRule rotationRule, AttachmentRule scaleRule, bool weld){
        GOToAttach.transform.parent=newParent.transform;
        handleAttachmentRules(GOToAttach,locationRule,rotationRule,scaleRule);
        if (weld){
            simulatePhysics(GOToAttach,Vector3.zero,false);
        }
    }

     public static void detachGameObject(GameObject GOToDetach, AttachmentRule locationRule, AttachmentRule rotationRule, AttachmentRule scaleRule){
        //making the parent null sets its parent to the world origin (meaning relative & global transforms become the same)
        GOToDetach.transform.parent=null;
        handleAttachmentRules(GOToDetach,locationRule,rotationRule,scaleRule);
    }

    public static void handleAttachmentRules(GameObject GOToHandle, AttachmentRule locationRule, AttachmentRule rotationRule, AttachmentRule scaleRule){
        GOToHandle.transform.localPosition=
        (locationRule==AttachmentRule.KeepRelative)?GOToHandle.transform.position:
        //technically don't need to change anything but I wanted to compress into ternary
        (locationRule==AttachmentRule.KeepWorld)?GOToHandle.transform.localPosition:
        new Vector3(0,0,0);

        //localRotation in Unity is actually a Quaternion, so we need to specifically ask for Euler angles
        GOToHandle.transform.localEulerAngles=
        (rotationRule==AttachmentRule.KeepRelative)?GOToHandle.transform.eulerAngles:
        //technically don't need to change anything but I wanted to compress into ternary
        (rotationRule==AttachmentRule.KeepWorld)?GOToHandle.transform.localEulerAngles:
        new Vector3(0,0,0);

        GOToHandle.transform.localScale=
        (scaleRule==AttachmentRule.KeepRelative)?GOToHandle.transform.lossyScale:
        //technically don't need to change anything but I wanted to compress into ternary
        (scaleRule==AttachmentRule.KeepWorld)?GOToHandle.transform.localScale:
        new Vector3(1,1,1);
    }

     public void simulatePhysics(GameObject target,Vector3 oldParentVelocity,bool simulate){
        Rigidbody rb=target.GetComponent<Rigidbody>();
        if (rb){
            if (!simulate){
                
                Destroy(rb);
            } 
        } else{
            if (simulate){
                
                //there's actually a problem here relative to the UE4 version since Unity doesn't have this simple "simulate physics" option
                //The object will NOT preserve momentum when you throw it like in UE4.
                //need to set its velocity itself.... even if you switch the kinematic/gravity settings around instead of deleting/adding rb
                Rigidbody newRB=target.AddComponent<Rigidbody>();
                newRB.velocity=oldParentVelocity;
            }
        }
    }




   

}
