using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float totalDuration;
    public GameObject cameraleft; 
    public GameObject sideright;

    // Update is called once per frame
    void Update()
    {
 
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            transform.Translate(Time.deltaTime* horizontalInput * 0.9f, 0, 0);
        }
    }

}