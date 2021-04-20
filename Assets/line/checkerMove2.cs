using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkerMove2 : MonoBehaviour
{
    LerpBehaviour lerpBehaviour;
    ObjectBehavior objectBehavior;
    ChangePartsSystemBehavior systemBehavior;
    new Renderer renderer;
    GameObject player;
    LerpBehaviour playerLerpBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        lerpBehaviour = GetComponent<LerpBehaviour>();
        player = GameObject.Find("Player");
        playerLerpBehaviour = player.GetComponent<LerpBehaviour>();

        lerpBehaviour.frontList = playerLerpBehaviour.frontList;
        lerpBehaviour.backList = playerLerpBehaviour.backList;
        lerpBehaviour.isAutoMove = true;

        objectBehavior = GetComponent<ObjectBehavior>();
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        renderer = GetComponent<Renderer>();
        Color color;
        color = (lerpBehaviour.leftMove) ? Color.blue : Color.red;
        renderer.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        Color color;
        color = (lerpBehaviour.leftMove) ? Color.blue : Color.red;
        renderer.material.color = color;

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
        if (!objectBehavior.changePartsSystemBehavior.changeMode)
        {
            if (lerpBehaviour.leftMove)
            {
                transform.position += transform.right / 10;
            }
            else
            {
                transform.position -= transform.right / 10;
            }
            transform.rotation = Quaternion.FromToRotation(transform.up, objectBehavior.onNormal) * transform.rotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("enemy"))
        {

            other.gameObject.SetActive(false);
        }
    }

}
