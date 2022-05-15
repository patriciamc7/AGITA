using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinctMovePlayer : MonoBehaviour
{
    public GameObject CameraRight;
    public Player playerStript; 
    public bool Bolexit = false;
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

    private Vector3 posPlayersum;
    public movimentVagon movimentVagon;

    float range = 20f;

    public GameObject seeHand; 
    public GameObject seeNeck; 
    public GameObject seeElbow; 
    public GameObject seeSholder;
    public GameObject pvisible; 
    private Vector3 initRay;
    private float velocity = 0;

    private bool init_value = true;
    private Vector3 Pos_i;

    private GameObject vagon; 
    private Vector3 pointtSideWall;

    public GameObject cubePoint;

    public float speedPlayer;
    private float oldvelocity = 0; 
    private bool OneTime = true;
    private float timeAnimation = 0;
    private float posYold = 0; 
    private float posXold = 0;

    private float timeMortal;
    public bool mortalBool = false;
    public Vector3 desplacamentKinect;

    private Vector4 rectangleLeft;  //x1, y1, x2, y2
    private Vector4 rectangleRight;  //x1, y1, x2, y2

    public Vector4 rectangleLeftInit; // = new Vector4(-2, 2, -0.49f, 1.51f);  //x1, y1, x2, y2
    public Vector4 rectangleRightInit; // = new Vector4(-1.1f, 1.1f, -0.33f, 1.07f);  //x1, y1, x2, y2

    private Vector4 coodRectangle; 
    // Update is called once per frame
    void Update()
    {
        //If Body detected assign Kineckt gameobject
        if (GameObject.Find("Body_Person") != null)
        {
            esqueleto(); 

            moveCharater();
            SalMortal(); 

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
        Body.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        vagon = GameObject.Find("vagon");
       
        cubeVisisble();

    }
    void cubeVisisble()
    {
        rightHand = GetChildWithName(Body, "WristRight");
        neck = GetChildWithName(Body, "Neck");
        elbowRight = GetChildWithName(Body, "ElbowRight");
        sholderRight = GetChildWithName(Body, "ShoulderRight");

        //______ cambiar simetria del que detecta la kinect a els nodes del esquelet pasa x y z
        
        seeNeck.gameObject.transform.position = new Vector3((-1.0f * neck.gameObject.transform.position.z) + desplacamentKinect.x + vagon.gameObject.transform.position.x, neck.gameObject.transform.position.y + desplacamentKinect.y, neck.gameObject.transform.position.x * -1.0f + desplacamentKinect.z);
        seeHand.gameObject.transform.position = new Vector3((-1.0f * rightHand.gameObject.transform.position.z) + desplacamentKinect.x + vagon.gameObject.transform.position.x, rightHand.gameObject.transform.position.y + desplacamentKinect.y, rightHand.gameObject.transform.position.x * -1.0f + desplacamentKinect.z);
        seeElbow.gameObject.transform.position = new Vector3((-1.0f * elbowRight.gameObject.transform.position.z) + desplacamentKinect.x + vagon.gameObject.transform.position.x, elbowRight.gameObject.transform.position.y + desplacamentKinect.y, elbowRight.gameObject.transform.position.x * -1.0f + desplacamentKinect.z);
        seeSholder.gameObject.transform.position = new Vector3((-1.0f * sholderRight.gameObject.transform.position.z) + desplacamentKinect.x + vagon.gameObject.transform.position.x, sholderRight.gameObject.transform.position.y + desplacamentKinect.y, sholderRight.gameObject.transform.position.x * -1.0f + desplacamentKinect.z);

        //move x collider (rectangle) left
        rectangleLeft = rectangleLeftInit; 
        rectangleLeft.x = rectangleLeftInit.x + vagon.gameObject.transform.position.x; 
        rectangleLeft.z = rectangleLeftInit.z + vagon.gameObject.transform.position.x;

        //move x collider (rectangle) right
        rectangleRight = rectangleRightInit; 
        rectangleRight.x = rectangleRightInit.x + vagon.gameObject.transform.position.x;
        rectangleRight.z = rectangleRightInit.z + vagon.gameObject.transform.position.x;

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
       
    }


    void rayWall(Vector3 VectorInPlain) 
    {
        Vector3 aux;

        posPlayersum = Hada.gameObject.transform.position;

        if (VectorInPlain.z > 0) //lado left
        {
            aux.y = seeNeck.gameObject.transform.position.y + VectorInPlain.y * (seeNeck.gameObject.transform.position.z + 1) / VectorInPlain.z;
            aux.x = seeNeck.gameObject.transform.position.x + VectorInPlain.x * (seeNeck.gameObject.transform.position.z + 1) / VectorInPlain.z;
            aux.z = 2 * seeNeck.gameObject.transform.position.z + 1 ;

            coodRectangle = rectangleLeft; 
            changeDirection(true);
           
        }
        else //lado  right
        {
            aux.x = seeNeck.gameObject.transform.position.x + VectorInPlain.x * (seeNeck.gameObject.transform.position.z - 1) / VectorInPlain.z;
            aux.y = seeNeck.gameObject.transform.position.y + VectorInPlain.y * (seeNeck.gameObject.transform.position.z - 1) / VectorInPlain.z;
            aux.z = 2 * seeNeck.gameObject.transform.position.z - 1 ;
            coodRectangle = rectangleRight;

            changeDirection();
            
        }
        pointtSideWall = aux;
        cubePoint.gameObject.transform.position = aux;
        //revotar saltaria poner centro del collider 
        //Debug.Log(rectangleRight.y +" " + rectangleRight.w + "POINT"+ aux); 
        //Debug.Log(FindPoint(coodRectangle, cubePoint.gameObject.transform.position));


        if (!FindPoint(coodRectangle, cubePoint.gameObject.transform.position) || playerStript.collitionObjectsPlayer)
        {
            pointtSideWall = new Vector3(seeSholder.gameObject.transform.position.x, seeSholder.gameObject.transform.position.y, aux.z); 
        }
        
        posPlayersum.x = Vector3.Lerp(posPlayersum, pointtSideWall, 0.1f).x;
        posPlayersum.y = Vector3.Lerp(posPlayersum, pointtSideWall, 0.1f).y;
        posPlayersum.z = aux.z;
        Hada.gameObject.transform.position = posPlayersum;
        if (OneTime && Time.time <5)
        {
            Hada.gameObject.transform.position = new Vector3 (seeSholder.transform.position.x, seeSholder.transform.position.y , -1);
            OneTime = false;
        }

    }
    private void SalMortal() 
    {
        
        float difY = (Hada.gameObject.transform.position.y - posYold)*1000; 
        float difX = (Hada.gameObject.transform.position.x - posXold)*1000;
        timeMortal += 1;
        //Debug.Log("Y "+ difY +"X "+difX); 
        if (difY > 10 && difX >30 && timeMortal>2)
        {
            Debug.Log(mortalBool);
            mortalBool = true; 
        }
        if (mortalBool && timeMortal > 3)
        {
            timeMortal = 0;
            mortalBool = false;

        }

        posYold = Hada.gameObject.transform.position.y;
        posXold = Hada.gameObject.transform.position.x;

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
            float modulo = Mathf.Sqrt(Mathf.Pow(velocity_Hand.x, 2) + Mathf.Pow(velocity_Hand.y, 2) + Mathf.Pow(velocity_Hand.z, 2));
                        
            //speedPlayer = modulo;
            velocity = modulo - oldvelocity; 
            //Debug.Log(velocity +" "+speedPlayer);
            timeAnimation += 1 * Time.deltaTime ;
            if (timeAnimation > 3.0f) { 
                if (Mathf.Abs(velocity) < 1)
                    if(velocity >= 0.05) { 
                        speedPlayer = 0; // rápida animacion
                        timeAnimation = 0;
                    }
                    else
                         speedPlayer = 1; // lenta animacion
            }
            Pos_i = pointtSideWall;

            oldvelocity = modulo; 

        }
    }
    void changeDirection( bool left = false)
    {
		Vector3 dir = pointtSideWall - Hada.gameObject.transform.position;
        BoxCollider hada_Collider = Hada.GetComponent<BoxCollider>();

        if (dir.x < 0)
        { //atras 
            Hada.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
            if (left)
                hada_Collider.center = new Vector3(3, 0.49f, 0);
            else
                hada_Collider.center = new Vector3(-3, 0.49f, 0);


        }
        else
		{ //delante
			Hada.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            if (left)
                hada_Collider.center = new Vector3(-3, 0.49f, 0);
            else
                hada_Collider.center = new Vector3(3, 0.49f, 0);
        }

	}

    static bool FindPoint( Vector4 coodRectangle, Vector3 point)
    {
        if (point.x > coodRectangle.x && point.x < coodRectangle.z && point.y < coodRectangle.y && point.y > coodRectangle.w) 
            return true;

        return false;
    }
}
