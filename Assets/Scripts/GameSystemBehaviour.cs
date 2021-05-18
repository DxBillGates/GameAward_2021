using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystemBehaviour : MonoBehaviour
{
    GameObject selectedObject;
    GameObject selectObject;
    Renderer soRenderer;
    ChangePartsSystemBehavior systemBehavior;
    PauseParentBehaviour pauseParent;
    PauseUIManager pauseUIManager;
    Color initialColor;
    //TextMesh modeText;
    // Start is called before the first frame update
    private void Awake()
    {
        pauseParent = gameObject.AddComponent<PauseParentBehaviour>();
        pauseUIManager = gameObject.AddComponent<PauseUIManager>();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        initialColor = GameObject.Find("mobius_special").GetComponent<Renderer>().material.color;
        systemBehavior = GetComponent<ChangePartsSystemBehavior>();
        selectedObject = null;
        selectObject = null;
        soRenderer = null;
        //modeText = GameObject.Find("ChangeModeText").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

        //FadeManager2.FadeIn();

        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (systemBehavior.isMouse && !PauseUIManager.isPause)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    selectObject = hit.collider.gameObject;
                    soRenderer = selectObject.GetComponent<Renderer>();
                    //Color color = Color.red;
                    //color.a = 0.25f;
                    //soRenderer.material.color = color;

                    if (selectObject != systemBehavior.firstObj)
                    {
                        FlashingBehaviour fb = selectObject.GetComponent<FlashingBehaviour>();
                        fb.enabled = true;
                        fb.isFlashing = true;
                    }
                }
                else
                {
                    selectObject = null;
                }

                if (selectedObject && selectedObject != selectObject)
                {
                    if (selectedObject != systemBehavior.firstObj && selectedObject != systemBehavior.secondObj)
                    {
                        soRenderer = selectedObject.GetComponent<Renderer>();
                        Color color = Color.white;
                        color.a = 0.25f;
                        soRenderer.material.color = initialColor;
                        FlashingBehaviour fb = selectedObject.GetComponent<FlashingBehaviour>();
                        fb.isFlashing = false;
                        Debug.Log("離れたよ！");
                    }
                }
            }

            selectedObject = selectObject;
            //modeText.text = "マウス入れ替えモード\n1キーで切り替え";

            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    systemBehavior.isMouse = false;
            //}
        }
        else
        {
            //modeText.text = "分身入れ替えモード\n1キーで切り替え";
            //if (soRenderer)
            //{
            //    soRenderer = selectedObject.GetComponent<Renderer>();
            //    Color color = Color.white;
            //    color.a = 0.25f;
            //    soRenderer.material.color = color;
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    systemBehavior.isMouse = true;
            //}
        }
    }
}
