using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (FindName(other.gameObject, "Chest"))
        {
            Transform trans = other.transform;
            Transform child = trans.FindChild(name);
            Animator anim = child.gameObject.GetComponent<Animator>();
            /*if (anim != null)
                anim.Play("Animated PBR Chest _Opening_UnCommon");*/
            Debug.Log("True");

        }

    }

    private bool FindName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform child = trans.FindChild(name);
        if (child)
            return true;
        else
            return false;
    }
}
