using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBehaviour : MonoBehaviour
{
    public float hp;
    public float recovery;
    float maxHp;
    new Renderer renderer;
    Color color;
    float angle;
    bool isDamage, oldIsDamage;
    float noDamageTime;
    public float regenerationSpan;
    bool scaleLerp;
    float scaleLerpT;
    bool minusScaleLerp;
    ChangePartsSystemBehavior systemBehavior;
    public RectTransform hpUiTransform;
    float hpUiXScale;
    public float killEnemyCountCurrentFrame;
    public float killEnemyCountBeforeFrame;
    int frame;
    Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        size = transform.localScale;
        minusScaleLerp = false;
        isDamage = oldIsDamage = false;
        noDamageTime = 0;
        angle = 0;
        maxHp = hp;
        renderer = GetComponent<Renderer>();
        color = new Color(1, 1, 1, 1);
        scaleLerp = false;
        scaleLerpT = 0;
        if (regenerationSpan == 0)
        {
            regenerationSpan = 2;
        }
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        hpUiXScale = hpUiTransform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (frame % 10 == 0)
        {
            killEnemyCountBeforeFrame = killEnemyCountCurrentFrame;
            killEnemyCountCurrentFrame = 0;
            if (killEnemyCountBeforeFrame != 0)
            {
                //Debug.Log(killEnemyCountBeforeFrame + ":" + frame);
            }

            if (killEnemyCountBeforeFrame >= 10)
            {
                hp += 10;
            }
        }

        Vector3 newHpUiScale = new Vector3(1, 1, 1);
        newHpUiScale.x = hpUiXScale * hp / maxHp;
        newHpUiScale.y = hpUiTransform.localScale.y;
        newHpUiScale.z = hpUiTransform.localScale.z;

        hpUiTransform.localScale = newHpUiScale;

        if (!systemBehavior.changeMode)
        {
            isDamage = false;
            color.a = hp / maxHp;
            renderer.material.color = color;
            transform.rotation = Quaternion.Euler(angle, 0, angle);

            angle += Time.deltaTime * 10;


            if (!oldIsDamage)
            {
                noDamageTime += Time.deltaTime;
            }
            if (noDamageTime >= regenerationSpan)
            {
                if (hp < maxHp)
                {
                    hp += recovery;
                    if (hp > maxHp) hp = maxHp;
                    scaleLerp = true;
                    noDamageTime = 0;
                }
            }

            if (scaleLerp)
            {
                scaleLerpT += Time.deltaTime + Time.deltaTime * 2;
                if (minusScaleLerp)
                {
                    if (scaleLerpT >= 1)
                    {
                        scaleLerp = false;
                        minusScaleLerp = false;
                        scaleLerpT = 1;
                    }
                    transform.localScale = Vector3.Lerp(size, size - new Vector3(30, 30, 30), easeInOutQuint(scaleLerpT));
                }
                else
                {
                    if (scaleLerpT >= 1)
                    {
                        scaleLerp = false;
                        scaleLerpT = 1;
                    }
                    transform.localScale = Vector3.Lerp(size, size + new Vector3(30, 30, 30), easeInOutQuint(scaleLerpT));
                }
            }
            else
            {
                scaleLerpT = 0;
                transform.localScale = size;
            }

            oldIsDamage = isDamage;
        }
        ++frame;
    }

    void Damage(int damage)
    {
        noDamageTime = 0;
        hp -= damage;
        isDamage = true;
        scaleLerp = true;
        minusScaleLerp = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.collider.gameObject);
        BulletBehavior bulletBehavior = collision.gameObject.GetComponent<BulletBehavior>();
        if (bulletBehavior)
        {
            Damage(bulletBehavior.attackValue);
        }
    }

    float easeInOutQuint(float x)
    {
        return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
    }
}
