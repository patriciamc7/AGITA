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

    public Material trailLight;
    public Material trailLightPink;
    public Material trailLightGreen;
    public Material trailLightBlue;

    public Material trailDark;
    public Material trailDarkPink;
    public Material trailDarkGreen;
    public Material trailDarkBlue;

    public Vector3 scale;
    public GameObject firefliesModule;
    private ParticleSystem fireflies = null;
   
    void Update()
    {
        if (fireflies!=null)
        {
            fireflies.transform.position = Vector3.Lerp(fireflies.transform.position, this.transform.position, 0.05f);
            Object.Destroy(fireflies.gameObject, 5.0f);
        }

    }
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
        Transform mesh = this.gameObject.transform.Find("mesh");
        Transform wingLeft = this.gameObject.transform.Find("Bip001").Find("Bip001 Pelvis").Find("Bip001 Spine").Find("Bip001 Spine1").Find("wing_left");
        Transform wingRight = this.gameObject.transform.Find("Bip001").Find("Bip001 Pelvis").Find("Bip001 Spine").Find("Bip001 Spine1").Find("wing_right");
        Transform TrailLight = this.gameObject.transform.Find("TrailLight");
        Transform TrailDark = TrailLight.gameObject.transform.Find("TrailDark");
        ParticleSystem[] Trail = TrailLight.gameObject.GetComponentsInChildren<ParticleSystem>();
        VisualEffect[] Effects = TrailLight.gameObject.GetComponentsInChildren<VisualEffect>();

        if (other.gameObject.CompareTag("FloorCrystal"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Cristal");

            if (child != null)
            {
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = material;
                if (wingLeft != null)
                    wingLeft.gameObject.GetComponent<Renderer>().material = material;
                if (wingRight != null)
                    wingRight.gameObject.GetComponent<Renderer>().material = material;
                if (TrailLight != null)
                {
                    TrailLight.GetComponent<Renderer>().material = trailLight;
                    TrailDark.GetComponent<Renderer>().material = trailDark;
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
        if (other.gameObject.CompareTag("FloorCrystalPink"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Cristal");

            if (child != null)
            {
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = materialPink;
                if (wingLeft != null)
                    wingLeft.gameObject.GetComponent<Renderer>().material = materialPink;
                if (wingRight != null)
                    wingRight.gameObject.GetComponent<Renderer>().material = materialPink;
                if (TrailLight != null)
                {
                    TrailLight.GetComponent<Renderer>().material = trailLightPink;
                    TrailDark.GetComponent<Renderer>().material = trailDarkPink;
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
        if (other.gameObject.CompareTag("FloorCrystalGreen"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Cristal");

            if (child != null)
            {
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = materialGreen;
                if (wingLeft != null)
                    wingLeft.gameObject.GetComponent<Renderer>().material = materialGreen;
                if (wingRight != null)
                    wingRight.gameObject.GetComponent<Renderer>().material = materialGreen;
                if (TrailLight != null)
                {
                    TrailLight.GetComponent<Renderer>().material = trailLightGreen;
                    TrailDark.GetComponent<Renderer>().material = trailDarkGreen;
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
        if (other.gameObject.CompareTag("FloorCrystalBlue"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Cristal");

            if (child != null)
            {
                if (mesh != null)
                    mesh.gameObject.GetComponent<Renderer>().material = materialBlue;
                if (wingLeft != null)
                    wingLeft.gameObject.GetComponent<Renderer>().material = materialBlue;
                if (wingRight != null)
                    wingRight.gameObject.GetComponent<Renderer>().material = materialBlue;
                if (TrailLight != null)
                {
                    TrailLight.GetComponent<Renderer>().material = trailLightBlue;
                    TrailDark.GetComponent<Renderer>().material = trailDarkBlue;
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
        if (other.gameObject.CompareTag("FloorMushroom"))
        {
            Transform trans = other.transform;
            Transform child = trans.Find("Bolets");
            Transform child1 = child.Find("mushroom_1");
            Transform child2 = child.Find("mushroom_2");

            if (child2 != null)
            {
                child.gameObject.transform.localScale = new Vector3(scale.x *Time.deltaTime , scale.y *Time.deltaTime , scale.z *Time.deltaTime );
                //Animator anim = child.GetComponent<Animator>();
                //if (anim != null)
                //    anim.Play("Animated PBR Chest _Opening_UnCommon");

            }
            else if(child2 != null)
            {

            }
        }

        //Fountain effects event
        if (other.gameObject.CompareTag("Fountain"))
        {
            ParticleSystem[] water = other.gameObject.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < water.Length; i++)
            {
                water[i].Play();
            }
            
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        //Fireflies event
        if (other.gameObject.CompareTag("FloorFireflies"))
        {
            fireflies = other.gameObject.GetComponentInChildren<ParticleSystem>();
        }
    }

    //The delay coroutine
    public IEnumerator DelayedAnimation(ParticleSystem ps, float time)
    {
        yield return new WaitForSeconds(time);
        ps.Play();
    }
}