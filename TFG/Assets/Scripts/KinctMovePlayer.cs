using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinctMovePlayer : MonoBehaviour
{
    public GameObject Hada;
    public GameObject KinectParent;
    private GameObject Body;
    private GameObject rightHand;
    private GameObject neck;
    private GameObject elbowRight;
    private GameObject sholderRight;
    private Vector3 HandToNeck; 
    private Vector3 HandToElbow;
    private Vector3 HandToSholder;
    private int methodChoose = 1;
    private Vector3 VectorInPlain  = new Vector3(0,0,2.92f);
    private Vector3 posPlayersum;
    public float vecloctyPlayer; 
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
            //move player camioneta
            Body.gameObject.transform.position = Body.gameObject.transform.position + new Vector3(-vecloctyPlayer * Time.deltaTime, 0, 0);

            rightHand = GetChildWithName(Body, "WristRight");
            neck = GetChildWithName(Body, "Neck");
            elbowRight = GetChildWithName(Body, "ElbowRight");
            sholderRight = GetChildWithName(Body, "ShoulderRight");
            
            HandToNeck = vector2nodesNormalice(rightHand.gameObject.transform.position, neck.gameObject.transform.position);
            HandToElbow = vector2nodesNormalice(rightHand.gameObject.transform.position, elbowRight.gameObject.transform.position);
            HandToSholder = vector2nodesNormalice(rightHand.gameObject.transform.position, sholderRight.gameObject.transform.position);
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
    Vector3 vector2nodesNormalice(Vector3 hand, Vector3 otherNode) 
    {
        return Vector3.Normalize(hand - otherNode); 
    }
    void changeMethod()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            methodChoose = 1;
            Debug.Log("Hand and Neck"); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            methodChoose = 2;
            Debug.Log("Hand and Elbow");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            methodChoose = 3;
            Debug.Log("Hand and Sholder");

        }
    }

    void obtainRecta(Vector3 VectorInPlain, Vector3 other) 
    {
        Vector3 recta = other + VectorInPlain; 

    }
    void moveCharater() 
    {
        changeMethod();
        if (methodChoose == 1) 
        {
            VectorInPlain.x = HandToNeck.x; 
            VectorInPlain.y = HandToNeck.y;
        }
        if (methodChoose == 2) 
        {
            VectorInPlain.x = HandToElbow.x;
            VectorInPlain.y = HandToElbow.y;
        }
        if (methodChoose == 3)
        {
            VectorInPlain.x = HandToSholder.x;
            VectorInPlain.y = HandToSholder.y;
        }
        //ejes al reves
        VectorInPlain = rightHand.gameObject.transform.position;
        VectorInPlain = Vector3.Scale(VectorInPlain, new Vector3(-1, 1, 1));

        posPlayersum = Hada.gameObject.transform.position; 
        posPlayersum.x = posPlayersum.x + (VectorInPlain.x - posPlayersum.x) / 27;
		posPlayersum.y = posPlayersum.y + (VectorInPlain.y - posPlayersum.y) / 27;
		posPlayersum.z = Hada.gameObject.transform.position.z;
		Hada.gameObject.transform.position = posPlayersum;
	}
    
}
