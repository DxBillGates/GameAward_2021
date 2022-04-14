using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 威力増加オブジェクトのコンポーネント
/// </summary>
public class AmplifierBehaviour : MonoBehaviour
{
    private float addValue { get; set; }
    ObjectBehavior objectBehavior;
    // Start is called before the first frame update
    void Start()
    {
        // 各オブジェクトの基底コンポーネントを取得
        objectBehavior = GetComponent<ObjectBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // メビウスのパーツにしっかりと立てるように調整
        transform.rotation = Quaternion.FromToRotation(transform.up, objectBehavior.onNormal) * transform.rotation;
    }
}
