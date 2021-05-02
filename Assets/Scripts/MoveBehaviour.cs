using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    ChangePartsSystemBehavior systemBehavior;
    NewLerpBehaviour lerpBehaviour;
    ObjectBehavior objectBehavior;
    public GameObject start, end;
    public BatteryBehaviour batteryBehaviour { get; set; }
    MeshRenderer meshRenderer;
    BossCreaterBehaviour bossCreater;
    //FeverSystemBehaviour feverSystem;
    // Start is called before the first frame update
    public float addDamageValue;
    void Start()
    {
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        lerpBehaviour = GetComponent<NewLerpBehaviour>();
        objectBehavior = GetComponent<ObjectBehavior>();
        //feverSystem = GameObject.Find("GameSystem").GetComponent<FeverSystemBehaviour>();
        bossCreater = GameObject.Find("BossCreater").GetComponent<BossCreaterBehaviour>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!systemBehavior.changeMode)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, objectBehavior.onNormal) * transform.rotation;
            if (lerpBehaviour.leftMove)
            {
                transform.position += transform.right * Time.deltaTime * (objectBehavior.speed + objectBehavior.addSpeed);
            }
            else
            {
                transform.position -= transform.right * Time.deltaTime * (objectBehavior.speed + objectBehavior.addSpeed);
            }

            if (Vector3.Distance(transform.position, end.transform.position) <= 1)
            {
                if(batteryBehaviour.OutputEnergy())
                {
                    transform.position = start.transform.position;
                    transform.rotation = start.transform.rotation;
                    addDamageValue = 0;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        meshRenderer.material.color = new Color(1,1-1.0f/batteryBehaviour.outputAmount + addDamageValue,0,1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            EnemyBehaviour enemyBehaviour = other.gameObject.GetComponent<EnemyBehaviour>();
            //int value = (feverSystem.isFever) ? feverSystem.increaseDamage+ (int)batteryBehaviour.outputAmount : (int)batteryBehaviour.outputAmount;
            int value;
            value = (int)batteryBehaviour.outputAmount;
            enemyBehaviour.Damage(value);
            if (enemyBehaviour)
                if (enemyBehaviour.hp <= 0)
                {
                    if (other.gameObject.name != "Boss")
                    {
                        Destroy(other.gameObject);
                        //feverSystem.IncreaseDeadCount();
                    }
                    else
                        other.gameObject.SetActive(false);
                    ++bossCreater.nowSacrificeCount;
                }

        }
        if (other.gameObject.CompareTag("amplifier"))
        {
            addDamageValue = other.gameObject.GetComponent<AmplifierBehaviour>().addValue;
        }
    }
}
