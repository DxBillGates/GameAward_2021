using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreaterBehavior : MonoBehaviour
{
    public bool leftMode;
    public Vector3 fallVector;
    public bool drawRay;
    public float createTimeSpan;
    public int maxEnemy;
    public Mesh rightMesh;
    public Mesh setMesh;
    public Material setMaterial;
    int count;
    float t;
    public GameObject prefabEnemy;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        t = 0;
        if (createTimeSpan == 0)
        {
            createTimeSpan = 3;
        }

        GetComponent<Renderer>().material.color = (leftMode) ? Color.blue : Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (drawRay)
            Debug.DrawRay(transform.position, fallVector * 10, Color.red);

        if (t >= createTimeSpan && count < maxEnemy)
        {
            Create();
            count++;
            t = 0;
        }

        t += Time.deltaTime;
    }

    void Create()
    {
        //ラープポイントの取得用
        LerpBehaviour player = GameObject.Find("Player").GetComponent<LerpBehaviour>();

        //プレハブから追加する情報を取得
        ObjectBehavior prefabObjBehavior = prefabEnemy.GetComponent<ObjectBehavior>();
        LerpBehaviour prefabLerpBehavior = prefabEnemy.GetComponent<LerpBehaviour>();
        EnemyBehaviour prefabEnemyBehavior = prefabEnemy.GetComponent<EnemyBehaviour>();
        AttackBehaviour prefabAttackBehavior = prefabEnemy.GetComponent<AttackBehaviour>();

        //新規オブジェクトの生成
        GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        newGameObject.name = "CreatedEnemy";

        //コライダーの切り替え
        Destroy(newGameObject.GetComponent<Collider>());
        newGameObject.AddComponent<BoxCollider>();

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
        newLerpBehvior.lerpPoints = new List<Transform>();

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
        if (setMesh)
        {
            if (!leftMode)
            {
                newMeshFilter.mesh = rightMesh;
            }
            else
            {
                newMeshFilter.mesh = setMesh;
            }
        }
        if (setMaterial)
        {
            newMeshRenderer.material = setMaterial;
        }
    }
}
