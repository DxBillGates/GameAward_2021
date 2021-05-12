using System.Collections;
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

    bool scaleLerp;
    float scaleLerpT;
    AudioManagerBehaviour audioManager;
    public float takeDamageValue;
    public float startTakeDamageValue;
    // Start is called before the first frame update
    void Start()
    {
        scaleLerp = false;
        scaleLerpT = 0;
        t = 0;
        lerpBehaviour = GetComponent<LerpBehaviour>();
        objectBehavior = GetComponent<ObjectBehavior>();
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        renderer = GetComponent<Renderer>();
        //Color color;
        //color = (lerpBehaviour.leftMove) ? Color.blue : Color.red;
        //renderer.material.color = color;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerBehaviour>();
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
            if (hit.collider.gameObject.layer == 7)
            {
                if (hit.distance <= 1)
                {
                    if (!onMove)
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
                transform.position -= transform.right * Time.deltaTime * (objectBehavior.speed + objectBehavior.addSpeed);
            }
            else
            {
                transform.position += transform.right * Time.deltaTime * (objectBehavior.speed + objectBehavior.addSpeed);
            }
            transform.rotation = Quaternion.FromToRotation(transform.up, objectBehavior.onNormal) * transform.rotation;
        }


        if (scaleLerp)
        {
            scaleLerpT += Time.deltaTime / damageSpan;
            if (scaleLerpT >= 1)
            {
                scaleLerp = false;
                scaleLerpT = 1;
            }
            //transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(1, 1, 1) - new Vector3(0.5f, 0.5f, 0.5f), scaleLerpT);
            renderer.sharedMaterial.color *= Color.red;
        }
        else
        {
            renderer.material.color = Color.white;
            scaleLerpT = 0;
            //transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Damage(int value)
    {
        if(t == 0)
        {
            hp -= value;
            scaleLerp = true;
            audioManager.hitSE.Play();
        }
        t += Time.deltaTime;
        if (t >= damageSpan)
        {
            t = 0;
        }
    }
}
