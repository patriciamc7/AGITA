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
    Vector3 inversePositionHand; 
    float range = 100f;

    public GameObject seeHand; 
    public GameObject seeNeck; 
    public GameObject seeElbow; 
    public GameObject seeSholder;
    private Vector3 initRay;

    public GameObject rightside; 
    public GameObject leftside;

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
            Body.gameObject.transform.position = Body.gameObject.transform.position + new Vector3(vecloctyPlayer * Time.deltaTime, 0, 0);

            rightHand = GetChildWithName(Body, "WristRight");
            neck = GetChildWithName(Body, "Neck");
            elbowRight = GetChildWithName(Body, "ElbowRight");
            sholderRight = GetChildWithName(Body, "ShoulderRight");
            //__________
            seeHand.gameObject.transform.position = rightHand.gameObject.transform.position;
            seeNeck.gameObject.transform.position = neck.gameObject.transform.position;
            seeElbow.gameObject.transform.position = elbowRight.gameObject.transform.position;
            seeSholder.gameObject.transform.position = sholderRight.gameObject.transform.position;

            //______
            rightHand.gameObject.transform.position = Vector3.Scale(rightHand.gameObject.transform.position, new Vector3(-1, 1, 1));
            neck.gameObject.transform.position = Vector3.Scale(neck.gameObject.transform.position, new Vector3(-1, 1, 1));
            elbowRight.gameObject.transform.position = Vector3.Scale(elbowRight.gameObject.transform.position, new Vector3(-1, 1, 1));
            sholderRight.gameObject.transform.position = Vector3.Scale(sholderRight.gameObject.transform.position, new Vector3(-1, 1, 1));

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
        initRay = neck.gameObject.transform.position;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            methodChoose = 1;
            initRay = neck.gameObject.transform.position;
            Debug.Log("Hand and Neck"); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            methodChoose = 2;
            initRay = elbowRight.gameObject.transform.position;
            Debug.Log("Hand and Elbow");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            methodChoose = 3;
            initRay = sholderRight.gameObject.transform.position;
            Debug.Log("Hand and Sholder");

        }
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
        //VectorInPlain = rightHand.gameObject.transform.position;
        //VectorInPlain = Vector3.Scale(VectorInPlain, new Vector3(-1, 1, 1));
        Vector3 pointRightSide = rayWall(VectorInPlain);
        //Debug.Log(pointRightSide); 

        posPlayersum = Hada.gameObject.transform.position; 
        posPlayersum.x = posPlayersum.x + (pointRightSide.x - posPlayersum.x) / 27;
		posPlayersum.y = posPlayersum.y + (pointRightSide.y - posPlayersum.y) / 27;

        //only right
		posPlayersum.z = rightside.gameObject.transform.position.z;
		Hada.gameObject.transform.position = posPlayersum;
	}

    Vector3 rayWall(Vector3 VectorInPlain) 
    {
        RaycastHit hit;

        if (Physics.Raycast(Vector3.Scale(initRay, new Vector3(-1, 1, 1)), Vector3.Scale(VectorInPlain, new Vector3(1, 1, -1)), out hit, range))
        {
            if (hit.collider.name == "RightSide")
            {
                //Debug.Log("RAYO: " + hit.collider.name);
                //Debug.Log("RAYO: "+ hit.point);
                return hit.point;
            }
            return Vector3.Scale(rightHand.gameObject.transform.position, new Vector3(-1, 1, 1));
        }
        
        return Vector3.Scale(rightHand.gameObject.transform.position, new Vector3(-1, 1, 1)); 
        
    }
	private void OnDrawGizmos()
	{
        if (GameObject.Find("Body_Person") != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.Scale(initRay, new Vector3(-1,1,1)), Vector3.Scale(VectorInPlain, new Vector3(1,1,-1)) * range);
        }
	}

}
