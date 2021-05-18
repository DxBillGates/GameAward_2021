using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class YubiScll : MonoBehaviour
{
    int count = 0;
    int change = 0;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0)
        {
            change = 1;
        }

        if (scroll < 0)
        {
            change = -1;          
        }

        {
            count++;

            if (count < 20)
            {
                pos.y += 0.05f;
            }
            if (count > 20)
            {
                pos.y -= 0.05f;
            }
            if (count >= 40)
            {
                pos = new Vector3(-6.5f, 31, -56);
                count = 0;
            }
        }

        if (change > 0)
        {
            transform.rotation = new Quaternion(0, 0, 180, 0);
        }

        if(change<0)
        {
            Destroy(gameObject);
        }

        transform.position = pos;

    }
}
