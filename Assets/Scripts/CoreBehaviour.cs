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
    // Start is called before the first frame update
    void Start()
    {
        angle = 0;
        maxHp = hp;
        renderer = GetComponent<Renderer>();
        color = new Color(1,1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        color.a = hp / maxHp;
        renderer.material.color = color;
        transform.rotation = Quaternion.Euler(angle, 0, angle);

        angle += 0.016f;
    }

    void Damage()
    {
        --hp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.collider.gameObject);
        Damage();
    }
}
