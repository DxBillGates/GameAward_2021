using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    Behaviour[] behaviours;
    // Start is called before the first frame update
    void Start()
    {
        PauseParentBehaviour.pauseBehaviours.Add(this);
        behaviours = null;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPause()
    {
        if (behaviours != null)
        {
            return;
        }
        behaviours = gameObject.GetComponentsInChildren<Behaviour>();
        foreach (var component in behaviours)
        {
            if (!(component.GetType() == typeof(PauseParentBehaviour)))
            {
                component.enabled = false;
            }
        }
    }

    public void OnResume()
    {
        if (behaviours == null)
        {
            return;
        }
        foreach (var component in behaviours)
        {
            component.enabled = true;
        }

        behaviours = null;
    }

    private void OnDestroy()
    {
        PauseParentBehaviour.pauseBehaviours.Remove(this);
    }

    public void OnPauseOtherComponent(Behaviour com)
    {
        if (behaviours != null)
        {
            return;
        }
        behaviours = gameObject.GetComponentsInChildren<Behaviour>();
        foreach (var component in behaviours)
        {
            if (!(component.GetType() == typeof(PauseParentBehaviour)) && !(component.GetType() == com.GetType()))
            {
                component.enabled = false;
            }
        }
    }
}
