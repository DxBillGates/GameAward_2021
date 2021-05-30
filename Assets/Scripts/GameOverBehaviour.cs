using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverBehaviour : MonoBehaviour
{
    public float sceneTime;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        FadeManager2.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeManager2.FadeOut("StageSelect");
        }

        if (t >= sceneTime)
        {
            FadeManager2.FadeOut("StageSelect");
        }
        t += Time.deltaTime;
    }
}
