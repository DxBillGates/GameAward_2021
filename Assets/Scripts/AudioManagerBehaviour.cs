using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBehaviour : MonoBehaviour
{
    public GameObject hit;
    public GameObject glassSEObject;
    public GameObject clickSEObject;

    public AudioSource hitSE;
    public AudioSource glassSE;
    public AudioSource clickSE;
    // Start is called before the first frame update
    void Start()
    {
        hitSE = hit.GetComponent<AudioSource>();
        glassSE = glassSEObject.GetComponent<AudioSource>();
        clickSE = clickSEObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
