using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public bool startBlack = false;
    private float t=0f;
    private float fadeTimer=0f;
    public float fadeDur = 1f;
    bool fadeType;
    // Start is called before the first frame update
    void Start()
    {
        if(startBlack)
        {
            GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
            FadeIn();
        }
        else 
        {
            GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(t<fadeTimer)
        {
            t+=Time.deltaTime*fadeDur;

            float alph = 0f;
            if(fadeType)
            {alph=Mathf.Lerp(0f, 1f, t);}
            else {alph=Mathf.Lerp(1f, 0f, t);}
            GetComponent<Image>().color = new Color(0f, 0f, 0f, alph);
        }
        else
        {
            GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }
    }
    public void FadeOut()
    {
        t=0;
        fadeType = true;
        fadeTimer = 1;
    }
    public void FadeIn()
    {
        t=0;
        fadeType = false;
        fadeTimer = 1;
    }
}
