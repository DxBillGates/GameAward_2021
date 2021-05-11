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
        //���[�v�|�C���g�̎擾�p
        LerpBehaviour player = GameObject.Find("Player").GetComponent<LerpBehaviour>();

        //�v���n�u����ǉ���������擾
        ObjectBehavior prefabObjBehavior = prefab.GetComponent<ObjectBehavior>();
        LerpBehaviour prefabLerpBehavior = prefab.GetComponent<LerpBehaviour>();
        EnemyBehaviour prefabEnemyBehavior = prefab.GetComponent<EnemyBehaviour>();
        AttackBehaviour prefabAttackBehavior = prefab.GetComponent<AttackBehaviour>();


        //�V�K�I�u�W�F�N�g�̐���
        GameObject newGameObject = Instantiate(prefab);
        newGameObject.name = "Boss";

        //newGameObject.transform.localScale = new Vector3(3, 3, 3);

        Boss2Behaviour boss2Behaviour = newGameObject.GetComponent<Boss2Behaviour>();
        if(boss2Behaviour)
        {
            boss2Behaviour.creater = gameObject;
        }

        //�R���C�_�[�̐؂�ւ�
        //Destroy(newGameObject.GetComponent<Collider>());
        //BoxCollider newCollider = newGameObject.AddComponent<BoxCollider>();
        //newCollider.size = Vector3.one;


        //�r�w�C�r�A��ǉ����擾
        ObjectBehavior newObjectBehavior = newGameObject.GetComponent<ObjectBehavior>();
        LerpBehaviour newLerpBehvior = newGameObject.GetComponent<LerpBehaviour>();
        EnemyBehaviour newEnemyBehavior = newGameObject.GetComponent<EnemyBehaviour>();
        AttackBehaviour newAttackBehavior = newGameObject.GetComponent<AttackBehaviour>();

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

        ////���f���ݒ�
        //MeshFilter newMeshFilter = newGameObject.GetComponent<MeshFilter>();
        //MeshRenderer newMeshRenderer = newGameObject.GetComponent<MeshRenderer>();
        //if (rightMesh && leftMesh)
        //{
        //    if (!leftMode)
        //    {
        //        newMeshFilter.mesh = rightMesh;
        //    }
        //    else
        //    {
        //        newMeshFilter.mesh = leftMesh;
        //    }
        //}
        //if (material)
        //{
        //    newMeshRenderer.material = material;
        //}

        isCreate = true;
    }
}
