using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color color = gameObject.GetComponent<Renderer>().material.color;
        color.a = 0;

        gameObject.GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScaleDec(float scale)
    {
        Vector3 one;

        one = gameObject.transform.localScale; 

        one.x = scale * 0.05f;
        one.y = scale * 0.05f;
        one.z = scale * 0.05f;

        gameObject.transform.localScale = one; 
    }

    public void ScalePlu(float scale)
    {
        Vector3 one;

        one = gameObject.transform.localScale;

        one.x = scale * 0.05f;
        one.y = scale * 0.05f;
        one.z = scale * 0.05f;

        gameObject.transform.localScale = one;
    }

}
