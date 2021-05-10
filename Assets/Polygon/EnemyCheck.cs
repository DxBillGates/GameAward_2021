using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    private Material material;

    float dist = 5;
    float factor = 1;
    float idx = 0;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        idx += 0.0001f;
        factor += idx;
        material.SetFloat("_StartDistance", dist);
        material.SetFloat("_ScaleFactor", factor);
    }

    public void BreakPolygon(bool check)
    {
        if (check)
        {
            dist = 100;

            //for (int i = 0; i < 20; i++)
            {
                idx += 0.0001f;
                factor += idx;
                material.SetFloat("_StartDistance", dist);
                material.SetFloat("_ScaleFactor", factor);
            }

            flag = true;

        }

        if (flag)
        {
            StartCoroutine(Delay(1, gameObject));
        }

    }

    private IEnumerator Delay(float waitTime, GameObject go)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(go);

    }
}
