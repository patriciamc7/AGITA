using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;

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

        transform.Translate(-horizontalInput + movementSpeed * Time.deltaTime, 0, 0);
        transform.Translate(0, -horizontalvertical * movementSpeed * Time.deltaTime, 0);
    }
}
