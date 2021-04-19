using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGenerator : MonoBehaviour
{

    public GameObject hall;
    Vector3 pos=new Vector3(0,23,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("checker"))
        {
            GameObject obj = Instantiate(hall) as GameObject;
            
            obj.transform.position = pos;
        }
    }
}
