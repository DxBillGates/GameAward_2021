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
    AudioManagerBehaviour audioManager;
    bool isBreak;
    float time;
    int i;
    List<GameObject> gameObjects;
    public RectTransform gameClearUI;
    Vector3 initialRotate;
    Vector3 initialScale;
    float lerpTime;
    void Start()
    {
        FadeManager2.FadeIn();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerBehaviour>();
        isBreak = false;
        time = 0;
        i = 0;
        gameObjects = new List<GameObject>();

        initialRotate = gameClearUI.localEulerAngles;
        initialScale = gameClearUI.localScale;
        audioManager.clearSE.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBreak && time >= 0.2f)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("enemy");
            foreach (var g in objects)
            {
                gameObjects.Add(g);
            }
            isBreak = true;
            time = 0;
        }

        if (isBreak && time >= 0.2f && i < gameObjects.Count)
        {
            EnemyCheck e = gameObjects[i].GetComponent<EnemyCheck>();
            if (e) e.BreakPolygon(true);
            audioManager.glassSE.Play();
            ++i;
            time = 0;
        }

        if (lerpTime >= 1) lerpTime = 1;

        gameClearUI.transform.localEulerAngles = Vector3.Lerp(initialRotate, new Vector3(0, 360, 0), easeInOutQuint(lerpTime));
        gameClearUI.transform.localScale = Vector3.Lerp(initialScale, new Vector3(2, 2, 2), easeInOutQuint(lerpTime));

        lerpTime += Time.deltaTime * 0.5f;
        time += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            FadeManager2.FadeOut("StageSelect");
        }
    }

    float easeInOutQuint(float x)
    {
        return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
    }
}
