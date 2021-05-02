using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplifierBehaviour : MonoBehaviour
{
    public float addValue;
    ObjectBehavior objectBehavior;
    // Start is called before the first frame update
    void Start()
    {
        objectBehavior = GetComponent<ObjectBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.FromToRotation(transform.up, objectBehavior.onNormal) * transform.rotation;
    }
}
