using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YubiMove : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 firstPos;
    Vector3 endPos;

    void Start()
    {
        firstPos = new Vector3(23, 30, -4.5f);
        endPos = new Vector3(1, 45, -4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstPos.x > endPos.x)
        {
            firstPos.x-=0.16f;
        }
        if (firstPos.y < endPos.y)
        {
            firstPos.y+=0.1f;
        }

        if (firstPos.x <= endPos.x&& firstPos.y <= endPos.y)
        {
            firstPos = new Vector3(23, 30, -4.5f);
        }

        transform.position = firstPos;
    }
}
