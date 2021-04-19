using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    Vector3 target;
    public float timeSpan;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        target = new Vector3(0, 25, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(t >= timeSpan)
        {
            t = 0;
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.transform.position = transform.position + transform.up * 2;
            Rigidbody rb = bullet.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce((target - transform.position).normalized * 100);

        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position,target - transform.position,out hit))
        {
            if(hit.collider.gameObject.name == "Core")
            {
                t += Time.deltaTime;
            }
            else
            {
                t = 0;
            }
        }
    }
}
