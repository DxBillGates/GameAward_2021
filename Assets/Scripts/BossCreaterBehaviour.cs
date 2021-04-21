using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreaterBehaviour : MonoBehaviour
{
    public bool leftMode;
    public int nowSacrificeCount;
    public int maxSacrificeCount;
    public bool drawRay;
    public Vector3 fallVector;
    public Mesh rightMesh, leftMesh;
    public Material material;
    public GameObject prefab;

    public bool isCreate;
    // Start is called before the first frame update
    void Start()
    {
        isCreate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (drawRay)
            Debug.DrawRay(transform.position, fallVector * 10, Color.red);

        if(nowSacrificeCount >= maxSacrificeCount && !isCreate)
        {
            Create();
        }
    }

    void Create()
    {
        //ラープポイントの取得用
        LerpBehaviour player = GameObject.Find("Player").GetComponent<LerpBehaviour>();

        //プレハブから追加する情報を取得
        ObjectBehavior prefabObjBehavior = prefab.GetComponent<ObjectBehavior>();
        LerpBehaviour prefabLerpBehavior = prefab.GetComponent<LerpBehaviour>();
        EnemyBehaviour prefabEnemyBehavior = prefab.GetComponent<EnemyBehaviour>();
        AttackBehaviour prefabAttackBehavior = prefab.GetComponent<AttackBehaviour>();

        //新規オブジェクトの生成
        GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        newGameObject.name = "Boss";

        newGameObject.transform.localScale = new Vector3(3, 3, 3);


        //コライダーの切り替え
        Destroy(newGameObject.GetComponent<Collider>());
        BoxCollider newCollider = newGameObject.AddComponent<BoxCollider>();
        newCollider.size = Vector3.one;


        //ビヘイビアを追加＆取得
        ObjectBehavior newObjectBehavior = newGameObject.AddComponent<ObjectBehavior>();
        LerpBehaviour newLerpBehvior = newGameObject.AddComponent<LerpBehaviour>();
        EnemyBehaviour newEnemyBehavior = newGameObject.AddComponent<EnemyBehaviour>();
        AttackBehaviour newAttackBehavior = newGameObject.AddComponent<AttackBehaviour>();

        //オブジェクトビヘイビアの設定
        newObjectBehavior.fallVector = fallVector.normalized / 10;

        //ラープビヘイビアの設定
        newLerpBehvior.frontList = player.frontList;
        newLerpBehvior.backList = player.backList;
        newLerpBehvior.isAutoMove = true;
        newLerpBehvior.leftMove = leftMode;

        //エネミービヘイビアの設定
        newEnemyBehavior.onMove = false;
        newEnemyBehavior.damageSpan = prefabEnemyBehavior.damageSpan;
        newEnemyBehavior.hp = prefabEnemyBehavior.hp;

        //アタックビヘイビアの設定
        newAttackBehavior.timeSpan = prefabAttackBehavior.timeSpan;
        newAttackBehavior.attackValue = prefabAttackBehavior.attackValue;

        //生成オブジェクトの詳細設定
        newGameObject.tag = "enemy";
        newGameObject.transform.position = transform.position;
        newGameObject.layer = 6;
        newGameObject.transform.rotation = Quaternion.FromToRotation(newGameObject.transform.up, -fallVector) * newGameObject.transform.rotation;
        newGameObject.transform.rotation = Quaternion.FromToRotation(newGameObject.transform.right, -newGameObject.transform.right) * newGameObject.transform.rotation;

        //モデル設定
        MeshFilter newMeshFilter = newGameObject.GetComponent<MeshFilter>();
        MeshRenderer newMeshRenderer = newGameObject.GetComponent<MeshRenderer>();
        if (rightMesh && leftMesh)
        {
            if (!leftMode)
            {
                newMeshFilter.mesh = rightMesh;
            }
            else
            {
                newMeshFilter.mesh = leftMesh;
            }
        }
        if (material)
        {
            newMeshRenderer.material = material;
        }

        isCreate = true;
    }
}
