using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryEnemyBehaviour : MonoBehaviour
{
    public float value;
    public GameObject beam;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = Instantiate(beam) as GameObject;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
    }

    public void GenerateBeam(bool check)
    {
        if (check)
        {
            count++;
            if (count == 1)
            {
                GameObject obj = Instantiate(beam) as GameObject;
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
            }
        }

    }
}
