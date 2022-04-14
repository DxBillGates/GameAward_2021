using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�̍U���R���|�[�l���g
/// </summary>
public class AttackBehaviour : MonoBehaviour
{
    // �U������I�u�W�F�N�g�̍��W
    Vector3 target;

    // �U���Ԋu
    public float timeSpan { get; set; }

    // �t���[���J�E���^�[
    float t;

    // �U����
    public int attackValue { get; set; }

    // ����ւ��V�X�e���̃R���|�[�l���g
    private ChangePartsSystemBehavior systemBehavior;

    // �e�̃��b�V��
    [SerializeField]
    Mesh bulletModel;

    // Start is called before the first frame update
    void Start()
    {
        // �t���[���J�E���^�[��������
        t = 0;

        // �U���ڕW�̍��W������
        target = new Vector3(0, 25, 0);

        // ����ւ��V�X�e���̃R���|�[�l���g���擾
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // ����ւ��V�X�e���������ւ������m�F��
        // true�Ȃ�U���Ԋu�����ƂɍU���I�u�W�F�N�g�𐶐�
        if (!systemBehavior.changeMode)
        {
            if (t >= timeSpan)
            {
                // �e�̃X�s�[�h
                const float SPEED = 6;

                // �e���o������
                const float HEIGHT = 10;

                // �t���[���J�E���^�[������
                t = 0;

                // �e�I�u�W�F�N�g����
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
            // ���C�L���X�g���s���R�A�I�u�W�F�N�g������ɂ��邩�m�F
            // ����΃t���[���J�E���^�[��i�߂�
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
