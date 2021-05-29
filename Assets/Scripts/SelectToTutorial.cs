using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectToTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FadeManager2.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FadeManager2.FadeOut("TT");
        }
    }
}
