using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public float cameraVelocity;
    public float totalDuration;
    public KinctMovePlayer KinectScript;
    public GameObject RightSide; 
    public GameObject LeftSide;
    //public boo
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Time.time < totalDuration)
        if (GameObject.Find("Body_Person") != null)
        {
            if (this.name == "RightCamera")
                transform.Translate(-KinectScript.vecloctyPlayer * Time.deltaTime, 0, 0);
            else
            {
                transform.Translate(KinectScript.vecloctyPlayer * Time.deltaTime, 0, 0);
                RightSide.gameObject.transform.position += new Vector3(KinectScript.vecloctyPlayer * Time.deltaTime, 0, 0);
                LeftSide.gameObject.transform.position += new Vector3(KinectScript.vecloctyPlayer * Time.deltaTime, 0, 0);

            }
        }
    }
	
}