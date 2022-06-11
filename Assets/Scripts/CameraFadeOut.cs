using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeOut : MonoBehaviour
{
    public GameObject Fade;

    [Range(0, 1)]
    public float FadeTime;
    public bool ActiveFade = false;
    public Camera camera;
    void Update()
    {
        if(ActiveFade)
        {
            Fade.transform.Find("CanvasLeft").GetComponent<CanvasGroup>().alpha = Mathf.Lerp(Fade.transform.Find("CanvasLeft").GetComponent<CanvasGroup>().alpha, 1, FadeTime);
            Fade.transform.Find("CanvasRight").GetComponent<CanvasGroup>().alpha = Mathf.Lerp(Fade.transform.Find("CanvasRight").GetComponent<CanvasGroup>().alpha, 1, FadeTime);
            camera.GetComponent<AudioSource>().volume = Mathf.Lerp(camera.GetComponent<AudioSource>().volume, 0, FadeTime);

            if (Fade.transform.Find("CanvasLeft").GetComponent<CanvasGroup>().alpha == 1)
                Application.Quit();
        }
    }

}
