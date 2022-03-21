using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public float cameraVelocity;
    public float totalDuration;
    //public KinctMovePlayer KinectScript;
    public float velocitycamera;
    public GameObject cameraleft; 

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Body_Person") != null)
        {
            
            transform.position += new Vector3(velocitycamera * Time.deltaTime, 0, 0);
            
            cameraleft.gameObject.transform.position += new Vector3(velocitycamera * Time.deltaTime, 0, 0);
        }

        //movimeinto flechas
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float horizontalvertical = Input.GetAxisRaw("Vertical");
        if (horizontalInput != 0 || horizontalvertical != 0)
        {
            transform.Translate(Time.deltaTime* horizontalInput, Time.deltaTime * horizontalvertical, 0);
        }
    }

}