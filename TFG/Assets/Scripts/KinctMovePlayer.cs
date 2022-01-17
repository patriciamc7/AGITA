using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinctMovePlayer : MonoBehaviour
{
    public GameObject Hada;
    public GameObject KinectParent; 
    public GameObject Body;
    public GameObject rightHand; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If Body detected assign Kineckt gameobject
        if (GameObject.Find("Body_Person") != null)
        {
            Body = GameObject.Find("Body_Person"); 
            Body.gameObject.transform.SetParent(KinectParent.gameObject.transform);

            rightHand = GetChildWithName(Body, "WristRight");
            //GetChildWithName(Body, "WristLeft"); 
            moveCharater(); 
        }

    }

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    void moveCharater() 
    {
        Hada.gameObject.transform.position = rightHand.gameObject.transform.position; 
    }
}
