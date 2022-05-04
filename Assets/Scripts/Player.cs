using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float horizontalvertical = Input.GetAxisRaw("Vertical");

        transform.Translate(0, 0, -horizontalInput * Time.deltaTime);
        transform.Translate(0, horizontalvertical  * Time.deltaTime, 0);
    }
}
