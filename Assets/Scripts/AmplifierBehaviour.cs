using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �З͑����I�u�W�F�N�g�̃R���|�[�l���g
/// </summary>
public class AmplifierBehaviour : MonoBehaviour
{
    private float addValue { get; set; }
    ObjectBehavior objectBehavior;
    // Start is called before the first frame update
    void Start()
    {
        // �e�I�u�W�F�N�g�̊��R���|�[�l���g���擾
        objectBehavior = GetComponent<ObjectBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���r�E�X�̃p�[�c�ɂ�������Ɨ��Ă�悤�ɒ���
        transform.rotation = Quaternion.FromToRotation(transform.up, objectBehavior.onNormal) * transform.rotation;
    }
}
