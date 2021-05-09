using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    public Vector3 vector;
    public Vector3 gravity { get; set; }
    ObjectBehavior objectBehavior;
    // Start is called before the first frame update
    void Start()
    {
        objectBehavior = GetComponent<ObjectBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectBehavior.isFly)
        {
            vector += gravity * Time.deltaTime;
            transform.position += vector * Time.deltaTime;
        }
        else
        {
            this.enabled = false;
        }
    }
}