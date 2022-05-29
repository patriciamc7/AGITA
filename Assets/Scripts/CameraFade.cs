using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    public Texture Texture;
    public Color FadeColor;

    [Range(0, 5)]
    public int pause;

    [Range(0, 5)]
    public float FadeTime;
    private Color ColorLerp;
    private float CurrentTime;
    private bool CanStart = false;
    private bool FadeIsCompelete = false;
    // Start is called before the first frame update
    void Start()
    {
        ColorLerp = FadeColor;
        StartCoroutine("StartCameraFade");
    }

    // Update is called once per frame
    void Update()
    {
        if (CanStart)
        {
            CurrentTime = Time.time;
            ColorLerp = Color.Lerp(FadeColor, Color.clear, CurrentTime / FadeTime);
        }
        if (CurrentTime > FadeTime)
            FadeComplete();
    }

    public void OnGUI()
    {
        GUI.color = ColorLerp;  
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture);
    }

    public void FadeComplete()
    {
        FadeIsCompelete = true;
        this.enabled = false;
        this.gameObject.SetActive(false);
    }
    IEnumerator StartCameraFade()
    {
        yield return new WaitForSecondsRealtime(pause);
        CanStart = true; 
        yield return null;
    }
}
