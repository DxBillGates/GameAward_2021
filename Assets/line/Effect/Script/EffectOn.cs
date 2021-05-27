using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOn : MonoBehaviour
{
    public GameObject ef;
    bool one;
    
    // Start is called before the first frame update
    void Start()
    {
        one = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(!GameObject.FindGameObjectWithTag("checker"))
        {
            one = true;
        }

    }

    public void Died()
    {
        Destroy(gameObject);
    }

    public void CreateSpark()
    {
        
        if (one)
        {
            GameObject efs = Instantiate(ef) as GameObject;
            efs.transform.rotation = transform.rotation;
            efs.transform.position = transform.position;
            efs.transform.parent = transform;

            one = false;
        }
    }

}
