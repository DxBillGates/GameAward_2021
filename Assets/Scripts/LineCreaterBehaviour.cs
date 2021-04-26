using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreaterBehaviour : MonoBehaviour
{
    GameObject lineCreater;
    public GameObject endObject;
    bool isCreate;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        isCreate = false;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isCreate)
            {
                lineCreater = new GameObject();
                lineCreater.transform.position = transform.position;
                lineCreater.transform.rotation = transform.rotation;
                Debug.Log("LineCreater生成");
                isCreate = true;
            }
        }

        if (isCreate)
        {
            while (true)
            {
                LineCreaterMoveFunction();
                CreateTestObject(0.5f);
                if (Vector3.Distance(lineCreater.transform.position, endObject.transform.position) <= 1)
                {
                    isCreate = false;
                    break;
                }
                Debug.DrawRay(lineCreater.transform.position, lineCreater.transform.up * 10, Color.red);
            }
        }
    }

    void LineCreaterMoveFunction()
    {
        RaycastHit hit;
        //下方にレイキャストを行い入れ替え中でなければ乗っているパーツと親子関係を設定
        if (Physics.Raycast(lineCreater.transform.position, -lineCreater.transform.up, out hit, 10))
        {
            if (hit.collider.gameObject.layer == 7)
            {
                Debug.Log("AA");
                lineCreater.transform.rotation = Quaternion.FromToRotation(lineCreater.transform.up, hit.normal) * lineCreater.transform.rotation;
            }
        }
        lineCreater.transform.position += -lineCreater.transform.right * Time.deltaTime;
    }

    void CreateTestObject(float timeSpan)
    {
        t += Time.deltaTime;
        if (t >= timeSpan)
        {
            t = 0;
            GameObject newTestObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newTestObject.transform.position = lineCreater.transform.position;
        }
    }
}
