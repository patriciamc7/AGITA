using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Update is called once per frame
    public bool collitionObjectsPlayer = false; 
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) //CHOCA SUELO
        {
            collitionObjectsPlayer = false; 
        }
        if (other.gameObject.layer == 8) // CHOCA INTERACTIVE
        {
            collitionObjectsPlayer = false; 
        }
    }


}
