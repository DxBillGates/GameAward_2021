using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearBehaviour : MonoBehaviour
{
    //[SerializeField] GameObject dog;
    //[SerializeField] GameObject fracture;
    //[SerializeField] GameObject deer;
    //[SerializeField] GameObject pig;
    //[SerializeField] GameObject gorilla;
    // Start is called before the first frame update
    void Start()
    {
        FadeManager2.FadeIn();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("enemy");
        foreach (var obj in gameObjects)
        {
            EnemyCheck enemyCheck = obj.GetComponent<EnemyCheck>();
            enemyCheck.BreakPolygon(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeManager2.FadeOut("StageSelect");
        }
    }
}
