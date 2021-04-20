using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSample : MonoBehaviour
{

    ObjectBehavior objBehavior;
    Vector3 onNormal;
    GameObject child;
    bool isSetChild;
    bool changeMode;

    // Start is called before the first frame update
    void Start()
    {
        onNormal = new Vector3();
        objBehavior = GetComponent<ObjectBehavior>();
        child = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        child.GetComponent<SphereCollider>().enabled = false;
        child.GetComponent<MeshRenderer>().enabled = false;
        isSetChild = changeMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        onNormal = objBehavior.onNormal;

        {
            transform.position -= transform.right / 5;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isSetChild)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
                child.transform.position = transform.position;
                child.transform.rotation = transform.rotation;
                isSetChild = true;
            }
            else
            {
                changeMode = true;
            }
        }

        if (changeMode)
        {
            objBehavior.changePartsSystemBehavior.Change(gameObject, child);
            if (!objBehavior.changePartsSystemBehavior.changeMode)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
                isSetChild = false;
                changeMode = false;
            }
        }
        if (!objBehavior.changePartsSystemBehavior.changeMode)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, onNormal) * transform.rotation;
        }
    }

   

}
