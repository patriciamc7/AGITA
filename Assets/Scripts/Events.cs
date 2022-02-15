using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //Chest event
        if (other.gameObject.CompareTag("FloorChest"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Chest");
            ParticleSystem fireflies = other.gameObject.GetComponentInChildren<ParticleSystem>();
            if (child != null)
            {
                Animator anim = child.GetComponent<Animator>();
                if (anim != null)
                    anim.Play("Animated PBR Chest _Opening_UnCommon");
                if (fireflies != null)
                    fireflies.Play();
            }
        }
        //Fountain event
        if (other.gameObject.CompareTag("FloorFountain"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Fountain");
            ParticleSystem water = other.gameObject.GetComponentInChildren<ParticleSystem>();
            if (child != null)
            {
                if (water != null)
                    water.Play();
            }
        }
        
    }

}
