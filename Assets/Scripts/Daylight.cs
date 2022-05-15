using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daylight : MonoBehaviour
{
    public Material skybox;
    public Light sun;
    public Color night;
    void Start()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 0.5f);
    }
    void Update()
    {
        if (Time.time > 90f)
        {
            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(RenderSettings.skybox.GetFloat("_Exposure"), 0, 0.001f));
            sun.intensity = Mathf.Lerp(sun.intensity, 0, 0.001f);
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, night, 0.001f);
        }
    }
}
