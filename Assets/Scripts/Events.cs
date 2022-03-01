using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public Material material;
    public Vector3 scale;
    public void OnTriggerEnter(Collider other)
    {
        //Chest event
        if (other.gameObject.CompareTag("FloorChest"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Chest");
            ParticleSystem fireflies = other.gameObject.GetComponentInChildren<ParticleSystem>();

            Transform child2 = child.transform.Find("Base");
            ParticleSystem light = child2.gameObject.GetComponentInChildren<ParticleSystem>();
            if (child != null)
            {
                Animator anim = child.GetComponent<Animator>();
                if (anim != null)
                    anim.Play("Animated PBR Chest _Opening_UnCommon");
                if (fireflies != null)
                    fireflies.Play();
                if (light != null)
                {
                    StartCoroutine(DelayedAnimation(light, 0.9f));
                }
                    
            }
        }
        //Fountain event
        if (other.gameObject.CompareTag("FloorFountain"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Fountain");
            ParticleSystem[] water = other.gameObject.GetComponentsInChildren<ParticleSystem>();
            if (child != null)
            {
                for (int i = 0; i < water.Length; i++)
                {
                    water[i].Play();
                }

            }
        }

        //Crystal event
        if (other.gameObject.CompareTag("FloorCrystal"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Cristal");

            if (child != null)
            {
                Transform mesh = this.gameObject.transform.Find("mesh");
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = material;

            }
        }

        //Mushroom event
        if (other.gameObject.CompareTag("FloorMushroom"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Bolets");

            if (child != null)
            {
                child.gameObject.transform.localScale = new Vector3(scale.x *Time.deltaTime , scale.y *Time.deltaTime , scale.z *Time.deltaTime );
                //Animator anim = child.GetComponent<Animator>();
                //if (anim != null)
                //    anim.Play("Animated PBR Chest _Opening_UnCommon");

            }
        }
        //Fireflies event
        if (other.gameObject.CompareTag("FloorFireflies"))
        {
            Transform trans = other.transform;
            ParticleSystem fireflies = other.gameObject.GetComponentInChildren<ParticleSystem>();

            if (fireflies != null)
            {
                fireflies.gameObject.transform.position = this.gameObject.transform.position;
                Object.Destroy(fireflies, 5.0f);

            }
        }

        //Bubble event
        if (other.gameObject.CompareTag("FloorBubbles"))
        {
            Transform trans = other.transform;
            ParticleSystem bubbles = other.gameObject.GetComponentInChildren<ParticleSystem>();
           
            if (bubbles != null)
            {
                var subEmittersModule = bubbles.subEmitters;
                subEmittersModule.enabled = true;
            }
        }

    }

    
    // The delay coroutine
    public IEnumerator DelayedAnimation(ParticleSystem ps, float time)
    {
        yield return new WaitForSeconds(time);
        ps.Play();
    }
}