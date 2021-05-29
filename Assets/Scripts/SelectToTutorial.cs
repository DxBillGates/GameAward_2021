using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectToTutorial : MonoBehaviour
{
    public float time;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        FadeManager2.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(t >= time)
        {
            FadeManager2.FadeOut("TT");
        }
        if(Input.GetMouseButtonDown(0))
        {
            FadeManager2.FadeOut("TT");
        }
        t += Time.deltaTime;
    }
}
