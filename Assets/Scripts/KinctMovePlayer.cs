using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinctMovePlayer : MonoBehaviour
{
    public GameObject CameraRight;
    //public float USER_DESP_X = ; 
    //public float USER_DESP_Y;
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
    //private Vector3 VectorInPlain  = new Vector3(0,0,2.92f);
    private Vector3 posPlayersum;
    public movimentVagon movimentVagon;
    //public float vecloctyPlayer;
    float range = 20f;

    public GameObject seeHand; 
    public GameObject seeNeck; 
    public GameObject seeElbow; 
    public GameObject seeSholder;
    public GameObject pvisible; 
    private Vector3 initRay;

    //public GameObject rightside; 
    //public GameObject leftside;
    private bool init_value = true;
    private Vector3 Pos_i;
    private bool ini1 = true; 
    //private Vector3 auxpos = new Vector3(0,0,0);
    private GameObject vagon; 
    private Vector3 pointtSideWall;

    public NotExitCollider ScriptRebotar;
    public GameObject cubePoint; 
    // Update is called once per frame
    void Update()
    {
        //If Body detected assign Kineckt gameobject
        if (GameObject.Find("Body_Person") != null)
        {
            esqueleto(); 

            moveCharater();

            changeDirection(); 

            knowVelocity();
        }

    }

    //guardar nodo del esqueleto a variable 
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
    void esqueleto()
    {
        Body = GameObject.Find("Body_Person");
        //Body.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        vagon = GameObject.Find("vagon");
        //Body.gameObject.transform.SetParent(vagon.gameObject.transform);
        if (ini1)
        {
            Body.gameObject.transform.Rotate(0, 180, 0);
            Body.gameObject.transform.Translate(0, 0.6f, 0f);
            ini1 = false;
        }
        //vagon.gameObject.transform.position += new Vector3(movimentVagon.velocitycamera * Time.deltaTime, 0, 0);

        cubeVisisble();

    }
    void cubeVisisble()
    {
        rightHand = GetChildWithName(Body, "WristRight");
        neck = GetChildWithName(Body, "Neck");
        elbowRight = GetChildWithName(Body, "ElbowRight");
        sholderRight = GetChildWithName(Body, "ShoulderRight");

		//______ cambiar simetria del que detecta la kinect a els nodes del esquelet pasa x y z
		seeNeck.gameObject.transform.position = new Vector3(neck.gameObject.transform.position.z +vagon.gameObject.transform.position.x, neck.gameObject.transform.position.y, neck.gameObject.transform.position.x);
		seeHand.gameObject.transform.position = new Vector3(rightHand.gameObject.transform.position.z + vagon.gameObject.transform.position.x, rightHand.gameObject.transform.position.y, rightHand.gameObject.transform.position.x);
		seeElbow.gameObject.transform.position = new Vector3(elbowRight.gameObject.transform.position.z + vagon.gameObject.transform.position.x, elbowRight.gameObject.transform.position.y, elbowRight.gameObject.transform.position.x);
		seeSholder.gameObject.transform.position = new Vector3(sholderRight.gameObject.transform.position.z + vagon.gameObject.transform.position.x, sholderRight.gameObject.transform.position.y, sholderRight.gameObject.transform.position.x);

		// traslladem a on esta el coll tots respectivament
		//pvisible.gameObject.transform.Translate(seeNeck.gameObject.transform.position); 
		//trasladar el esqueleto que detecta la kinect y a consecuencia se mueve el persona visible 
		//Body.gameObject.transform.position +=new Vector3(0,0, cameraMovement.velocitycamera * Time.deltaTime);

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
        switch (methodChoose)
        {

            case 1:
                    HandToNeck = vector2nodesNormalice(seeHand.gameObject.transform.position, seeNeck.gameObject.transform.position);
                    rayWall(HandToNeck);
                    break;
            case 2:
                    HandToElbow = vector2nodesNormalice(seeHand.gameObject.transform.position, seeElbow.gameObject.transform.position);
                    rayWall(HandToElbow);
                    break;
            case 3:
                    HandToSholder = vector2nodesNormalice(seeHand.gameObject.transform.position, seeSholder.gameObject.transform.position);
                    rayWall(HandToSholder);
                    break; 
        }
        //Debug.Log(VectorInPlain);

        //ejes al reves
        //rayWall(VectorInPlain); 

        //playerDepth(); 

    }


    void rayWall(Vector3 VectorInPlain) 
    {
        Vector3 aux;

        posPlayersum = Hada.gameObject.transform.position;

        BoxCollider hada_Collider = Hada.GetComponent<BoxCollider>();


        //aux.z = 2 * seeNeck.gameObject.transform.position.z + 1 ;

        //Debug.Log(VectorInPlain); 
        if (VectorInPlain.z > 0) //lado left
        {
            aux.y = seeNeck.gameObject.transform.position.y + VectorInPlain.y * (seeNeck.gameObject.transform.position.z + 1) / VectorInPlain.z;
            aux.x = seeNeck.gameObject.transform.position.x + VectorInPlain.x * (seeNeck.gameObject.transform.position.z + 1) / VectorInPlain.z;
            aux.z = 2 * seeNeck.gameObject.transform.position.z + 1 ;

            hada_Collider.center = new Vector3(0, 0, -7);
        }
        else //lado  right
        {
            aux.x = seeNeck.gameObject.transform.position.x + VectorInPlain.x * (seeNeck.gameObject.transform.position.z - 1) / VectorInPlain.z;
            aux.y = seeNeck.gameObject.transform.position.y + VectorInPlain.y * (seeNeck.gameObject.transform.position.z - 1) / VectorInPlain.z;
            aux.z = 2 * seeNeck.gameObject.transform.position.z - 1 ;

            hada_Collider.center = new Vector3(0, 0, 7);
        }
        pointtSideWall = aux;
        cubePoint.gameObject.transform.position = aux; 
        //revotar saltaria poner centro del collider 

        if (ScriptRebotar.Bolexit )
        {
            pointtSideWall = seeSholder.gameObject.transform.position; 
        }
        
        posPlayersum.x = Vector3.Lerp(posPlayersum, pointtSideWall, 0.1f).x;
        posPlayersum.y = Vector3.Lerp(posPlayersum, pointtSideWall, 0.1f).y;
        posPlayersum.z = aux.z;
        Hada.gameObject.transform.position = posPlayersum;
    }
	//Draw ray 
	private void OnDrawGizmos()
	{
		if (GameObject.Find("Body_Person") != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(initRay, HandToNeck * range);
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
                //Debug.Log("Hacer animacion");
            }
            Pos_i = pointtSideWall;

        }
    }
    void changeDirection()
    {
        Vector3 dir = pointtSideWall - Hada.gameObject.transform.position;

        if (dir.x < 0)
            Hada.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);     
        else
            Hada.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

    }
}
