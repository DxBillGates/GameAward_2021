using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    private Material material;

    float dist = 5;
    public float factor = 1;
    public float idx = 1;
    bool flag = false;
    bool mCheck = false;
    public bool isDecrease;
    bool isBreak;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        isDecrease = false;
        isBreak = true;
    }

    // Update is called once per frame
    void Update()
    {
        dist += 10;
        BreakPolygon(mCheck);
        //idx += 0.01f;
        //factor += idx;
        //material.SetFloat("_StartDistance", dist);
        material.SetFloat("_ScaleFactor", factor);
    }

    public void BreakPolygon(bool check)
    {
        if (check && isBreak)
        {
            //dist = 100;
            if(!isDecrease)
            {
                idx += 0.01f;
                dist += 10;
                factor += idx;
                material.SetFloat("_StartDistance", dist);
                material.SetFloat("_ScaleFactor", factor);
                if(factor >= 30)
                {
                    isDecrease = true;
                }
            }
            else
            {
                idx -= 0.01f;
                dist -= 10;
                factor -= idx * 5;
                if(factor <= 0)
                {
                    factor = 1;
                    dist = 5;
                    isBreak = false;
                }
                material.SetFloat("_StartDistance", dist);
                material.SetFloat("_ScaleFactor", factor);
            }

            flag = true;
            mCheck = check;
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
