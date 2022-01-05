using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraVelocity;
    public float totalDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < totalDuration)
            transform.Translate(cameraVelocity * Time.deltaTime, 0, 0);
    }
}
