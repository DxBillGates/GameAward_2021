using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePartsSystemBehavior : MonoBehaviour
{
    public bool isSelectFirstObj, changePreMode, changeMode;
    GameObject firstObj, secondObj;
    Vector3 fPos, sPos;
    Vector3 fAngle, sAngle;
    float t;
    bool isMouseChange;
    public bool isMouse;
    public GameObject setObject { get; set; }
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (isMouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!changeMode)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Color color = Color.red;
                    color.a = 0.25f;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (!isSelectFirstObj)
                        {
                            isSelectFirstObj = true;
                            firstObj = hit.collider.gameObject;
                            fPos = firstObj.transform.position;
                            fAngle = firstObj.transform.eulerAngles;

                            firstObj.GetComponent<Renderer>().material.color = color;
                        }
                        else
                        {
                            secondObj = hit.collider.gameObject;
                            sPos = secondObj.transform.position;
                            sAngle = secondObj.transform.eulerAngles;
                            changePreMode = true;
                            changeMode = true;
                            secondObj.GetComponent<Renderer>().material.color = color;
                            isMouseChange = true;
                        }
                    }
                }
            }

            if (changeMode)
            {
                if (isMouseChange)
                    Change();
            }
        }
    }

    public void Change()
    {
        if (t >= 1)
        {
            t = 1;
            isSelectFirstObj = changePreMode = changeMode = isMouseChange = false;
        }

        firstObj.transform.position = Vector3.Lerp(fPos, sPos, t);
        secondObj.transform.position = Vector3.Lerp(sPos, fPos, t);

        firstObj.transform.rotation = Quaternion.Euler(Vector3.Lerp(fAngle, sAngle, t));
        secondObj.transform.rotation = Quaternion.Euler(Vector3.Lerp(sAngle, fAngle, t));

        t += Time.deltaTime;
        //入れ替え終了
        if (!changeMode)
        {
            Color color = new Color(1, 1, 1, 0.25f);
            firstObj.GetComponent<Renderer>().material.color = color;
            secondObj.GetComponent<Renderer>().material.color = color;
            firstObj = secondObj = null;
            t = 0;
        }
    }

    public void Change(GameObject gameObject, GameObject child)
    {
        if (!isMouseChange)
            if (!changeMode)
            {
                RaycastHit hit;
                float maxDistance = 10;
                Color color = Color.red;
                color.a = 0.25f;
                if (Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, out hit, maxDistance))
                {
                    isSelectFirstObj = true;
                    firstObj = hit.collider.gameObject;
                    fPos = firstObj.transform.position;
                    fAngle = firstObj.transform.eulerAngles;

                    firstObj.GetComponent<Renderer>().material.color = color;
                }

                if (Physics.Raycast(child.transform.position, -child.transform.up, out hit, maxDistance))
                {
                    secondObj = hit.collider.gameObject;
                    sPos = secondObj.transform.position;
                    sAngle = secondObj.transform.eulerAngles;
                    changePreMode = true;
                    changeMode = true;
                    child.transform.parent = secondObj.transform;

                    secondObj.GetComponent<Renderer>().material.color = color;
                }
            }
            else
            {
                Change();
            }
    }

    public void Initialize()
    {
        if(setObject)
        {
            Color color = new Color(1, 1, 1, 0.25f);
            setObject.GetComponent<Renderer>().material.color = color;
            setObject = null;
        }
        if (firstObj)
        {
            Color color = new Color(1, 1, 1, 0.25f);
            firstObj.GetComponent<Renderer>().material.color = color;
            if (secondObj)
            {
                secondObj.GetComponent<Renderer>().material.color = color;
            }
            firstObj = secondObj = null;
        }

        isSelectFirstObj = changePreMode = changeMode = false;
        firstObj = secondObj = null;
        fPos = sPos = new Vector3();
        fAngle = sAngle = new Vector3();
        t = 0;
        isMouseChange = false;
    }
}
