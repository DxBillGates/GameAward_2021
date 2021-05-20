using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePartsSystemBehavior : MonoBehaviour
{
    public bool isSelectFirstObj, changePreMode, changeMode;
    public GameObject firstObj, secondObj;
    Vector3 fPos, sPos;
    Vector3 fAngle, sAngle;
    float t;
    bool isMouseChange;
    public bool isMouse;
    public GameObject setObject { get; set; }
    GameObject core;
    CoreBehaviour coreBehaviour;
    public bool clearFlag;
    BossCreaterBehaviour bossCreaterBehaviour;
    public GameObject boss;
    public EnemyBehaviour bossBehavior;
    public bool oldChangeFlag;
    BatteryBehaviour batteryBehaviour;
    PauseBehaviour pauseBehaviour;
    Color initialColor;
    Boss2Behaviour boss2;

    void Start()
    {
        initialColor = GameObject.Find("mobius_special").GetComponent<Renderer>().material.color;
        pauseBehaviour = gameObject.AddComponent<PauseBehaviour>();
        clearFlag = false;
        core = GameObject.Find("Core");
        coreBehaviour = core.GetComponent<CoreBehaviour>();
        Initialize();
        bossCreaterBehaviour = GameObject.Find("BossCreater").GetComponent<BossCreaterBehaviour>();
        bossBehavior = null;
        boss = null;
        batteryBehaviour = GameObject.Find("First").GetComponent<BatteryBehaviour>();

    }

    void Update()
    {
        oldChangeFlag = changeMode;
        //if (bossCreaterBehaviour)
        //{
        //    if (bossCreaterBehaviour.isCreate)
        //    {
        //        if (!bossBehavior)
        //        {
        //            boss = GameObject.Find("Boss");
        //            bossBehavior = boss.GetComponent<EnemyBehaviour>();
        //        }
        //    }
        //}

        //if (bossBehavior)
        //{
        //    if (bossBehavior.hp <= 0)
        //    {
        //        if (!boss.GetComponent<Boss2Behaviour>())
        //        {
        //            clearFlag = true;
        //        }
        //        else
        //        {
        //            boss2 = boss.GetComponent<Boss2Behaviour>();
        //            if (boss2.deadChildAmount == boss2.createEnemyAmount)
        //            {
        //                clearFlag = true;
        //            }
        //        }
        //    }
        //}

        if(bossCreaterBehaviour.isCreate)
        {
            GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
            if(bosses.Length <= 0)
            {
                clearFlag = true;
            }
        }

        if (coreBehaviour.hp <= 0/* || batteryBehaviour.amountEnergy <= 0*/)
        {
            FadeManager.Instance.LoadScene("GameOverScene", 0.8f);
            // SceneManager.LoadScene("GameOverScene");
            //StartCoroutine(Change(2, "GameOverScene"));

        }
        if (clearFlag)
        {
            FadeManager.Instance.LoadScene("GameClearScene", 0.8f);
            //SceneManager.LoadScene("GameClearScene");
            //StartCoroutine(Change(2, "GameClearScene"));
        }

        if (isMouse && !PauseUIManager.isPause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool notChange = false;
                if (boss2)
                {
                    notChange = boss2.isDivide;
                }
                if (!changeMode && !notChange)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Color color = Color.red;
                    color.a = 0.25f;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.layer == 7)
                        {
                            if (!isSelectFirstObj)
                            {
                                isSelectFirstObj = true;
                                firstObj = hit.collider.gameObject;
                                fPos = firstObj.transform.position;
                                fAngle = firstObj.transform.eulerAngles;

                                firstObj.GetComponent<Renderer>().material.color = color;

                                FlashingBehaviour fb = firstObj.GetComponent<FlashingBehaviour>();
                                fb.isFlashing = false;

                                PauseParentBehaviour.PauseExclutionGameObject(this);
                            }
                            else
                            {
                                secondObj = hit.collider.gameObject;

                                if (secondObj == firstObj)
                                {
                                    Initialize();
                                    PauseParentBehaviour.ResumeExclution();
                                    return;
                                }

                                sPos = secondObj.transform.position;
                                sAngle = secondObj.transform.eulerAngles;
                                changePreMode = true;
                                changeMode = true;
                                secondObj.GetComponent<Renderer>().material.color = color;
                                isMouseChange = true;

                                FlashingBehaviour fb = secondObj.GetComponent<FlashingBehaviour>();
                                fb.isFlashing = false;

                                if (PauseParentBehaviour.isPause)
                                {
                                    BatteryBehaviour.AllDestroy();
                                }
                            }
                        }
                    }
                }
            }

            if (changeMode)
            {
                if (isMouseChange)
                {
                    Change();
                }
            }
        }
    }

    public void Change()
    {
        if (t >= 1)
        {
            t = 1;
            isSelectFirstObj = changePreMode = changeMode = isMouseChange = false;
        }

        firstObj.transform.position = Vector3.Lerp(fPos, sPos, t);
        secondObj.transform.position = Vector3.Lerp(sPos, fPos, t);

        firstObj.transform.rotation = Quaternion.Euler(Vector3.Lerp(fAngle, sAngle, t));
        secondObj.transform.rotation = Quaternion.Euler(Vector3.Lerp(sAngle, fAngle, t));

        t += Time.deltaTime;
        //入れ替え終了
        if (!changeMode)
        {
            Color color = new Color(1, 1, 1, 0.25f);
            firstObj.GetComponent<Renderer>().material.color = initialColor;
            secondObj.GetComponent<Renderer>().material.color = initialColor;
            t = 0;

            FlashingBehaviour fb = firstObj.GetComponent<FlashingBehaviour>();
            if (fb.isFlashing)
            {
                fb.isFlashing = false;
            }
            fb = secondObj.GetComponent<FlashingBehaviour>();
            if (fb.isFlashing)
            {
                fb.isFlashing = false;
            }
            firstObj = secondObj = null;
            PauseParentBehaviour.ResumeExclution();
            if (batteryBehaviour.isOutput)
            {
                batteryBehaviour.lineCreater.isCreate = true;
                batteryBehaviour.lineCreater.lineLength = 0;
            }
        }
    }

    private void Change(GameObject gameObject, GameObject child)
    {
        if (!isMouseChange)
            if (!changeMode)
            {
                RaycastHit hit;
                float maxDistance = 10;
                Color color = Color.red;
                color.a = 0.25f;
                if (Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, out hit, maxDistance))
                {
                    isSelectFirstObj = true;
                    firstObj = hit.collider.gameObject;
                    fPos = firstObj.transform.position;
                    fAngle = firstObj.transform.eulerAngles;

                    firstObj.GetComponent<Renderer>().material.color = color;
                }

                if (Physics.Raycast(child.transform.position, -child.transform.up, out hit, maxDistance))
                {
                    secondObj = hit.collider.gameObject;
                    sPos = secondObj.transform.position;
                    sAngle = secondObj.transform.eulerAngles;
                    changePreMode = true;
                    changeMode = true;
                    child.transform.parent = secondObj.transform;

                    secondObj.GetComponent<Renderer>().material.color = color;
                }
            }
            else
            {
                Change();
            }
    }

    public void Initialize()
    {
        if (setObject)
        {
            Color color = new Color(1, 1, 1, 0.25f);
            setObject.GetComponent<Renderer>().material.color = initialColor;
            setObject = null;
        }
        if (firstObj)
        {
            Color color = new Color(1, 1, 1, 0.25f);
            firstObj.GetComponent<Renderer>().material.color = initialColor;
            if (secondObj)
            {
                secondObj.GetComponent<Renderer>().material.color = initialColor;
            }
        }

        isSelectFirstObj = changePreMode = changeMode = false;
        firstObj = secondObj = null;
        fPos = sPos = new Vector3();
        fAngle = sAngle = new Vector3();
        t = 0;
        isMouseChange = false;

        firstObj = secondObj = null;
    }

    private IEnumerator Change(float waitTime, string scene)
    {
        yield return new WaitForSeconds(waitTime);
        FadeManager2.FadeOut(scene);
    }

}
