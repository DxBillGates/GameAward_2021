using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBehaviour : MonoBehaviour
{
    public GameObject hit;
    public AudioSource hitSE { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        hitSE = hit.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
