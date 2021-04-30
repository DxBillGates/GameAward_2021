using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    ChangePartsSystemBehavior systemBehavior;
    LineLerpBehaviour lerpBehaviour;
    ObjectBehavior objectBehavior;
    public GameObject start, end;
    // Start is called before the first frame update
    void Start()
    {
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        lerpBehaviour = GetComponent<LineLerpBehaviour>();
        objectBehavior = GetComponent<ObjectBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!systemBehavior.changeMode)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, objectBehavior.onNormal) * transform.rotation;
            if (lerpBehaviour.leftMove)
            {
                transform.position += transform.right * Time.deltaTime * 10;
            }
            else
            {
                transform.position -= transform.right * Time.deltaTime * 10;
            }

            if (Vector3.Distance(transform.position, end.transform.position) <= 1)
            {
                transform.position = start.transform.position;
                transform.rotation = start.transform.rotation;
            }
        }
    }
}
