using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBright : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        this.gameObject.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1, 0, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        this.gameObject.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1, 0, 0, 1));
    }
}
