using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneSystemBehaviour : MonoBehaviour
{
    GameObject selectObject;
    FlashingBehaviour selectUI;
    // Start is called before the first frame update
    void Start()
    {
        selectObject = null;
        selectUI = null;
        Application.targetFrameRate = 60;
        FadeManager2.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Start")
            {
                if (selectUI && selectUI.gameObject.name != "Start") selectUI.isFlashing = false;
                selectUI = hit.collider.gameObject.GetComponent<FlashingBehaviour>();
                selectUI.isFlashing = true;
                if (Input.GetMouseButtonDown(0))
                {
                    FadeManager2.FadeOut("StageSelect");
                }
            }
            else if (hit.collider.gameObject.name == "Exit")
            {
                if (selectUI && selectUI.gameObject.name != "Exit") selectUI.isFlashing = false;
                selectUI = hit.collider.gameObject.GetComponent<FlashingBehaviour>();
                selectUI.isFlashing = true;
                if (Input.GetMouseButtonDown(0))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;   // UnityEditorÇÃé¿çsÇí‚é~Ç∑ÇÈèàóù
#else
        Application.Quit();                                // ÉQÅ[ÉÄÇèIóπÇ∑ÇÈèàóù
#endif
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
    }
}
