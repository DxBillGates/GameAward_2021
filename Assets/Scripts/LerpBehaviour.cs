using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBehaviour : MonoBehaviour
{
    public List<Transform> frontList;
    public List<Transform> backList;
    public List<Transform> lerpPoints;
    public int index;
    public bool lerpMode;
    public bool oldLerpMode;
    public bool frontLerpMode;
    public float t;
    public bool isAutoMove;
    bool isMove;
    public string str;
    public bool leftMove;
    ChangePartsSystemBehavior changePartsSystemBehavior;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        frontLerpMode = false;
        oldLerpMode = false;
        isMove = false;
        changePartsSystemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        //leftMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!oldLerpMode && !changePartsSystemBehavior.changeMode)
            if (CheckHitPoint(backList[0].position))
            {
                lerpMode = true;
                frontLerpMode = true;
                AddList(true, backList);
                str = "a";
            }
            else if (CheckHitPoint(backList[2].position))
            {
                lerpMode = true;
                frontLerpMode = false;
                AddList(false, backList);
                str = "b";
            }
            else if (CheckHitPoint(frontList[0].position))
            {
                lerpMode = true;
                frontLerpMode = false;
                AddList(true, frontList);
                str = "c";
            }
            else if (CheckHitPoint(frontList[2].position))
            {
                lerpMode = true;
                frontLerpMode = true;
                AddList(false, frontList);
                str = "d";
            }

        if (!changePartsSystemBehavior.changeMode)
            if (lerpMode)
            {
                Move();
                if (index == 0)
                {
                    if (t < 0)
                    {
                        Initialize();
                        transform.rotation = Quaternion.FromToRotation(transform.right, new Vector3(1, 0, 0)) * transform.rotation;
                        return;
                    }
                }
                if (index == lerpPoints.Count - 1)
                {
                    if (t > 1)
                    {
                        Initialize();
                        transform.rotation = Quaternion.FromToRotation(transform.right, new Vector3(1, 0, 0)) * transform.rotation;
                        return;
                    }
                }
                if (t > 1)
                {
                    t = 1;
                }
                if (t < 0)
                {
                    t = 0;
                }

                if (index >= lerpPoints.Count - 1)
                {
                    Initialize();
                    transform.rotation = Quaternion.FromToRotation(transform.right, new Vector3(1, 0, 0)) * transform.rotation;
                    return;
                }
                transform.position = Vector3.Lerp(lerpPoints[index].position, lerpPoints[index + 1].position, t);

                if (isMove)
                {
                    if (t == 1)
                    {
                        ++index;
                        t = 0;
                        isMove = false;
                    }
                    else if (t == 0)
                    {
                        --index;
                        t = 1;
                        isMove = false;
                    }
                }
            }

        oldLerpMode = lerpMode;
    }

    bool CheckHitPoint(Vector3 p)
    {
        if (Vector3.Distance(p, transform.position) < 1)
        {
            return true;
        }
        return false;
    }

    void Move()
    {
        if (isAutoMove)
        {
            if (frontLerpMode)
            {
                if (leftMove)
                {
                    t -= Time.deltaTime;
                    isMove = true;
                }
                else
                {
                    t += Time.deltaTime;
                    isMove = true;
                }
            }
            else
            {
                if (leftMove)
                {
                    t += Time.deltaTime;
                    isMove = true;
                }
                else
                {
                    t -= Time.deltaTime;
                    isMove = true;
                }
            }
        }
        else
        {
            if (frontLerpMode)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    t -= Time.deltaTime;
                    isMove = true;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    isMove = true;
                    t += Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    isMove = true;
                    t += Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    isMove = true;
                    t -= Time.deltaTime;
                }
            }
        }
    }

    void AddList(bool isFront, List<Transform> transforms)
    {
        lerpPoints.Clear();
        if (isFront)
        {
            for (int i = 0; i < transforms.Count; ++i)
            {
                lerpPoints.Add(transforms[i]);
            }
        }
        else
        {
            for (int i = transforms.Count - 1; i >= 0; --i)
            {
                lerpPoints.Add(transforms[i]);
            }
        }
    }

    void Initialize()
    {
        index = 0;
        t = 0;
        lerpMode = false;
        isMove = false;
    }

}
