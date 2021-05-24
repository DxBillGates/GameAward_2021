using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneSystemBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        FadeManager2.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //SceneManager.LoadScene("Stage1");
            FadeManager2.FadeOut("StageSelect");
        }
    }
}
