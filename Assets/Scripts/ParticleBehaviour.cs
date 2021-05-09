using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    public Vector3 vector;
    public Vector3 gravity { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vector += gravity * Time.deltaTime;
        transform.position += vector * Time.deltaTime;
    }
}
