using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalc : MonoBehaviour
{


    private Vector3 lastPosition ;
    private float totalDistance ;
    // Start is called before the first frame update
    void Start()
    {

        lastPosition = transform.position;
        totalDistance = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        // float dist = Vector3.Distance(lastPosition, transform.position) ;
        // totalDistance += dist;
        // lastPosition = transform.position;

        // Debug.Log("____________Total distance travelled:" + totalDistance);


        // Vector3 difference = new Vector3(
        // transform.position.x - lastPosition.x,
        // transform.position.y - lastPosition.y,
        // transform.position.z - lastPosition.z);


        // float d = Mathf.Sqrt(Mathf.Pow(difference.x, 2f) + 0 + Mathf.Pow(difference.z, 2f));
        // totalDistance += d;
        // lastPosition = transform.position;

        // Debug.Log("____________Total distance travelled:" + totalDistance);

        // only measure distance when hitting an arrow key
            if (Input.GetButton ("Vertical")) {
                float dist = Vector3.Distance(lastPosition, transform.position);
                totalDistance += dist;
                lastPosition = transform.position;

                Debug.Log("____________Total distance travelled:" + totalDistance);
               
            }

            if (Input.GetButton ("Horizontal")) {
                float dist = Vector3.Distance(lastPosition, transform.position);
                totalDistance += dist;
                lastPosition = transform.position;

                Debug.Log("____________Total distance travelled:" + totalDistance);
              
            }
        
    }
}
