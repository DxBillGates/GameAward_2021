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
    TextMesh modeText;
    // Start is called before the first frame update
    void Start()
    {
        systemBehavior = GetComponent<ChangePartsSystemBehavior>();
        selectedObject = null;
        selectObject = null;
        soRenderer = null;
        modeText = GameObject.Find("ChangeModeText").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (systemBehavior.isMouse)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    selectObject = hit.collider.gameObject;
                    soRenderer = selectObject.GetComponent<Renderer>();
                    Color color = Color.red;
                    color.a = 0.25f;
                    soRenderer.material.color = color;
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
                        soRenderer.material.color = color;
                    }
                }
            }

            selectedObject = selectObject;
            modeText.text = "?}?E?X???????????[?h\n1?L?[??????????";

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                systemBehavior.isMouse = false;
            }
        }
        else
        {
            modeText.text = "???g???????????[?h\n1?L?[??????????";
            if (soRenderer)
            {
                soRenderer = selectedObject.GetComponent<Renderer>();
                Color color = Color.white;
                color.a = 0.25f;
                soRenderer.material.color = color;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                systemBehavior.isMouse = true;
            }
        }
    }
}
