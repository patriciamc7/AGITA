using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerVelocity;

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

        transform.Translate(playerVelocity * Time.deltaTime, 0, 0);
        transform.Translate(-horizontalInput * playerVelocity * 0.5f * Time.deltaTime, 0, 0);
        transform.Translate(0, horizontalvertical * playerVelocity * 0.5f * Time.deltaTime, 0);
    }
}
