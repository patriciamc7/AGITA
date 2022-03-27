using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour
{
    public GameObject[] objects;
    public bool isRot = false;
    public bool isLeft = false;
    private Quaternion rot = Quaternion.identity;
    private GameObject floor;
    //private Camera LeftCamera;
    //private Camera RightCamera;
    //private float widthThresold = 3f;
    void Start()
    {
        if (isRot)
            rot = Quaternion.Euler(Vector3.up * (Random.Range(0, 4) * 90));
        if (isLeft)
            rot = Quaternion.Euler(Vector3.up * 180);
        floor = Instantiate(objects[Random.Range(0, objects.Length)], transform.position, rot);
        floor.transform.SetParent(this.transform);

    }
    
    //void Update()
    //{
    //    Vector2 screenPositionL = LeftCamera.WorldToScreenPoint(transform.position);
    //    Vector2 screenPositionR = RightCamera.WorldToScreenPoint(transform.position);
    //    if (screenPositionL.x < widthThresold && screenPositionR.x < widthThresold)
    //        Destroy(floor);
    //}

}
