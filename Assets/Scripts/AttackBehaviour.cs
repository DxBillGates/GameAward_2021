using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    Vector3 target;
    public float timeSpan;
    float t;
    public int attackValue;
    ChangePartsSystemBehavior systemBehavior;
    public Mesh bulletModel;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        target = new Vector3(0, 25, 0);
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!systemBehavior.changeMode)
        {
            if (t >= timeSpan)
            {
                t = 0;
                GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                bullet.GetComponent<MeshFilter>().mesh = bulletModel;
                BulletBehavior bulletBehavior = bullet.AddComponent<BulletBehavior>();
                bulletBehavior.attackValue = attackValue;
                bullet.transform.position = transform.position + transform.up * 6;
                bullet.layer = 12;
                Rigidbody rb = bullet.AddComponent<Rigidbody>();
                rb.useGravity = false;
                //rb.AddForce((target - transform.position).normalized * 1000);
                bulletBehavior.vector = (target - transform.position).normalized *  6;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up * 10, transform.up, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "Core")
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
}
