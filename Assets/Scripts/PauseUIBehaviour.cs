using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PauseUIManager.pauseUIBehaviours.Add(this);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        PauseUIManager.pauseUIBehaviours.Remove(this);
    }
}
