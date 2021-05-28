using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreForce : MonoBehaviour
{
    bool up = false;
    float n;

    public float y = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        n = Random.Range(800, 1000);

        if (!up)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * n * y);
            //transform.Rotate(1,5,10);
            up = true;
        }

        if (up)
        {
            GetComponent<Rigidbody>().AddForce(-transform.up * n * 0.01f);
        }

    }


}
