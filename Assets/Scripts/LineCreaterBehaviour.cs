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
    BatteryBehaviour batteryBehaviour;
    float addDamage;
    GameObject amplifier;

    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        amplifier = null;
        addDamage = 0;
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
        isCreate = false;
        t = 0;
        lineCreater.transform.position = transform.position;
        lineCreater.transform.rotation = transform.rotation;
        batteryBehaviour = GetComponent<BatteryBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCreate && !lineCreaterObjectBehaviour.changePartsSystemBehavior.changeMode && batteryBehaviour.outputAmount > 0 && batteryBehaviour.amountEnergy > 0)
        {
            while (true)
            {
                LineCreaterMoveFunction();
                lineCreaterObjectBehaviour.AnotherUpdate();
                lineCreaterLerpBehaviour.AnotherMoveFunc();
                CreateTestObject(0.5f);
                if (Vector3.Distance(lineCreater.transform.position, endObject.transform.position) <= 1)
                {
                    isCreate = false;
                    isDelete = false;
                    lineCreater.transform.position = transform.position;
                    lineCreater.transform.rotation = transform.rotation;
                    amplifier = null;
                    addDamage = 0;
                    Debug.Log("成功しました");
                    break;
                }

                time += Time.deltaTime;
                if (time >= 120)
                {
                    lineCreater.transform.position = transform.position;
                    lineCreater.transform.rotation = transform.rotation;
                    amplifier = null;
                    addDamage = 0;
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

        if (Physics.Raycast(lineCreater.transform.position, -lineCreater.transform.right, out hit, 1))
        {
            if (hit.collider.gameObject.CompareTag("amplifier"))
            {
                if (hit.collider.gameObject != amplifier)
                {
                    //Debug.Log("Hit");
                    //Debug.Log("HitObjName:" + hit.collider.gameObject.name);
                    //Debug.Log("AddDamage:" + hit.collider.gameObject.GetComponent<AmplifierBehaviour>().addValue);
                    addDamage += hit.collider.gameObject.GetComponent<AmplifierBehaviour>().addValue;
                    //Debug.Log("ResultDamage:" + addDamage);
                    amplifier = hit.collider.gameObject;
                }
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

            GameObject newTestObject = Instantiate(prefab);
            newTestObject.transform.position = lineCreater.transform.position;
            newTestObject.transform.rotation = lineCreater.transform.rotation;

            //MeshFilter mf = newTestObject.GetComponent<MeshFilter>();
            //mf.mesh = prefab.GetComponent<MeshFilter>().mesh;

            newTestObject.transform.localScale /= 2;

            ObjectBehavior objBehaviour = newTestObject.GetComponent<ObjectBehavior>();
            NewLerpBehaviour lerpBehaviour = newTestObject.GetComponent<NewLerpBehaviour>();
            MoveBehaviour moveBehaviour = newTestObject.GetComponent<MoveBehaviour>();
            moveBehaviour.start = gameObject;
            moveBehaviour.end = endObject;
            moveBehaviour.enabled = true;
            moveBehaviour.batteryBehaviour = batteryBehaviour;
            moveBehaviour.addDamageValue = addDamage;
            //ラープビヘイビアの設定
            lerpBehaviour.enabled = true;
            lerpBehaviour.frontList = lineCreaterLerpBehaviour.frontList;
            lerpBehaviour.backList = lineCreaterLerpBehaviour.backList;
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
            //GameObject newTestObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //newTestObject.tag = "lines";
            //newTestObject.transform.position = lineCreater.transform.position;
            //newTestObject.transform.rotation = lineCreater.transform.rotation;
            //newTestObject.AddComponent<Rigidbody>().useGravity = false;

            //newTestObject.GetComponent<Collider>().isTrigger = true;

            //LerpBehaviour player = GameObject.Find("Player").GetComponent<LerpBehaviour>();

            //newTestObject.transform.localScale /= 2;

            //MeshFilter mf = newTestObject.GetComponent<MeshFilter>();
            //mf.mesh = setMesh;
            //MeshRenderer mr = newTestObject.GetComponent<MeshRenderer>();
            //mr.material = setMaterial;
            //ObjectBehavior objBehaviour = newTestObject.AddComponent<ObjectBehavior>();
            //LineLerpBehaviour lerpBehaviour = newTestObject.AddComponent<LineLerpBehaviour>();
            //MoveBehaviour moveBehaviour = newTestObject.AddComponent<MoveBehaviour>();
            //moveBehaviour.start = gameObject;
            //moveBehaviour.end = endObject;
            //moveBehaviour.enabled = true;
            //moveBehaviour.batteryBehaviour = batteryBehaviour;
            ////ラープビヘイビアの設定
            //lerpBehaviour.enabled = true;
            //lerpBehaviour.frontList = player.frontList;
            //lerpBehaviour.backList = player.backList;
            //lerpBehaviour.lerpPoints = new List<Vector3>();

            //lerpBehaviour.isAutoMove = lineCreaterLerpBehaviour.isAutoMove;
            //lerpBehaviour.leftMove = lineCreaterLerpBehaviour.leftMove;
            //lerpBehaviour.frontLerpMode = lineCreaterLerpBehaviour.frontLerpMode;
            //lerpBehaviour.str = lineCreaterLerpBehaviour.str;
            //lerpBehaviour.isMove = lineCreaterLerpBehaviour.isMove;
            //lerpBehaviour.oldLerpMode = lineCreaterLerpBehaviour.oldLerpMode;

            //lerpBehaviour.lerpMode = lineCreaterLerpBehaviour.lerpMode;
            ////lerpBehaviour.lerpPoints = new List<Transform>();

            //lerpBehaviour.lerpPoints = lineCreaterLerpBehaviour.GetLerpPoints();
            //lerpBehaviour.index = lineCreaterLerpBehaviour.index;
            //lerpBehaviour.t = lineCreaterLerpBehaviour.t;
            ////lerpBehaviour.enabled = false;
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

    //public void CreateLine()
    //{
    //    isCreate = true;
    //    if (isCreate && !lineCreaterObjectBehaviour.changePartsSystemBehavior.changeMode && batteryBehaviour.outputAmount > 0)
    //    {
    //        while (true)
    //        {
    //            LineCreaterMoveFunction();
    //            lineCreaterObjectBehaviour.AnotherUpdate();
    //            lineCreaterLerpBehaviour.AnotherMoveFunc();
    //            CreateTestObject(0.5f);
    //            if (Vector3.Distance(lineCreater.transform.position, endObject.transform.position) <= 1)
    //            {
    //                isCreate = false;
    //                isDelete = false;
    //                lineCreater.transform.position = transform.position;
    //                lineCreater.transform.rotation = transform.rotation;
    //                Debug.Log("成功しました");
    //                break;
    //            }

    //            time += Time.deltaTime;
    //            if (time >= 120)
    //            {
    //                Debug.Log("失敗しました");
    //                time = 0;
    //                isCreate = false;
    //                isDelete = false;
    //                break;
    //            }
    //        }
    //    }

    //}
}
