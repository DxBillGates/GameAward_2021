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
        //���[�v�|�C���g�̎擾�p
        LerpBehaviour player = GameObject.Find("Player").GetComponent<LerpBehaviour>();

        //�v���n�u����ǉ���������擾
        ObjectBehavior prefabObjBehavior = prefabEnemy.GetComponent<ObjectBehavior>();
        LerpBehaviour prefabLerpBehavior = prefabEnemy.GetComponent<LerpBehaviour>();
        EnemyBehaviour prefabEnemyBehavior = prefabEnemy.GetComponent<EnemyBehaviour>();
        AttackBehaviour prefabAttackBehavior = prefabEnemy.GetComponent<AttackBehaviour>();

        //�V�K�I�u�W�F�N�g�̐���
        GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        newGameObject.name = "CreatedEnemy";

        //�R���C�_�[�̐؂�ւ�
        Destroy(newGameObject.GetComponent<Collider>());
        newGameObject.AddComponent<BoxCollider>();

        //�r�w�C�r�A��ǉ����擾
        ObjectBehavior newObjectBehavior = newGameObject.AddComponent<ObjectBehavior>();
        LerpBehaviour newLerpBehvior = newGameObject.AddComponent<LerpBehaviour>();
        EnemyBehaviour newEnemyBehavior = newGameObject.AddComponent<EnemyBehaviour>();
        AttackBehaviour newAttackBehavior = newGameObject.AddComponent<AttackBehaviour>();

        //�I�u�W�F�N�g�r�w�C�r�A�̐ݒ�
        newObjectBehavior.fallVector = fallVector.normalized / 10;

        //���[�v�r�w�C�r�A�̐ݒ�
        newLerpBehvior.frontList = player.frontList;
        newLerpBehvior.backList = player.backList;
        newLerpBehvior.isAutoMove = true;
        newLerpBehvior.leftMove = leftMode;
        newLerpBehvior.lerpPoints = new List<Transform>();

        //�G�l�~�[�r�w�C�r�A�̐ݒ�
        newEnemyBehavior.onMove = false;
        newEnemyBehavior.damageSpan = prefabEnemyBehavior.damageSpan;
        newEnemyBehavior.hp = prefabEnemyBehavior.hp;

        //�A�^�b�N�r�w�C�r�A�̐ݒ�
        newAttackBehavior.timeSpan = prefabAttackBehavior.timeSpan;
        newAttackBehavior.attackValue = prefabAttackBehavior.attackValue;

        //�����I�u�W�F�N�g�̏ڍאݒ�
        newGameObject.tag = "enemy";
        newGameObject.transform.position = transform.position;
        newGameObject.layer = 6;
        newGameObject.transform.rotation = Quaternion.FromToRotation(newGameObject.transform.up, -fallVector) * newGameObject.transform.rotation;
        newGameObject.transform.rotation = Quaternion.FromToRotation(newGameObject.transform.right, -newGameObject.transform.right) * newGameObject.transform.rotation;

        //���f���ݒ�
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
