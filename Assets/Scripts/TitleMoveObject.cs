using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMoveObject : MonoBehaviour
{
    public Transform axisTransform;
    public float radius;
    public float time;
    public float delay;
    float y;
    // Start is called before the first frame update
    void Start()
    {
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3();
        newPos.x = radius * Mathf.Sin(2 * Mathf.PI / 360.0f * time) + axisTransform.position.x;
        newPos.y = y;
        newPos.z = radius * Mathf.Cos(2 * Mathf.PI / 360.0f * time) + axisTransform.position.z;
        transform.position = newPos;

        Vector3 futurePos = new Vector3();
        futurePos.x = radius * Mathf.Sin(2 * Mathf.PI / 360.0f * (time + Time.deltaTime));
        futurePos.y = 0;
        futurePos.z = radius * Mathf.Cos(2 * Mathf.PI / 360.0f * (time + Time.deltaTime));

        Quaternion newRotation = Quaternion.LookRotation(-futurePos);
        transform.rotation = newRotation;

        time += (50 - delay) * Time.deltaTime;
    }
}
