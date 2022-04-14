using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の攻撃コンポーネント
/// </summary>
public class AttackBehaviour : MonoBehaviour
{
    // 攻撃するオブジェクトの座標
    Vector3 target;

    // 攻撃間隔
    public float timeSpan { get; set; }

    // フレームカウンター
    float t;

    // 攻撃力
    public int attackValue { get; set; }

    // 入れ替えシステムのコンポーネント
    private ChangePartsSystemBehavior systemBehavior;

    // 弾のメッシュ
    [SerializeField]
    Mesh bulletModel;

    // Start is called before the first frame update
    void Start()
    {
        // フレームカウンターを初期化
        t = 0;

        // 攻撃目標の座標初期化
        target = new Vector3(0, 25, 0);

        // 入れ替えシステムのコンポーネントを取得
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // 入れ替えシステムから入れ替え中か確認し
        // trueなら攻撃間隔をもとに攻撃オブジェクトを生成
        if (!systemBehavior.changeMode)
        {
            if (t >= timeSpan)
            {
                // 弾のスピード
                const float SPEED = 6;

                // 弾を出す高さ
                const float HEIGHT = 10;

                // フレームカウンター初期化
                t = 0;

                // 弾オブジェクト生成
                GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                bullet.GetComponent<MeshFilter>().mesh = bulletModel;
                BulletBehavior bulletBehavior = bullet.AddComponent<BulletBehavior>();
                bulletBehavior.attackValue = attackValue;
                bullet.transform.position = transform.position + transform.up * HEIGHT;
                bullet.layer = 12;
                Rigidbody rb = bullet.AddComponent<Rigidbody>();
                rb.useGravity = false;
                bulletBehavior.vector = (target - transform.position).normalized *  SPEED;
            }

            RaycastHit hit;
            // レイキャストを行いコアオブジェクトが頭上にあるか確認
            // あればフレームカウンターを進める
            if (Physics.Raycast(transform.position + transform.up * 10, transform.up, out hit))
            {
                if (hit.collider.gameObject.name == "Core")
                {
                    t += Time.deltaTime;
                }
                else
                {
                    t = 0;
                }
            }
        }
    }
}
