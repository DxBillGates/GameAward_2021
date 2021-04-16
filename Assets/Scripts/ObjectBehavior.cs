using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public Vector3 onNormal;
    public bool isFly;
    public ChangePartsSystemBehavior changePartsSystemBehavior { get; set; }
    void Start()
    {
        onNormal = new Vector3();
        isFly = true;
        changePartsSystemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
    }

    void Update()
    {
        SetParent();
        if (isFly)
        {
            //transform.position += -onNormal / 10;
        }
    }

    public void SetParent()
    {
        RaycastHit hit;
        //下方にレイキャストを行い入れ替え中でなければ乗っているパーツと親子関係を設定
        if (Physics.Raycast(transform.position, -transform.up, out hit, 5))
        {
            if (hit.collider.gameObject.layer == 7)
            {
                onNormal = hit.normal;
                if (!changePartsSystemBehavior.changeMode)
                {
                    transform.parent = hit.collider.gameObject.transform;
                    if (hit.distance <= 1)
                    {
                        isFly = false;
                        transform.position += onNormal / 10;
                    }
                    else
                    {
                        isFly = true;
                    }
                }
            }
        }
    }
}
