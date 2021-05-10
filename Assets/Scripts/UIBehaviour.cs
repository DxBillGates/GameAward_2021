using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_Continue()
    {
        PauseParentBehaviour.Resume();
    }

    public void OnClick_Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClick_Title()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
