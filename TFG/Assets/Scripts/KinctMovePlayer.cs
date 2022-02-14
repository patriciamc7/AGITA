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
    private bool initPos = true; 
   
    // Update is called once per frame
    void Update()
    {
        
        //If Body detected assign Kineckt gameobject
        if (GameObject.Find("Body_Person") != null)
        {
            Body = GameObject.Find("Body_Person");

            Body.gameObject.transform.SetParent(KinectParent.gameObject.transform);
            //move player middle walls
            if (initPos)
            {
                Body.gameObject.transform.Rotate(0, -90, 0);
                Body.gameObject.transform.Translate(new Vector3(-6, 0, 0));
                initPos = false; 
            }
            //tralación del esqueleto con en la pisción de la camera 
            Body.gameObject.transform.position += new Vector3(vecloctyPlayer * Time.deltaTime, 0, 0);

            cubeVisisble();

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

    void cubeVisisble()
    {
        rightHand = GetChildWithName(Body, "WristRight");
        neck = GetChildWithName(Body, "Neck");
        elbowRight = GetChildWithName(Body, "ElbowRight");
        sholderRight = GetChildWithName(Body, "ShoulderRight");

        //______ cambiar eje horizontal en los cubos visibles 
        seeHand.gameObject.transform.position = Vector3.Scale(rightHand.gameObject.transform.position, new Vector3(1, 1, -1));
        seeNeck.gameObject.transform.position = Vector3.Scale(neck.gameObject.transform.position, new Vector3(1, 1, -1));
        seeElbow.gameObject.transform.position = Vector3.Scale(elbowRight.gameObject.transform.position, new Vector3(1, 1, -1));
        seeSholder.gameObject.transform.position = Vector3.Scale(sholderRight.gameObject.transform.position, new Vector3(1, 1, -1));

        seeHand.gameObject.transform.position = seeHand.gameObject.transform.position + new Vector3(0, 0, -8);
        seeNeck.gameObject.transform.position = seeNeck.gameObject.transform.position + new Vector3(0, 0, -8);
        seeElbow.gameObject.transform.position = seeElbow.gameObject.transform.position + new Vector3(0, 0, -8);
        seeSholder.gameObject.transform.position = seeSholder.gameObject.transform.position + new Vector3(0, 0, -8);

    }
    Vector3 vector2nodesNormalice(Vector3 hand, Vector3 otherNode) 
    {
        return Vector3.Normalize(hand - otherNode); 
    }
    void changeMethod()
    {
        initRay = seeNeck.gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            methodChoose = 1;
            initRay = seeNeck.gameObject.transform.position;
            Debug.Log("Hand and Neck"); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            methodChoose = 2;
            initRay = seeElbow.gameObject.transform.position; 
            Debug.Log("Hand and Elbow");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            methodChoose = 3;
            initRay = seeSholder.gameObject.transform.position;
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
            VectorInPlain.z = HandToNeck.z;
        }
        if (methodChoose == 2) 
        {
            VectorInPlain.x = HandToElbow.x;
            VectorInPlain.y = HandToElbow.y;
            VectorInPlain.z = HandToElbow.z;

        }
        if (methodChoose == 3)
        {
            VectorInPlain.x = HandToSholder.x;
            VectorInPlain.y = HandToSholder.y;
            VectorInPlain.z = HandToSholder.z;
        }
        //ejes al reves
        //VectorInPlain = rightHand.gameObject.transform.position;
        //VectorInPlain = Vector3.Scale(VectorInPlain, new Vector3(-1, 1, 1));
        Vector3 pointRightSide = rayWall(VectorInPlain);
        //Debug.Log(pointRightSide); 

        posPlayersum = Hada.gameObject.transform.position;
        posPlayersum.x = lerp(posPlayersum, pointRightSide, 0.01f).x; 
        posPlayersum.y = lerp(posPlayersum, pointRightSide, 0.01f).y;

        //Debug.Log(VectorInPlain);
        playerDepth(); 
        //only right
        //Debug.Log(posPlayersum); 
        Hada.gameObject.transform.position = posPlayersum;
	}

    //si el vector director señala el lado left, el player se situe a la profundidad del left y lo mismo con el right
    void playerDepth()
    {
		if (VectorInPlain.z < 0)
            posPlayersum.z = leftside.gameObject.transform.position.z;
        else
            posPlayersum.z = rightside.gameObject.transform.position.z;

    }
    //interpolación lineal a: actual position, b: future position, f: interval position
    Vector3 lerp(Vector3 a, Vector3 b, float f)
    {
        return a * (1 - f) + b * f;
    }

    //ray intersaccion wall, return position. If not interation wall, future position is hand 
    Vector3 rayWall(Vector3 VectorInPlain) 
    {
        RaycastHit hit;

        if (Physics.Raycast(initRay, Vector3.Scale(VectorInPlain, new Vector3(1, 1, -1)), out hit, range))
        {
            if (hit.collider.name == "RightSide" || hit.collider.name == "LeftSide")
            {
                return hit.point;
            }
            return new Vector3(seeSholder.gameObject.transform.position.x, seeSholder.gameObject.transform.position.y, rightside.gameObject.transform.position.z);
        }

        return new Vector3(seeSholder.gameObject.transform.position.x, seeSholder.gameObject.transform.position.y, rightside.gameObject.transform.position.z);

    }
    //Draw ray 
	private void OnDrawGizmos()
	{
        if (GameObject.Find("Body_Person") != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(initRay,Vector3.Scale(VectorInPlain, new Vector3(1,1,-1)) * range);
        }
	}

}
