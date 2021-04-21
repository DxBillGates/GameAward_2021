using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystemBehaviour : MonoBehaviour
{
    public int maxDeadCount;
    int deadCount;

    public float setPreFeverTime;
    public float preFeverT { get; set; }
    public bool isPreFever { get; set; }

    public float setFeverTime;
    public float feverT { get; set; }
    public bool isFever { get; set; }

    public int increaseDamage;

    public bool specialMode;

    public TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        deadCount = 0;
        preFeverT = setPreFeverTime;
        isPreFever = false;
        feverT = setFeverTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPreFever)
        {
            preFeverT -= Time.deltaTime;
            if (preFeverT <= 0)
            {
                deadCount = 0;
                preFeverT = setPreFeverTime;
                isPreFever = false;
            }

            if (deadCount >= maxDeadCount)
            {
                deadCount = 0;
                if (!isFever)
                {
                    isFever = true;
                }
                else
                {
                    if (specialMode)
                        feverT = setFeverTime;
                }
            }
        }

        if (isFever)
        {
            feverT -= Time.deltaTime;
            if (feverT <= 0)
            {
                preFeverT = setPreFeverTime;
                isPreFever = false;

                feverT = setFeverTime;
                isFever = false;

                deadCount = 0;
            }
        }

        text.text = "PreFever : " + preFeverT.ToString() + "\n" + "Fever : " + feverT.ToString();
    }

    public void IncreaseDeadCount()
    {
        ++deadCount;
        if (!isPreFever)
        {
            isPreFever = true;
        }

        if (specialMode)
        {
            isPreFever = true;
            if (preFeverT != setPreFeverTime)
            {
                preFeverT = setPreFeverTime;
            }
        }
    }
}
