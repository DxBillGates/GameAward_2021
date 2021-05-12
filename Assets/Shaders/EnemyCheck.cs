using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    private Material material;

    float dist = 5;
    float factor = 1;
    public float idx = 0.1f;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        dist += 10;
        //idx += 0.01f;
        //factor += idx;
        //material.SetFloat("_StartDistance", dist);
        material.SetFloat("_ScaleFactor", factor);
    }

    public void BreakPolygon(bool check)
    {
        if (check)
        {
            //dist = 100;
            {
                idx += 0.1f;
                dist+=10;
                factor += idx;
                material.SetFloat("_StartDistance", dist);
                material.SetFloat("_ScaleFactor", factor);
            }

            flag = true;

        }

        if (flag)
        {
            StartCoroutine(Delay(1.5f, gameObject));
        }

    }

    private IEnumerator Delay(float waitTime, GameObject go)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(go);

    }
}
