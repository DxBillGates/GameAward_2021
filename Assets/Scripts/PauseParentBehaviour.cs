using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseParentBehaviour : MonoBehaviour
{
    public static List<PauseBehaviour> pauseBehaviours { get; set; }
    public static bool isPause;
    // Start is called before the first frame update
    private void Awake()
    {
        pauseBehaviours = new List<PauseBehaviour>();
        isPause = false;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        //if(isPause && Input.GetMouseButtonDown(0))
        //{
        //    Resume();
        //}
    }

    public static void Pause()
    {
        foreach (var pauseBehaviour in pauseBehaviours)
        {
            pauseBehaviour.OnPause();
        }
        isPause = true;
        PauseUIManager.OnPause();
    }

    public static void Resume()
    {
        foreach (var pauseBehaviour in pauseBehaviours)
        {
            pauseBehaviour.OnResume();
        }
        isPause = false;
        PauseUIManager.OnResume();
    }
}
