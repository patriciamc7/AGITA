using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour
{
    public GameObject[] objects;
    [Range(0, 1)]
    public float prob = 0.75f;
    public bool prob_rot = true;
    private Quaternion rot = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0f,1f)<= prob)
        {
            if (prob_rot)
                rot = Quaternion.Euler(Vector3.up * (Random.Range(0, 4) * 90));
            Instantiate(objects[Random.Range(0, objects.Length)], transform.position, rot);

        }
    }

 
}
