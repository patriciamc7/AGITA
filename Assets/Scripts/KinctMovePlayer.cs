using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinctMovePlayer : MonoBehaviour
{
    public GameObject CameraRight;
    //public GameObject coll;
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
    float range = 100f;

    public GameObject seeHand; 
    public GameObject seeNeck; 
    public GameObject seeElbow; 
    public GameObject seeSholder;
    public GameObject pvisible; 
    private Vector3 initRay;

    public GameObject rightside; 
    public GameObject leftside;
    private bool init_value = true;
    private Vector3 Pos_i;
    private bool ini1 = true; 
    //private Vector3 auxpos = new Vector3(0,0,0);
    private GameObject vagon; 
    private Vector3 pointtSideWall;

	
	// Update is called once per frame
	void Update()
    {
        
        //If Body detected assign Kineckt gameobject
        if (GameObject.Find("Body_Person") != null)
        {
            Body = GameObject.Find("Body_Person");
            Body.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            vagon = GameObject.Find("vagon"); 
            Body.gameObject.transform.SetParent(vagon.gameObject.transform);
            if (ini1) {
                Body.gameObject.transform.Rotate(0, 180, 0);
                Body.gameObject.transform.Translate(0,0.6f,0f);
                ini1 = false; 
            }

            cubeVisisble();

			HandToNeck = vector2nodesNormalice(seeHand.gameObject.transform.position, seeNeck.gameObject.transform.position);
            HandToElbow = vector2nodesNormalice(seeHand.gameObject.transform.position, seeElbow.gameObject.transform.position);
            HandToSholder = vector2nodesNormalice(seeHand.gameObject.transform.position, seeSholder.gameObject.transform.position);

            moveCharater();

            knowVelocity();
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

		//______ cambiar simetria del que detecta la kinect a els nodes del esquelet pasa x y z
		seeNeck.gameObject.transform.position = new Vector3(neck.gameObject.transform.position.z, neck.gameObject.transform.position.y, neck.gameObject.transform.position.x);
		seeHand.gameObject.transform.position= new Vector3(rightHand.gameObject.transform.position.z, rightHand.gameObject.transform.position.y, rightHand.gameObject.transform.position.x);
		seeElbow.gameObject.transform.position= new Vector3(elbowRight.gameObject.transform.position.z, elbowRight.gameObject.transform.position.y, elbowRight.gameObject.transform.position.x);
		seeSholder.gameObject.transform.position= new Vector3(sholderRight.gameObject.transform.position.z, sholderRight.gameObject.transform.position.y, sholderRight.gameObject.transform.position.x);
		// traslladem a on esta el coll tots respectivament
        //pvisible.gameObject.transform.Translate(seeNeck.gameObject.transform.position); 
        //trasladar el esqueleto que detecta la kinect y a consecuencia se mueve el persona visible 
        Body.gameObject.transform.position +=new Vector3(0,0, vecloctyPlayer * Time.deltaTime);
       
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
        //Debug.Log(VectorInPlain);

        //ejes al reves
        pointtSideWall = rayWall(VectorInPlain);
        //pointtSideWall += new Vector3(vecloctyPlayer * Time.deltaTime, 0, 0);
        //coll.gameObject.transform.position = pointtSideWall;

        posPlayersum = Hada.gameObject.transform.position;
        posPlayersum.x = Vector3.Lerp(posPlayersum, pointtSideWall, 0.1f).x; 
        posPlayersum.y = Vector3.Lerp(posPlayersum, pointtSideWall, 0.1f).y;

        playerDepth(); 
        Hada.gameObject.transform.position = posPlayersum;
        //Hada.gameObject.transform.Translate(vecloctyPlayer * Time.deltaTime, 0, 0); 
    }

    //si el vector director señala el lado left, el player se situe a la profundidad del left y lo mismo con el right
    void playerDepth()
    {
        BoxCollider hada_Collider = Hada.GetComponent<BoxCollider>();

        if (VectorInPlain.z > 0)
        {
            posPlayersum.z = leftside.gameObject.transform.position.z;
            hada_Collider.center = new Vector3(0,0,7); 
        }
        else
        {
            posPlayersum.z = rightside.gameObject.transform.position.z;
            hada_Collider.center = new Vector3(0, 0, -7);

        }

    }
    ////interpolación lineal a: actual position, b: future position, f: interval position
    //Vector3 lerp(Vector3 a, Vector3 b, float f)
    //{
    //    return a * (1 - f) + b * f;
    //}

    //ray intersaccion wall, return position. If not interation wall, future position is hand 
    Vector3 rayWall(Vector3 VectorInPlain) 
    {
        RaycastHit hit;
        Vector3 aux; 
        if (Physics.Raycast(initRay, VectorInPlain, out hit, range))
        {
            if (hit.collider.name == "RightSide" || hit.collider.name == "LeftSide")
            {
                aux = hit.point; 
               
                return aux;
            }
        }
        
        aux = Vector3.Lerp(seeHand.gameObject.transform.position, pointtSideWall, 0.9f);
        return aux; 
        //return seeNeck.gameObject.transform.position; 
        //auxpos += new Vector3(vecloctyPlayer * Time.deltaTime, 0, 0);

        //return pointtSideWall;
    }
    //Draw ray 
	private void OnDrawGizmos()
	{
        if (GameObject.Find("Body_Person") != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(initRay,VectorInPlain * range);
        }
	}

    void knowVelocity()
    {
        if (init_value)
        {
            Pos_i = pointtSideWall;
            init_value = false;
        }
        else {
           
            Vector3 Delta_pos = pointtSideWall - Pos_i;

            Vector3 velocity_Hand = Delta_pos ;
            //Debug.Log(velocity_Hand); 
            float modulo = Mathf.Sqrt(Mathf.Pow(velocity_Hand.x, 2) + Mathf.Pow(velocity_Hand.y, 2) + Mathf.Pow(velocity_Hand.z, 2)); 
            //Debug.Log(modulo);
            if (modulo > 0.1)
            {
                Debug.Log("Hacer animacion");
            }
            Pos_i = pointtSideWall;

        }
    }
}
