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
        //GameObject obj = Instantiate(beam) as GameObject;
        //obj.transform.position = transform.position;
        //obj.transform.rotation = transform.rotation;
    }

    public void GenerateBeam()
    {
        {
                GameObject obj = Instantiate(beam) as GameObject;
                Vector3 pos = transform.position;
                obj.transform.position =pos;
                obj.transform.rotation = transform.rotation;
        }

    }
}
