using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleL : MonoBehaviour
{
    private ParticleSystem particle;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //particle.Play();
    }

    public void ParticleBegin()
    {
        GameObject part = Instantiate(obj) as GameObject;
        part.transform.position = transform.position;
        part.transform.rotation = transform.rotation;

        particle.Play();

    }

}
