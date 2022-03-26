using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentVagon : MonoBehaviour
{
    public float velocitycamera;

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("Body_Person") != null)
        {

            transform.position += new Vector3(velocitycamera * Time.deltaTime, 0, 0);

        //    cameraleft.gameObject.transform.position += new Vector3(velocitycamera * Time.deltaTime, 0, 0);
        }
    }
}
