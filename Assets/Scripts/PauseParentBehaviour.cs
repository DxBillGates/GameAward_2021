using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseParentBehaviour : MonoBehaviour
{
    public static List<PauseBehaviour> pauseBehaviours { get; set; }
    public bool isPause;
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
            isPause = true;
        }

        if(isPause && Input.GetMouseButtonDown(0))
        {
            Resume();
            isPause = false;
        }
    }

    public static void Pause()
    {
        foreach (var pauseBehaviour in pauseBehaviours)
        {
            pauseBehaviour.OnPause();
        }
    }

    public static void Resume()
    {
        foreach (var pauseBehaviour in pauseBehaviours)
        {
            pauseBehaviour.OnResume();
        }
    }
}
