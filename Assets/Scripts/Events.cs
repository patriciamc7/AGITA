using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Events : MonoBehaviour
{
    public Material material;
    public Material materialPink;
    public Material materialGreen;
    public Material materialBlue;

    //public Material trailLight;
    //public Material trailLightPink;
    //public Material trailLightGreen;
    //public Material trailLightBlue;

    //public Material trailDark;
    //public Material trailDarkPink;
    //public Material trailDarkGreen;
    //public Material trailDarkBlue;


    public Vector3 scale;
    public GameObject firefliesModule;
    public GameObject butterfly;

    private ParticleSystem fireflies = null;
    private bool isFlower = false;
    private GameObject flower;
    void Update()
    {
        if (fireflies!=null)
        {
            fireflies.transform.position = Vector3.Lerp(fireflies.transform.position, this.transform.position, 0.05f);
            Object.Destroy(fireflies.gameObject, 5.0f);
        }
        if(flower != null)
        {
            flower.transform.position = Vector3.Lerp(flower.transform.position, this.transform.position, 0.05f);
        }
       
    }
    public void OnTriggerEnter(Collider other)
    {

        //Chest event
        if (other.gameObject.CompareTag("Chest"))
        {
            Debug.Log("entra");
           ParticleSystem fireflies = other.transform.parent.GetComponentInChildren<ParticleSystem>();

            Transform child = other.transform.Find("Base");
            ParticleSystem light = child.gameObject.GetComponentInChildren<ParticleSystem>();
            if (other != null)
            {
                other.GetComponent<AudioSource>().Play();
                Animator anim = other.GetComponent<Animator>();
                if (anim != null)
                    anim.Play("Animated PBR Chest _Opening_UnCommon");
                if (fireflies != null)
                {
                    fireflies.GetComponent<AudioSource>().Play();
                    fireflies.Play();
                }
                if (light != null)
                    StartCoroutine(DelayedAnimation(light, 0.8f));
            }
        }

        //Fountain event
        if (other.gameObject.CompareTag("fountain"))
        {
            ParticleSystem[] water = other.transform.parent.GetComponentsInChildren<ParticleSystem>();
            if (water != null)
            {
                for (int i = 0; i < water.Length; i++)
                {
                    water[i].Play();
                }
                other.GetComponent<AudioSource>().Play();
            }
        }

        //Crystal event
        Transform mesh = this.transform.Find("mesh");
        Transform wingLeft = mesh.Find("wing_left");
        Transform wingRight = mesh.Find("wing_left1");
        Transform TrailLight = this.transform.Find("TrailLight");
        Transform TrailDark = TrailLight.transform.Find("TrailDark");
        ParticleSystem[] Trail = TrailLight.gameObject.GetComponentsInChildren<ParticleSystem>();
        VisualEffect[] Effects = TrailLight.gameObject.GetComponentsInChildren<VisualEffect>();

        if (other.gameObject.CompareTag("WhiteCrystal"))
        {
            if (other != null)
            {
                other.gameObject.GetComponent<AudioSource>().Play();
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = material;
                if (wingLeft != null)
                    wingLeft.gameObject.GetComponent<Renderer>().material = material;
                if (wingRight != null)
                    wingRight.gameObject.GetComponent<Renderer>().material = material;
                if (TrailLight != null)
                {
                    for (int i = 0; i < Trail.Length; i++)
                    {
                        if (i == 0) {
                            Effects[i].Play();
                            Trail[i].Play();
                        }
                        else
                        {
                            Trail[i].Stop();
                            Effects[i].Stop();
                        }
                    }
                }
                
            }
        }
        if (other.gameObject.CompareTag("PinkCrystal"))
        {
            if (other != null)
            {
                other.gameObject.GetComponent<AudioSource>().Play();
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = materialPink;
                if (wingLeft != null)
                    wingLeft.gameObject.GetComponent<Renderer>().material = materialPink;
                if (wingRight != null)
                    wingRight.gameObject.GetComponent<Renderer>().material = materialPink;
                if (TrailLight != null)
                {
                    for (int i = 0; i < Trail.Length; i++)
                    {
                        if (i == 1)
                        {
                            Effects[i].Play();
                            Trail[i].Play();
                        }
                        else { 
                            Trail[i].Stop();
                            Effects[i].Stop();
                        }
                    }
                }


            }
        }
        if (other.gameObject.CompareTag("GreenCrystal"))
        {
            if (other != null)
            {
                other.gameObject.GetComponent<AudioSource>().Play();
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = materialGreen;
                if (wingLeft != null)
                    wingLeft.gameObject.GetComponent<Renderer>().material = materialGreen;
                if (wingRight != null)
                    wingRight.gameObject.GetComponent<Renderer>().material = materialGreen;
                if (TrailLight != null)
                {
                    //TrailLight.GetComponent<Renderer>().material = trailLightGreen;
                    //TrailDark.GetComponent<Renderer>().material = trailDarkGreen;
                    for (int i = 0; i < Trail.Length; i++)
                    {
                        if (i == 2)
                        {
                            Effects[i].Play();
                            Trail[i].Play();
                        }
                        else
                        {
                            Trail[i].Stop();
                            Effects[i].Stop();
                        }
                    }
                }

            }
        }
        if (other.gameObject.CompareTag("BlueCrystal"))
        {
           if (other != null)
           {
                other.gameObject.GetComponent<AudioSource>().Play();
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = materialBlue;
                if (wingLeft != null)
                    wingLeft.gameObject.GetComponent<Renderer>().material = materialBlue;
                if (wingRight != null)
                    wingRight.gameObject.GetComponent<Renderer>().material = materialBlue;
                if (TrailLight != null)
                {
                    //TrailLight.GetComponent<Renderer>().material = trailLightBlue;
                    //TrailDark.GetComponent<Renderer>().material = trailDarkBlue;
                    for (int i = 0; i < Trail.Length; i++)
                    {
                        if (i == 3)
                        {
                            Effects[i].Play();
                            Trail[i].Play();
                        }
                        else
                        {
                            Trail[i].Stop();
                            Effects[i].Stop();
                        }
                    }
                }
            }
        }

        //Mushroom event
        if (other.gameObject.CompareTag("Mushroom"))
        {
            Transform child1 = other.transform.transform.Find("mushroom_1");
            Transform child2 = other.transform.Find("mushroom_2");

            if (child1 != null)
            {
                other.gameObject.transform.localScale = new Vector3(scale.x *Time.deltaTime , scale.y *Time.deltaTime , scale.z *Time.deltaTime );
                //Animator anim = child.GetComponent<Animator>();
                //if (anim != null)
                //    anim.Play("Animated PBR Chest _Opening_UnCommon");

            }
            else if(child2 != null)
            {

            }
        }

        //Fountain effects event
        if (other.gameObject.CompareTag("FountainEffect"))
        {
            ParticleSystem water = other.gameObject.GetComponentInChildren<ParticleSystem>();
            water.Play();
            other.GetComponent<AudioSource>().Play();
        }

        //Butterfly event
        if (other.gameObject.CompareTag("Orb"))
        {
            if (isFlower)
            {
                other.GetComponent<AudioSource>().Play();
                ParticleSystem[] orbs = other.gameObject.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < orbs.Length; i++)
                {
                    orbs[i].transform.localScale = new Vector3(2, 2, 2);
                }

                GameObject butterflyInstance = other.transform.parent.Find("Butterfly").gameObject;
                butterflyInstance.SetActive(true);
                butterflyInstance.GetComponent<Animation>().Play();

                Destroy(flower);
                isFlower = false;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Fireflies event
        if (other.gameObject.CompareTag("Fireflies"))
        {
            fireflies = other.transform.parent.GetComponentInChildren<ParticleSystem>();
        }
        //Flower event
        if (other.gameObject.CompareTag("Flower"))
        {
            if (!isFlower)
            {
                isFlower = true;
                flower = other.gameObject;
            }

        }
    }

    //The delay coroutine
    public IEnumerator DelayedAnimation(ParticleSystem ps, float time)
    {
        yield return new WaitForSeconds(time);
        ps.Play();
    }
}