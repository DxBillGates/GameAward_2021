using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    //Rigidbody������ϐ�
    Rigidbody rigid;
    //���x
    Vector3 velocity;
    //���˂���Ƃ��̏����ʒu
    Vector3 position;
    // �����x
    public Vector3 acceleration;
    // �^�[�Q�b�g���Z�b�g����
    public Transform target;
    // ���e����
    float period = 2f;


    void Start()
    {

        // �����ʒu��posion�Ɋi�[
        position = transform.position;
        // rigidbody�擾
        rigid = this.GetComponent<Rigidbody>();


        velocity = new Vector3(35, 35, 0);

    }

    void Update()
    {

        acceleration = Vector3.zero;

        //�^�[�Q�b�g�Ǝ������g�̍�
        var diff = target.position - transform.position;

        //�����x
        acceleration += (diff - velocity * period) * 2f
                        / (period * period);


        //�����x�����ȏゾ�ƒǔ����キ����
        //if (acceleration.magnitude > 100f)
        //{
        //    acceleration = acceleration.normalized * 100f;
        //}

        // ���e���Ԃ����X�Ɍ��炵�Ă���
        period -= Time.deltaTime;

        // ���x�̌v�Z
        velocity += acceleration * Time.deltaTime;

    }

    void FixedUpdate()
    {
        // �ړ�����
        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    void OnCollisionEnter()
    {
        // �����ɓ��������玩�����g���폜
        Destroy(this.gameObject);

    }
}
