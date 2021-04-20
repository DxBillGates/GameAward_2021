using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBehaviour : MonoBehaviour
{
    public float hp;
    float maxHp;
    new Renderer renderer;
    Color color;
    float angle;
    bool isDamage, oldIsDamage;
    float noDamageTime;
    public float regenerationSpan;
    // Start is called before the first frame update
    void Start()
    {
        isDamage = oldIsDamage = false;
        noDamageTime = 0;
        angle = 0;
        maxHp = hp;
        renderer = GetComponent<Renderer>();
        color = new Color(1,1,1,1);

        if(regenerationSpan == 0)
        {
            regenerationSpan = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isDamage = false;
        color.a = hp / maxHp;
        renderer.material.color = color;
        transform.rotation = Quaternion.Euler(angle, 0, angle);

        angle += 0.016f;


        if(!oldIsDamage)
        {
            noDamageTime += Time.deltaTime;
        }
        if(noDamageTime >= regenerationSpan)
        {
            if(hp < maxHp)
            {
                hp++;
                noDamageTime = 0;
            }
        }
        oldIsDamage = isDamage;
    }

    void Damage()
    {
        noDamageTime = 0;
        --hp;
        isDamage = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.collider.gameObject);
        Damage();
    }
}
