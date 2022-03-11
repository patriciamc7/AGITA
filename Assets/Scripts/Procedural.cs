using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour
{
    public GameObject[] objects;
    public bool isRot = false;
    public bool isLeft = false;
    private Quaternion rot = Quaternion.identity;

    void Start()
    {
        if (isRot)
            rot = Quaternion.Euler(Vector3.up * (Random.Range(0, 4) * 90));
        if (isLeft)
            rot = Quaternion.Euler(Vector3.up * 180);
        GameObject floor = Instantiate(objects[Random.Range(0, objects.Length)], transform.position, rot);
        floor.transform.SetParent(this.transform);

    }
}
