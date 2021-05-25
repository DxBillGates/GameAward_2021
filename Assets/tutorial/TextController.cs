using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public string[] sentences; // 文章を格納する
    [SerializeField] Text uiText;   // uiTextへの参照

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1文字の表示にかける時間

    private int currentSentenceNum = 0; //現在表示している文章番号
    private string currentSentence = string.Empty;  // 現在の文字列
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeBeganDisplay = 1;         // 文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;       // 表示中の文字数
    private int change = 0;
    private int old = 0;
    private int count = 0;
    public GameObject arrow;
    public GameObject enemy;
    public GameObject enemyB;

    void Start()
    {
        SetNextSentence();
    }


    void Update()
    {
        old = change;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0 && change == 0)
        {
            change = 1;
        }

        if (scroll < 0 && change == 1)
        {
            change = 2;
            Instantiate(arrow);
        }

        if (Input.GetMouseButtonDown(0) && change >= 2)
        {
            count++;
        }

        if(count>=2)
        {
            if(change<=2)
            {
                GameObject enes = Instantiate(enemy) as GameObject;
                enes.transform.position = new Vector3(-58, 30, 0);

                GameObject enesB = Instantiate(enemyB) as GameObject;
                enesB.transform.position = new Vector3(-65, 16, 0);

                GameObject fin = GameObject.FindGameObjectWithTag("checker");
                Destroy(fin);
            }
            change = 3;
        }

        // 文章の表示完了 / 未完了
        if (IsDisplayComplete())
        {
            //最後の文章ではない & changeが変わったら
            if (currentSentenceNum < sentences.Length && change > old)
            {
                SetNextSentence();
            }
        }
        else
        {
            //changeが変わったら
            if (change > old)
            {
                timeUntilDisplay = 0; //※1
                SetNextSentence();
            }
        }

        //表示される文字数を計算
        int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        //表示される文字数が表示している文字数と違う
        if (displayCharCount != lastUpdateCharCount)
        {
            uiText.text = currentSentence.Substring(0, displayCharCount);
            //表示している文字数の更新
            lastUpdateCharCount = displayCharCount;
        }
    }

    // 次の文章をセットする
    void SetNextSentence()
    {
        currentSentence = sentences[currentSentenceNum];
        timeUntilDisplay = currentSentence.Length * intervalForCharDisplay;
        timeBeganDisplay = Time.time;
        currentSentenceNum++;
        lastUpdateCharCount = 0;
    }

    bool IsDisplayComplete()
    {
        return Time.time > timeBeganDisplay + timeUntilDisplay; //※2
    }
}

