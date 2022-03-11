using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBurst : MonoBehaviour
{
    public ParticleSystem burst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnColissionEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ParticleSystem ps = this.GetComponent<ParticleSystem>();
            ParticleSystem.SubEmittersModule var = ps.subEmitters;
            
            //burst.Play();
        }
    }
}
