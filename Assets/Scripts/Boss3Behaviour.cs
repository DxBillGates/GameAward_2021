using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Behaviour : MonoBehaviour
{
    Material material;
    public int maxReviveCount;
    int currentReviveCount;
    public bool breakPolygonFlag;
    bool isDecrease;
    EnemyBehaviour enemyBehaviour;

    float dist = 5;
    public float factor = 1;
    public float idx = 1;
    // Start is called before the first frame update
    void Start()
    {
        currentReviveCount = 0;
        material = GetComponent<Renderer>().material;
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(breakPolygonFlag)
        {
            BreakPolygon(true);
            enemyBehaviour.hp = enemyBehaviour.setHP;
        }
    }

    public void BreakPolygon(bool flag)
    {
        if (!isDecrease)
        {
            idx += 0.01f;
            dist += 10;
            factor += Mathf.Lerp(0,1,easeInOutQuint(idx));
            if(factor >= 70 && currentReviveCount < maxReviveCount)
            {
                isDecrease = true;
            }
            material.SetFloat("_StartDistance", dist);
            material.SetFloat("_ScaleFactor", factor);
            if(currentReviveCount >= maxReviveCount)
            {
                StartCoroutine(Delay(1, gameObject));
            }
        }
        if(isDecrease)
        {
            idx -= 0.01f;
            dist += 10;
            factor -= idx;
            if(factor <= 0)
            {
                idx = 1;
                dist = 5;
                factor = 1;
                isDecrease = false;
                breakPolygonFlag = false;
                GetComponent<PauseBehaviour>().OnResume();
                currentReviveCount++;
            }
            material.SetFloat("_StartDistance", dist);
            material.SetFloat("_ScaleFactor", factor);
        }
    }
    private IEnumerator Delay(float waitTime, GameObject go)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(go);
    }

    float easeInOutQuint(float x)
    {
        return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
    }
}
