using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerVelocity;
    public float cameraVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float horizontalvertical = Input.GetAxisRaw("Vertical");

        transform.Translate(cameraVelocity * Time.deltaTime, 0, 0);
        transform.Translate(-horizontalInput * cameraVelocity * playerVelocity * Time.deltaTime, 0,0);
        transform.Translate(0, horizontalvertical * cameraVelocity * playerVelocity * Time.deltaTime, 0);
    }
}
