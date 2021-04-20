using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreaterBehavior : MonoBehaviour
{
    public bool leftMode;
    public Vector3 fallVector;
    public bool drawRay;
    public float timeSpan;
    public int max;
    int count;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        t = 0;
        if(timeSpan == 0)
        {
            timeSpan = 3;
        }

        GetComponent<Renderer>().material.color = (leftMode) ? Color.blue : Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (drawRay)
            Debug.DrawRay(transform.position, fallVector * 10, Color.red);

        if(t >= timeSpan && count < max)
        {
            Create();
            count++;
            t = 0;
        }

        t += Time.deltaTime;
    }

    void Create()
    {
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        LerpBehaviour player = GameObject.Find("Player").GetComponent<LerpBehaviour>();

        Destroy(gameObject.GetComponent<CapsuleCollider>());
        gameObject.AddComponent<BoxCollider>();

        ObjectBehavior objBehavior = gameObject.AddComponent<ObjectBehavior>();
        EnemyBehaviour enemyBehaviour = gameObject.AddComponent<EnemyBehaviour>();
        enemyBehaviour.onMove = false;
        LerpBehaviour lerpBehaviour = gameObject.AddComponent<LerpBehaviour>();
        lerpBehaviour.frontList = player.frontList;
        lerpBehaviour.backList = player.backList;
        lerpBehaviour.isAutoMove = true;

        lerpBehaviour.leftMove = leftMode;

        objBehavior.fallVector = fallVector.normalized / 100;
        //lerpBehaviour.leftMove = true;

        gameObject.transform.position = transform.position;
        gameObject.name = "CreatedEnemy";
        gameObject.layer = 6;

        gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, -fallVector) * gameObject.transform.rotation;
        gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.right,-gameObject.transform.right) * gameObject.transform.rotation;

        gameObject.tag = "enemy";

        gameObject.AddComponent<AttackBehaviour>().timeSpan = 3;
    }
}
