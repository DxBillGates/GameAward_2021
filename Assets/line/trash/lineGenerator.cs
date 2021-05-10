using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineGenerator : MonoBehaviour
{
    public GameObject lineObj;
    GameObject obj;
    float span = 0.1f;
    float delta = 0;
    int count = 0;

    ChangePartsSystemBehavior changePartsSystemBehavior;

    // Start is called before the first frame update
    void Start()
    {
        changePartsSystemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Q) && count <= 1)
        //    count++;

        if (!changePartsSystemBehavior.changeMode)
        {
            //if (count >= 1)
            {
                this.delta += Time.deltaTime;
                if (this.delta > this.span)
                {
                    this.delta = 0;
                    obj = Instantiate(lineObj);
                    obj.transform.rotation = transform.rotation;
                    obj.transform.position = transform.position;
                }
            }
        }




    }

}
