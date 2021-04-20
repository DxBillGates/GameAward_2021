using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineGenerator : MonoBehaviour
{
    public GameObject lineObj;
    GameObject obj;
    float span = 0.3f;
    float delta = 0;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)  && count<=1)
            count++;

        if (count >= 1)
        {
            this.delta += Time.deltaTime;
            if (this.delta > this.span)
            {
                this.delta = 0;
                obj = Instantiate(lineObj);
                obj.transform.position = transform.position;
            }
        }


    }

}
