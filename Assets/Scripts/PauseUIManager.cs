using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIManager : MonoBehaviour
{
    public static List<PauseUIBehaviour> pauseUIBehaviours;

    private void Awake()
    {
        pauseUIBehaviours = new List<PauseUIBehaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void OnPause()
    {
        foreach (var ui in pauseUIBehaviours)
        {
            ui.gameObject.SetActive(true);
        }
    }

    public static void OnResume()
    {
        foreach (var ui in pauseUIBehaviours)
        {
            ui.gameObject.SetActive(false);
        }
    }
}
