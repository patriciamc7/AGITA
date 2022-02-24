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
       // movement();
    }
    //Si el peesonaje sale del colaider de la camera que elpunto donde quiera ir sea el centro de la cameraç
    // y elintervalo entre fames sea mayor para que vaya al centro de la camera. 
    //void OnTriggerStay(Collider other)
    //{
    //    //Debug.Log(other);

    //    if (other.gameObject.tag == "Camera")
    //    {
    //        //Debug.Log("Si");

    //        transform.position = transform.position + new Vector3(playerVelocity * Time.deltaTime, 0, 0);

    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    //Debug.Log(other);

    //    if (other.gameObject.tag == "Camera")
    //    {
    //        //Debug.Log("Si");
    //        Vector3 pos = transform.position; 
    //        transform.position = new Vector3(other.gameObject.transform.position.x, pos.y, pos.z); 

    //    }
    //}

    //void movement()
    //{
    //    float horizontalInput = Input.GetAxisRaw("Horizontal");
    //    float horizontalvertical = Input.GetAxisRaw("Vertical");

    //    transform.Translate(0, 0, cameraVelocity * Time.deltaTime);
    //    transform.Translate(0, 0, -horizontalInput * cameraVelocity * playerVelocity * Time.deltaTime);
    //    transform.Translate(0, 0, horizontalInput * cameraVelocity * playerVelocity * Time.deltaTime);
    //}
}
