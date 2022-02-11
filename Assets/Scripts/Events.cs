using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Transform trans = other.transform;
        Transform child = trans.Find("Chest");
        ParticleSystem Fireflies = other.gameObject.GetComponentInChildren<ParticleSystem>();
        if (child != null)
        {
            Animator anim = child.GetComponent<Animator>();
            if (anim != null)
                anim.Play("Animated PBR Chest _Opening_UnCommon");
            if (Fireflies != null)
                Fireflies.Play();
        }
    }

}
