using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour
{
    public GameObject[] objects;
    public bool is_rot = true;
    private Quaternion rot = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        if (is_rot)
            rot = Quaternion.Euler(Vector3.up * (Random.Range(0, 4) * 90));
        Instantiate(objects[Random.Range(0, objects.Length)], transform.position, rot);

    }

 
}
