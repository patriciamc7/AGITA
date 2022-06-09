using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeOut : MonoBehaviour
{
    public Texture Texture;
    public Color FadeColor;

    [Range(0, 1)]
    public float FadeTime;
    private Color ColorLerp;
    
    void Start()
    {
        ColorLerp = Color.clear;
    }
    void Update()
    {
        ColorLerp = Color.Lerp(ColorLerp, FadeColor, FadeTime);
        //AudioSource[] audio= this.GetComponents<AudioSource>();
        //audio[0].volume = Mathf.Lerp(audio[0].volume, 0, FadeTime);
    }

    public void OnGUI()
    {
        GUI.color = ColorLerp;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture);
    }

}
