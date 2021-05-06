using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLerpBehaviour : MonoBehaviour
{
    public List<Transform> frontList;
    public List<Transform> backList;
    public List<Vector3> lerpPoints;
    public int index;
    public bool lerpMode;
    public bool oldLerpMode;
    public bool frontLerpMode;
    public float t;
    public bool isAutoMove;
    public bool isMove;
    public string str;
    public bool leftMove;
    public ChangePartsSystemBehavior changePartsSystemBehavior;
    ObjectBehavior objectBehavior;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        //Initialize();
        //frontLerpMode = false;
        //oldLerpMode = false;
        //isMove = false;
        objectBehavior = GetComponent<ObjectBehavior>();
        changePartsSystemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        //leftMove = true;
        //lerpPoints = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        objectBehavior.speed = Vector3.Distance(frontList[0].position, frontList[1].position) + Vector3.Distance(frontList[1].position, frontList[2].position) / 2;
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
                        //SetRotate();
                        return;
                    }
                }
                if (index == lerpPoints.Count - 1)
                {
                    if (t > 1)
                    {
                        Initialize();
                        //SetRotate();
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
                    //SetRotate();
                    return;
                }
                transform.position = Vector3.Lerp(lerpPoints[index], lerpPoints[index + 1], t);

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

        if(oldLerpMode && !lerpMode)
        {
            SetRotate();
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
        float speed = Time.deltaTime * (objectBehavior.speed + objectBehavior.addSpeed) * Time.deltaTime * objectBehavior.speed;
        if (isAutoMove)
        {
            if (frontLerpMode)
            {
                if (leftMove)
                {
                    t -= speed;
                    isMove = true;
                }
                else
                {
                    t += speed;
                    isMove = true;
                }
            }
            else
            {
                if (leftMove)
                {
                    t += speed;
                    isMove = true;
                }
                else
                {
                    t -= speed;
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
                    t -= speed;
                    isMove = true;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    isMove = true;
                    t += speed;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    isMove = true;
                    t += speed;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    isMove = true;
                    t -= speed;
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
                lerpPoints.Add(transforms[i].position);
            }
        }
        else
        {
            for (int i = transforms.Count - 1; i >= 0; --i)
            {
                lerpPoints.Add(transforms[i].position);
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

    void SetRotate()
    {
        //接地面の法線とZ軸（Vector3(0,0,1)）から適切なX軸を生成してその方向を向かせる
        float z = (transform.forward.z >= 0) ? 1 : -1;
        Vector3 newRightVector = Vector3.Cross(transform.up, new Vector3(0, 0, z));
        transform.rotation = Quaternion.FromToRotation(transform.right, newRightVector) * transform.rotation;
    }

    public void AnotherMoveFunc()
    {
        objectBehavior.speed = Vector3.Distance(frontList[0].position, frontList[1].position) + Vector3.Distance(frontList[1].position, frontList[2].position) / 2;
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
                        SetRotate();
                        return;
                    }
                }
                if (index == lerpPoints.Count - 1)
                {
                    if (t > 1)
                    {
                        Initialize();
                        SetRotate();
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
                    SetRotate();
                    return;
                }
                transform.position = Vector3.Lerp(lerpPoints[index], lerpPoints[index + 1], t);

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

}
