using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreaterBehaviour : MonoBehaviour
{
    GameObject lineCreater;
    ObjectBehavior lineCreaterObjectBehaviour;
    LerpBehaviour lineCreaterLerpBehaviour;
    public GameObject endObject;
    public bool isCreate;
    public bool isDelete;
    float t;
    float time;
    Vector3 onNormal;
    // Start is called before the first frame update
    void Start()
    {
        onNormal = new Vector3();
        time = 0;
        lineCreater = new GameObject();
        lineCreaterObjectBehaviour = lineCreater.AddComponent<ObjectBehavior>();
        lineCreaterLerpBehaviour = lineCreater.AddComponent<LerpBehaviour>();


        LerpBehaviour player = GameObject.Find("Player").GetComponent<LerpBehaviour>();

        //ラープビヘイビアの設定
        lineCreaterLerpBehaviour.frontList = player.frontList;
        lineCreaterLerpBehaviour.backList = player.backList;
        lineCreaterLerpBehaviour.lerpPoints = new List<Transform>();
        lineCreaterLerpBehaviour.isAutoMove = true;
        lineCreaterLerpBehaviour.leftMove = false;

        isDelete = false;
        isCreate =false;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (!isCreate)
        //    {
        //        lineCreater.transform.position = transform.position;
        //        lineCreater.transform.rotation = transform.rotation;
        //        Debug.Log("LineCreater生成");
        //        isCreate = true;

        //    }
        //}

        if (isCreate && !lineCreaterObjectBehaviour.changePartsSystemBehavior.changeMode)
        {
            while (true)
            {
                LineCreaterMoveFunction();
                lineCreaterLerpBehaviour.AnotherMoveFunc();
                CreateTestObject(0.5f);
                if (Vector3.Distance(lineCreater.transform.position, endObject.transform.position) <= 1)
                {
                    isCreate = false;
                    isDelete = false;
                    Debug.Log("成功しました");
                break;
            }

                time += Time.deltaTime;
                if (time >= 120)
                {
                    Debug.Log("失敗しました");
                    time = 0;
                    isCreate = false;
                    isDelete = false;
                break;
            }
        }
    }

        if (lineCreaterObjectBehaviour.changePartsSystemBehavior.changeMode && !isDelete)
        {
            Debug.Log("全消ししました");
            AllDestroy();
            isDelete = true;
        }

        if (lineCreaterObjectBehaviour.changePartsSystemBehavior.oldChangeFlag && !lineCreaterObjectBehaviour.changePartsSystemBehavior.changeMode)
        {
            Debug.Log("生成開始します");
            lineCreater.transform.position = transform.position;
            lineCreater.transform.rotation = transform.rotation;
            isCreate = true;
        }
    }

    void LineCreaterMoveFunction()
    {
        RaycastHit hit;
        //下方にレイキャストを行い入れ替え中でなければ乗っているパーツと親子関係を設定
        if (Physics.Raycast(lineCreater.transform.position, -lineCreater.transform.up, out hit, 1))
        {
            if (hit.collider.gameObject.layer == 7)
            {
                //Debug.Log("AA");
                onNormal = hit.normal;
            }
        }
        lineCreater.transform.rotation = Quaternion.FromToRotation(lineCreater.transform.up, onNormal) * lineCreater.transform.rotation;
        lineCreater.transform.position += -lineCreater.transform.right * Time.deltaTime * 10;
    }

    void CreateTestObject(float timeSpan)
    {
        t += Time.deltaTime;
        if (t >= timeSpan)
        {
            t = 0;
            GameObject newTestObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newTestObject.tag = "lines";
            newTestObject.transform.position = lineCreater.transform.position;
            newTestObject.transform.rotation = lineCreater.transform.rotation;

            newTestObject.GetComponent<Collider>().isTrigger = false;

            LerpBehaviour player = GameObject.Find("Player").GetComponent<LerpBehaviour>();

            ObjectBehavior objBehaviour = newTestObject.AddComponent<ObjectBehavior>();
            LineLerpBehaviour lerpBehaviour = newTestObject.AddComponent<LineLerpBehaviour>();
            MoveBehaviour moveBehaviour = newTestObject.AddComponent<MoveBehaviour>();
            moveBehaviour.start = gameObject;
            moveBehaviour.end = endObject;
            moveBehaviour.enabled = false;
            //ラープビヘイビアの設定
            lerpBehaviour.enabled = false;
            lerpBehaviour.frontList = player.frontList;
            lerpBehaviour.backList = player.backList;
            lerpBehaviour.lerpPoints = new List<Vector3>();

            lerpBehaviour.isAutoMove = lineCreaterLerpBehaviour.isAutoMove;
            lerpBehaviour.leftMove = lineCreaterLerpBehaviour.leftMove;
            lerpBehaviour.frontLerpMode = lineCreaterLerpBehaviour.frontLerpMode;
            lerpBehaviour.str = lineCreaterLerpBehaviour.str;
            lerpBehaviour.isMove = lineCreaterLerpBehaviour.isMove;
            lerpBehaviour.oldLerpMode = lineCreaterLerpBehaviour.oldLerpMode;

            lerpBehaviour.lerpMode = lineCreaterLerpBehaviour.lerpMode;
            //lerpBehaviour.lerpPoints = new List<Transform>();

            lerpBehaviour.lerpPoints = lineCreaterLerpBehaviour.GetLerpPoints();
            lerpBehaviour.index = lineCreaterLerpBehaviour.index;
            lerpBehaviour.t = lineCreaterLerpBehaviour.t;
            //lerpBehaviour.enabled = false;
        }
    }

    void AllDestroy()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("lines");

        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
    }
}
