using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderTrigger : MonoBehaviour
{
   void onTriggerEnter(Collider other) {
       Debug.Log("Trigger hit");
   }
}
