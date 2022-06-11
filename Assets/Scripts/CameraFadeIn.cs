using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeIn : MonoBehaviour
{
    public GameObject Fade;

    [Range(0, 1)]
    public float FadeTime;
    public bool ActiveFade = true;

    void Awake()
    {
        Fade.transform.Find("CanvasLeft").GetComponent<CanvasGroup>().alpha = 1;
        Fade.transform.Find("CanvasRight").GetComponent<CanvasGroup>().alpha = 1;
    }
    void Update()
    {
        if(ActiveFade)
        {
            Fade.transform.Find("CanvasLeft").GetComponent<CanvasGroup>().alpha = Mathf.Lerp(Fade.transform.Find("CanvasLeft").GetComponent<CanvasGroup>().alpha, 0, FadeTime);
            Fade.transform.Find("CanvasRight").GetComponent<CanvasGroup>().alpha = Mathf.Lerp(Fade.transform.Find("CanvasRight").GetComponent<CanvasGroup>().alpha, 0, FadeTime);

            if (Fade.transform.Find("CanvasLeft").GetComponent<CanvasGroup>().alpha< 0.1)
                ActiveFade = false;
        }
    }
}
