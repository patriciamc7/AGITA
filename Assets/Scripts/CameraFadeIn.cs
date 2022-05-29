using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeIn : MonoBehaviour
{
    public Texture Texture;
    public Color FadeColor;

    [Range(0, 1)]
    public float FadeTime;
    private Color ColorLerp;
    void Start()
    {
        ColorLerp = FadeColor;
    }
    void Update()
    {
        ColorLerp = Color.Lerp(ColorLerp, Color.clear, FadeTime);
    }

    public void OnGUI()
    {
        GUI.color = ColorLerp;  
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture);
    }

}
