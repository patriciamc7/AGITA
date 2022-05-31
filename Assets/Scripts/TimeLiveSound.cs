using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLiveSound : MonoBehaviour
{
    public float timeOfLive; 
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeOfLive); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
