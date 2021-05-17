using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseParentBehaviour : MonoBehaviour
{
    public static List<PauseBehaviour> pauseBehaviours { get; set; }
    public static bool isPause;
    static bool isExclutionPause;
    static GameObject exclutionGameObject;
    static Behaviour exclutionBehaviour;
    // Start is called before the first frame update
    private void Awake()
    {
        pauseBehaviours = new List<PauseBehaviour>();
        isPause = false;
        isExclutionPause = false;
        exclutionGameObject = null;
        exclutionBehaviour = null;
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

    public static void PauseExclutionGameObject(Behaviour behaviour)
    {
        foreach (var pauseBehaviour in pauseBehaviours)
        {
            if(pauseBehaviour.gameObject == behaviour.gameObject)
            {
                exclutionGameObject = pauseBehaviour.gameObject;
                continue;
            }
            pauseBehaviour.OnPause();
        }
        isPause = true;
        isExclutionPause = true;
        //PauseUIManager.OnPause();
    }

    public static void ResumeExclution()
    {
            foreach (var pauseBehaviour in pauseBehaviours)
            {
                pauseBehaviour.OnResume();
            }
        isPause = false;
        isExclutionPause = false;
        PauseUIManager.OnResume();
    }
    public static void Pause()
    {
        foreach (var pauseBehaviour in pauseBehaviours)
        {
            if(isExclutionPause)
            {
                if(pauseBehaviour.gameObject == exclutionGameObject)
                {
                    continue;
                }
            }
            pauseBehaviour.OnPause();
        }
        isPause = true;
        PauseUIManager.OnPause();
    }

    public static void Resume()
    {
        if (!isExclutionPause)
        {
            foreach (var pauseBehaviour in pauseBehaviours)
            {
                pauseBehaviour.OnResume();
            }
        }
        isPause = false;
        PauseUIManager.OnResume();
    }
}
