using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryEnemyBehaviour : MonoBehaviour
{
    public float value;
    public GameObject beam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateBeam(bool check)
    {
        if(check)
        {

        GameObject obj = Instantiate(beam) as GameObject;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        }

    }
}
