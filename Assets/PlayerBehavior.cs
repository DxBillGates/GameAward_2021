using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    LerpBehaviour lerpBehaviour;
    ObjectBehavior objBehavior;
    Vector3 onNormal;
    GameObject child;
    bool isSetChild;
    bool changeMode;
    void Start()
    {
        lerpBehaviour = GetComponent<LerpBehaviour>();
        onNormal = new Vector3();
        objBehavior = GetComponent<ObjectBehavior>();
        child = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        child.GetComponent<CapsuleCollider>().enabled = true;
        child.GetComponent<MeshRenderer>().enabled = false;
        isSetChild = changeMode = false;
    }

    void Update()
    {
        onNormal = objBehavior.onNormal;

        if (!changeMode)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right / 15;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right / 15;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isSetChild)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
                child.transform.position = transform.position;
                child.transform.rotation = transform.rotation;
                isSetChild = true;
                child.layer = 9;

                RaycastHit hit;
                float maxDistance = 10;
                if (Physics.Raycast(child.transform.position, -child.transform.up, out hit, maxDistance))
                {
                    Color color = Color.red;
                    color.a = 0.25f;
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = color;
                    objBehavior.changePartsSystemBehavior.setObject = hit.collider.gameObject;
                }

            }
            else
            {
                changeMode = true;
            }
        }

        if (!objBehavior.changePartsSystemBehavior.setObject)
        {
            changeMode = false;
            if (isSetChild)
            {
                isSetChild = false;
                child.GetComponent<MeshRenderer>().enabled = false;
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
