using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public string[] sentences; // ���͂��i�[����
    [SerializeField] Text uiText;   // uiText�ւ̎Q��

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;   // 1�����̕\���ɂ����鎞��

    private int currentSentenceNum = 0; //���ݕ\�����Ă��镶�͔ԍ�
    private string currentSentence = string.Empty;  // ���݂̕�����
    private float timeUntilDisplay = 0;     // �\���ɂ����鎞��
    private float timeBeganDisplay = 1;         // ������̕\�����J�n��������
    private int lastUpdateCharCount = -1;       // �\�����̕�����
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

        // ���͂̕\������ / ������
        if (IsDisplayComplete())
        {
            //�Ō�̕��͂ł͂Ȃ� & change���ς������
            if (currentSentenceNum < sentences.Length && change > old)
            {
                SetNextSentence();
            }
        }
        else
        {
            //change���ς������
            if (change > old)
            {
                timeUntilDisplay = 0; //��1
                SetNextSentence();
            }
        }

        //�\������镶�������v�Z
        int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        //�\������镶�������\�����Ă��镶�����ƈႤ
        if (displayCharCount != lastUpdateCharCount)
        {
            uiText.text = currentSentence.Substring(0, displayCharCount);
            //�\�����Ă��镶�����̍X�V
            lastUpdateCharCount = displayCharCount;
        }
    }

    // ���̕��͂��Z�b�g����
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
        return Time.time > timeBeganDisplay + timeUntilDisplay; //��2
    }
}

