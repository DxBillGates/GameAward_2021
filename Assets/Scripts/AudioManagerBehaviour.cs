using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBehaviour : MonoBehaviour
{
    public GameObject hit;
    public GameObject glassSEObject;
    public GameObject clickSEObject;
    public GameObject clearSEObject;
    public GameObject overSEObject;
    public GameObject mutekiSEObject;

    public AudioSource hitSE;
    public AudioSource glassSE;
    public AudioSource clickSE;
    public AudioSource clearSE;
    public AudioSource overSE;
    public AudioSource mutekiSE;
    // Start is called before the first frame update
    void Start()
    {
        hitSE = hit.GetComponent<AudioSource>();
        glassSE = glassSEObject.GetComponent<AudioSource>();
        clickSE = clickSEObject.GetComponent<AudioSource>();
        clearSE = clearSEObject.GetComponent<AudioSource>();
        overSE = overSEObject.GetComponent<AudioSource>();
        mutekiSE = mutekiSEObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
