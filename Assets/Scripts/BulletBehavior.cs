using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public int attackValue;
    public Vector3 vector;
    public ChangePartsSystemBehavior systemBehavior;
    PauseBehaviour pauseBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        pauseBehaviour = gameObject.AddComponent<PauseBehaviour>();
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!systemBehavior.changeMode)
        {
            transform.position += vector;
        }
    }
}
