using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsBehavior : MonoBehaviour
{
    [SerializeField] int setNumber = 0;
    public int number { get; set; }

    [SerializeField]Vector3 front, back;
    void Start()
    {
        number = setNumber;
        float value = 2.5f;
        if (gameObject.name == "mobius_special")
        {

            front = transform.position - transform.forward * value;
            back = transform.position + transform.forward * value;
        }
        else
        {
            front = transform.position - transform.up * value;
            back = transform.position + transform.up * value;
        }
        //GameObject g1 = GameObject.CreatePrimitive(PrimitiveType.Sphere),g2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //g1.transform.position = front;
        //g2.transform.position = back;
    }

    void Update()
    {
        
    }
}
