using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSystem : MonoBehaviour
{
    public List<GameObject> stages;

    Vector3 oldTutorialPos;
    Vector3 oldStage1Pos;
    Vector3 oldStage2Pos;
    Vector3 oldStage3Pos;
    Vector3 oldStage4Pos;
    Vector3 oldStage5Pos;

    GameObject forwardScene;
    GameObject selectScene;
    public bool isRotateScene;

    Vector3 initialScale;
    Vector3 setScale;
    public float setScaleValue;
    float scaleLerpTime;
    public float maxLerpTime;

    public int rotateIndex;
    float rotateValue;
    public float rotateLerpTime;
    public float maxRotateTime;

    bool isGoTitle;
    FlashingBehaviour selectUI;
    // Start is called before the first frame update
    void Start()
    {
        oldTutorialPos = stages[0].transform.position;
        oldStage1Pos = stages[1].transform.position;
        oldStage2Pos = stages[2].transform.position;
        oldStage3Pos = stages[3].transform.position;
        oldStage4Pos = stages[4].transform.position;
        oldStage5Pos = stages[5].transform.position;

        isRotateScene = false;
        initialScale = stages[0].transform.localScale;
        setScale = initialScale * setScaleValue;
        isGoTitle = false;

        FadeManager2.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (!isRotateScene)
        {
            if (mouseWheelInput > 0)
            {
                rotateValue = 1;
                ++rotateIndex;
                if (rotateIndex <= 0)
                {
                    isRotateScene = true;
                    rotateLerpTime = 0;
                }
                else
                {
                    rotateIndex = 0;
                }
            }
            else if (mouseWheelInput < 0)
            {
                rotateValue = -1;
                --rotateIndex;
                if (!(Mathf.Abs(rotateIndex) >= stages.Count))
                {
                    if (rotateIndex < 0)
                    {
                        isRotateScene = true;
                        rotateLerpTime = 0;
                    }
                }
                else
                {
                    ++rotateIndex;
                }
            }
        }

        isGoTitle = CheckGoTitle();
        if (isGoTitle) FadeManager2.FadeOut("TitleScene");

        if (!isRotateScene)
        {
            CheckForwardScene();

            Vector3 scale = Vector3.Lerp(initialScale, setScale, easeInOutQuint(scaleLerpTime));
            forwardScene.transform.localScale = scale;

            scaleLerpTime += Time.deltaTime / maxLerpTime;
            if (scaleLerpTime >= 1)
            {
                scaleLerpTime = 1;
            }

            CheckSelectScene();
        }
        else
        {
            RotateScenes();
        }
    }
    bool CheckGoTitle()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "GoTitle")
            {
                selectUI = hit.collider.gameObject.GetComponent<FlashingBehaviour>();
                selectUI.isFlashing = true;
                if (Input.GetMouseButtonDown(0))
                {
                    return true;
                }
            }
            else
            {
                if (selectUI)
                {
                    selectUI.isFlashing = false;
                    selectUI = null;
                }
            }
        }
        else
        {
            if (selectUI)
            {
                selectUI.isFlashing = false;
                selectUI = null;
            }
        }
        return false;
    }

    bool CheckSelectScene()
    {
        if (Input.GetMouseButtonDown(0) && !isGoTitle)
        {
            if(forwardScene.name == "Tutorial")
            {
                FadeManager2.FadeOut("TT");
            }
            else if (forwardScene.name == "Stage1")
            {
                FadeManager2.FadeOut("Stage1");
            }
            else if (forwardScene.name == "Stage2")
            {
                FadeManager2.FadeOut("Stage2");
            }
            else if (forwardScene.name == "Stage3")
            {
                FadeManager2.FadeOut("Stage3");
            }
            else if (forwardScene.name == "Stage4")
            {
                FadeManager2.FadeOut("Stage4");
            }
            else if (forwardScene.name == "Stage5")
            {
                FadeManager2.FadeOut("Stage5");
            }
            PauseUIManager.OnResume();
        }
        return false;
    }

    void CheckForwardScene()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10))
        {
            forwardScene = hit.collider.gameObject;
        }
    }

    void RotateScenes()
    {
        rotateLerpTime += Time.deltaTime / maxRotateTime;

        if (rotateLerpTime > 1)
        {
            rotateLerpTime = 1;
            isRotateScene = false;
        }
        if (rotateValue < 0)
        {
            stages[0].transform.position = Vector3.Lerp(oldTutorialPos, oldTutorialPos + new Vector3(0, 4, 0), easeInOutQuint(rotateLerpTime));
            stages[1].transform.position = Vector3.Lerp(oldStage1Pos, oldTutorialPos, easeInOutQuint(rotateLerpTime));
            stages[2].transform.position = Vector3.Lerp(oldStage2Pos, oldStage1Pos, easeInOutQuint(rotateLerpTime));
            stages[3].transform.position = Vector3.Lerp(oldStage3Pos, oldStage2Pos, easeInOutQuint(rotateLerpTime));
            stages[4].transform.position = Vector3.Lerp(oldStage4Pos, oldStage3Pos, easeInOutQuint(rotateLerpTime));
            stages[5].transform.position = Vector3.Lerp(oldStage5Pos, oldStage4Pos, easeInOutQuint(rotateLerpTime));
        }
        else
        {
            stages[0].transform.position = Vector3.Lerp(oldTutorialPos, oldStage1Pos, easeInOutQuint(rotateLerpTime));
            stages[1].transform.position = Vector3.Lerp(oldStage1Pos, oldStage2Pos, easeInOutQuint(rotateLerpTime));
            stages[2].transform.position = Vector3.Lerp(oldStage2Pos, oldStage3Pos, easeInOutQuint(rotateLerpTime));
            stages[3].transform.position = Vector3.Lerp(oldStage3Pos, oldStage4Pos, easeInOutQuint(rotateLerpTime));
            stages[4].transform.position = Vector3.Lerp(oldStage4Pos, oldStage5Pos, easeInOutQuint(rotateLerpTime));
            stages[5].transform.position = Vector3.Lerp(oldStage5Pos, oldStage5Pos - new Vector3(0, 4, 0), easeInOutQuint(rotateLerpTime));
        }

        if (!isRotateScene)
        {
            oldTutorialPos = stages[0].transform.position;
            oldStage1Pos = stages[1].transform.position;
            oldStage2Pos = stages[2].transform.position;
            oldStage3Pos = stages[3].transform.position;
            oldStage4Pos = stages[4].transform.position;
            oldStage5Pos = stages[5].transform.position;
            scaleLerpTime = 0;
        }

        foreach (var stage in stages)
        {
            stage.transform.localScale = initialScale;
        }
    }

    float easeInOutQuint(float x)
    {
        return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
    }
}
