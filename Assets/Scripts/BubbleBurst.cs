using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBurst : MonoBehaviour
{
    public bool destroy = false;
    public bool destroyed = false;
    void Update()
    {
        if (destroyed)
            Destroy(this.gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            destroy = true;
    }

}
