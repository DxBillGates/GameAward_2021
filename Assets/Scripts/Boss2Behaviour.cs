using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Behaviour : MonoBehaviour
{
    public bool deadFlag;
    public bool exploFlag;
    float lerpTime;
    Vector3 deadPos;
    public float maxTime;
    public int createEnemyAmount;
    public GameObject creater { get; set; }
    public GameObject enemyPrehab;
    LerpBehaviour mLerpBehaviour;
    public List<GameObject> gameObjects;
    public int deadChildAmount;
    // Start is called before the first frame update
    void Start()
    {
        deadChildAmount = 0;
        gameObjects = new List<GameObject>();
        exploFlag = false;
        deadFlag = false;
        lerpTime = 0;
        deadPos = new Vector3();
        mLerpBehaviour = GetComponent<LerpBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(deadFlag)
        {
            transform.position = Vector3.Lerp(deadPos,deadPos + transform.up * 10,lerpTime);
            if(lerpTime >= 1)
            {
                lerpTime = 1;
                exploFlag = true;
            }
            lerpTime += Time.deltaTime;
        }
        if(exploFlag)
        {
            Divide();
            exploFlag = false;
            deadFlag = false;
            gameObject.SetActive(false);
        }
    }

    public void SetDeadPosition()
    {
        deadPos = transform.position;
    }

    public void Divide()
    {
        for(int i = 0;i < createEnemyAmount;++i)
        {
            GameObject newEnemy = Instantiate(enemyPrehab);

            Vector3 random = new Vector3();
            random = transform.up * Random.Range(1,10);

            ParticleBehaviour particleBehaviour = newEnemy.AddComponent<ParticleBehaviour>();
            particleBehaviour.vector = random;
            particleBehaviour.gravity = -transform.up;

            newEnemy.transform.position = transform.position;
            newEnemy.transform.rotation = transform.rotation;
            newEnemy.GetComponent<ObjectBehavior>().fallVector = -transform.up * Time.deltaTime;
            LerpBehaviour lerpBehaviour = newEnemy.GetComponent<LerpBehaviour>();
            lerpBehaviour.lerpPoints = new List<Transform>();
            lerpBehaviour.frontList = mLerpBehaviour.frontList;
            lerpBehaviour.backList = mLerpBehaviour.backList;
            lerpBehaviour.leftMove = mLerpBehaviour.leftMove;
            lerpBehaviour.isAutoMove = true;
            newEnemy.name = "BossChildEnemy";
            gameObjects.Add(newEnemy);
        }
    }
}
