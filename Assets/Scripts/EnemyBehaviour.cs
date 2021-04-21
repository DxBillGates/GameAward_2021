﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    LerpBehaviour lerpBehaviour;
    ObjectBehavior objectBehavior;
    ChangePartsSystemBehavior systemBehavior;
    new Renderer renderer;
    public bool onMove;
    public float hp;
    public float damageSpan;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        lerpBehaviour = GetComponent<LerpBehaviour>();
        objectBehavior = GetComponent<ObjectBehavior>();
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        renderer = GetComponent<Renderer>();
        //Color color;
        //color = (lerpBehaviour.leftMove) ? Color.blue : Color.red;
        //renderer.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        //Color color;
        //color = (lerpBehaviour.leftMove) ? Color.blue : Color.red;
        //renderer.material.color = color;

        RaycastHit hit;
        Vector3 forward = (lerpBehaviour.leftMove) ? -transform.right : transform.right;
        if (Physics.Raycast(transform.position, forward, out hit, 1))
        {
            if (hit.collider.gameObject.layer == 9)
            {
                if (hit.collider.gameObject.GetComponent<Renderer>().enabled)
                {
                    if (!systemBehavior.changeMode)
                        systemBehavior.Initialize();
                }
            }
        }

        if (Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            if(hit.collider.gameObject.layer == 7)
            {
                if(hit.distance <= 1)
                {
                    if(!onMove)
                    {
                        onMove = true;
                    }
                }
            }
        }

        if (!objectBehavior.changePartsSystemBehavior.changeMode && onMove)
        {
            if (lerpBehaviour.leftMove)
            {
                transform.position -= transform.right / 10;
            }
            else
            {
                transform.position += transform.right / 10;
            }
            transform.rotation = Quaternion.FromToRotation(transform.up, objectBehavior.onNormal) * transform.rotation;
        }
    }

    public void Damage()
    {
        if(t >= damageSpan)
        {
            t = 0;
            --hp;
        }
        t += Time.deltaTime;
    }
}
