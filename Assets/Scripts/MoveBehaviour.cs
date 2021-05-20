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
    public float green;
    private static CoreBehaviour coreBehaviour;
    PauseBehaviour pauseBehaviour;
    static Boss2Behaviour boss2Behaviour;

    GameObject pig;
    Vector3 initialSize;

    float dist = 5;
    float factor = 5;
    bool check = false;

    void Start()
    {
        initialSize = transform.localScale;
        pauseBehaviour = gameObject.AddComponent<PauseBehaviour>();
        systemBehavior = GameObject.Find("GameSystem").GetComponent<ChangePartsSystemBehavior>();
        lerpBehaviour = GetComponent<NewLerpBehaviour>();
        objectBehavior = GetComponent<ObjectBehavior>();
        //feverSystem = GameObject.Find("GameSystem").GetComponent<FeverSystemBehaviour>();
        bossCreater = GameObject.Find("BossCreater").GetComponent<BossCreaterBehaviour>();
        meshRenderer = GetComponent<MeshRenderer>();
        coreBehaviour = GameObject.Find("Core").GetComponent<CoreBehaviour>();

        pig = GameObject.Find("erecPig");
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
                //transform.position += transform.right * Time.deltaTime * 10;
            }
            else
            {
                transform.position -= transform.right * Time.deltaTime * (objectBehavior.speed + objectBehavior.addSpeed);
                //transform.position -= transform.right * Time.deltaTime * 10;
            }

            if (Vector3.Distance(transform.position, end.transform.position) <= 2)
            {
                if (batteryBehaviour.OutputEnergy())
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
        green = 1.0f / batteryBehaviour.testOutputValue;
        //green = 1 - 1.0f / (batteryBehaviour.outputAmount + addDamageValue);
        meshRenderer.material.color = new Color(1,green, 0, 1);
        transform.localScale = initialSize * (green + 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Boss"))
        {
            //EnemyBehaviour enemyBehaviour = other.gameObject.GetComponent<EnemyBehaviour>();
            ////int value = (feverSystem.isFever) ? feverSystem.increaseDamage+ (int)batteryBehaviour.outputAmount : (int)batteryBehaviour.outputAmount;
            //int value;
            //value = (int)batteryBehaviour.outputAmount;
            //enemyBehaviour.Damage(value);
            //if (enemyBehaviour)
            //    if (enemyBehaviour.hp <= 0)
            //    {
            //        if (other.gameObject.name != "Boss")
            //        {
            //            Destroy(other.gameObject);
            //            //feverSystem.IncreaseDeadCount();
            //        }
            //        else
            //            other.gameObject.SetActive(false);
            //        ++bossCreater.nowSacrificeCount;
            //    }
            HitEnemy(other.gameObject);
        }
        if (other.gameObject.CompareTag("amplifier"))
        {
            addDamageValue += other.gameObject.GetComponent<AmplifierBehaviour>().addValue;
        }
    }

    private void HitEnemy(GameObject other)
    {
        EnemyBehaviour enemyBehaviour = other.GetComponent<EnemyBehaviour>();
        if (!enemyBehaviour.enabled)
        {
            return;
        }
        //int value = (feverSystem.isFever) ? feverSystem.increaseDamage+ (int)batteryBehaviour.outputAmount : (int)batteryBehaviour.outputAmount;
        int value;
        value = (int)(batteryBehaviour.testDamageValue + addDamageValue);
        if (enemyBehaviour.takeDamageValue == 0)
        {
            if (value >= enemyBehaviour.startTakeDamageValue)
            {
                enemyBehaviour.Damage(value);
            }
        }
        else
        {
            if (value <= enemyBehaviour.startTakeDamageValue)
            {
                //Debug.Log(value + ":" + enemyBehaviour.startTakeDamageValue);
                enemyBehaviour.Damage((int)enemyBehaviour.takeDamageValue);
            }
        }

        if (other.name == "boss2")
        {
            Debug.Log("!");
        }
        if (enemyBehaviour)
            if (enemyBehaviour.hp <= 0)
            {

                if (other.layer == 10)
                {
                    batteryBehaviour.amountEnergy += other.GetComponent<BatteryEnemyBehaviour>().value;
                    if (batteryBehaviour.amountEnergy + other.GetComponent<BatteryEnemyBehaviour>().value >= batteryBehaviour.initialAmountEnergy)
                    {
                        batteryBehaviour.amountEnergy = batteryBehaviour.initialAmountEnergy;
                    }
                }
                if (other.name != "Boss")
                {

                    if (other.name == "BossChildEnemy")
                    {
                        Debug.Log(boss2Behaviour);
                        boss2Behaviour.deadChildAmount++;
                    }

                    check = true;

                    var enemyCheck = other.gameObject.GetComponent<EnemyCheck>();
               
                    if (enemyCheck)
                    {
                        other.gameObject.GetComponent<BatteryEnemyBehaviour>().GenerateBeam();
                        enemyCheck.BreakPolygon(check);
                        other.gameObject.GetComponent<PauseBehaviour>().OnPauseOtherComponent(enemyCheck);

                    }
                    //other.gameObject.GetComponent<EnemyCheck>().BreakPolygon(check);   

                    //other.gameObject.GetComponent<BatteryEnemyBehaviour>().GenerateBeam(check);   

                    ++coreBehaviour.killEnemyCountCurrentFrame;
                    //feverSystem.IncreaseDeadCount();
                }
                else
                {
                    if (other.GetComponent<Boss2Behaviour>())
                    {
                        //other.gameObject.GetComponent<ParticleSystem>().Play();
                        other.gameObject.GetComponent<BatteryEnemyBehaviour>().GenerateBeam();
                        boss2Behaviour = other.GetComponent<Boss2Behaviour>();
                        boss2Behaviour.SetDeadPosition();
                        boss2Behaviour.deadFlag = true;
                    }
                    else
                    {
                        other.gameObject.GetComponent<BatteryEnemyBehaviour>().GenerateBeam();
                        //other.gameObject.GetComponent<ParticleSystem>().Play();
                        other.SetActive(false);
                    }
                }
                ++bossCreater.nowSacrificeCount;
            }
    }
}
