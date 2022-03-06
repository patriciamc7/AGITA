using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int numElements;
    public int depth;
    public GameObject[] modules;

    void Start()
    {
       for (int i = 0; i < numElements; i++)
            {
                for (int j = 0; j < depth; j++)
                {
                    Vector3 rightPosition = new Vector3(2 * i, 0, -1-2*j);
                    Instantiate(modules[j], rightPosition, Quaternion.identity);
                    Vector3 leftPosition = new Vector3(2 * i, 0, 1+2*j);
                    Instantiate(modules[j], leftPosition, Quaternion.Euler(Vector3.up * 180));
                }
            }
       
    }

}
