using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBehaviour : MonoBehaviour
{
    public float amountEnergy;
    public float outputAmount;
    public float maxOutputAmount;
    public float increaseEnergyValue;
    public RectTransform batteryUI;
    public RectTransform outputUI;
    Vector3 initialbatteryUISize;
    Vector3 initialOutputUISize;
    public float initialAmountEnergy;
    float oldOutputAmount;
    LineCreaterBehaviour lineCreater;
    public float recoveryTime;
    public float recoveryValue;
    float recT;

    public Text amountEnergyText;
    public Text outputAmountText;

    GameObject[] objs;


    [SerializeField] float maxDamageLevel;
    public float testOutputValue;
    float testDamageValue;

    // Start is called before the first frame update
    void Start()
    {
        recT = 0;
        oldOutputAmount = 0;
        initialbatteryUISize = batteryUI.localScale;
        initialOutputUISize = outputUI.localScale;
        initialAmountEnergy = amountEnergy;
        lineCreater = GameObject.Find("First").GetComponent<LineCreaterBehaviour>();
        
    }

    // Update is called once per frame
    void Update()
    {

        amountEnergyText.text = amountEnergy.ToString();
        outputAmountText.text = outputAmount.ToString();

        batteryUI.localScale = new Vector3(initialbatteryUISize.x, initialbatteryUISize.y * amountEnergy / initialAmountEnergy, initialbatteryUISize.z);
        outputUI.localScale = new Vector3(initialOutputUISize.x,-outputAmount / initialAmountEnergy, initialOutputUISize.z);
        oldOutputAmount = outputAmount;

        //testDamageValue = 27.0f / lineCreater.lineLength;
        testOutputValue = lineCreater.lineLength / 2.7f;
        testOutputValue = Mathf.Round(testOutputValue);
        SetDamageValue();
        Debug.Log("DamageValue:" + testDamageValue);
        Debug.Log("OutputValue:" + testOutputValue);

        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheelInput > 0)
        {
            IncreaseOutputEnergy();
            if(oldOutputAmount == 0)
            {
                //lineCreater.CreateLine();
                lineCreater.isCreate = true;
                lineCreater.lineLength = 0;
            }
        }
        else if(mouseWheelInput < 0)
        {
            DecreaseOutputEnergy();
            if (outputAmount == 0)
            {
                GameObject[] lines = GameObject.FindGameObjectsWithTag("lines");
                if (lines.Length > 0)
                {
                    foreach (GameObject line in lines)
                    {
                        Destroy(line);
                    }
                }
            }
        }

        if (amountEnergy == 0)
        {
            GameObject[] lines = GameObject.FindGameObjectsWithTag("lines");
            if (lines.Length > 0)
            {
                foreach (GameObject line in lines)
                {
                    Destroy(line);
                }
            }
        }

        recT += Time.deltaTime;
        if(recT >= recoveryTime)
        {
            recT = 0;
            if(outputAmount > 0 && amountEnergy == 0)
            {
                lineCreater.isCreate = true;
            }
            amountEnergy += recoveryValue;
        }

        if(amountEnergy >= initialAmountEnergy)
        {
            amountEnergy = initialAmountEnergy;
        }
    }

    public bool OutputEnergy()
    {
        amountEnergy -= outputAmount;
        if (amountEnergy <= 0)
        {
            amountEnergy = 0;
            return false;
        }
        if(outputAmount <= 0)
        {
            return false;
        }
        return true;
    }

    public void IncreaseOutputEnergy()
    {
        outputAmount += increaseEnergyValue;
        if(outputAmount >= maxOutputAmount)
        {

           objs = GameObject.FindGameObjectsWithTag("lines");

            foreach (GameObject obj in objs)
            {

                obj.GetComponent<ParticleSystem>().Play();
            }

            //obj = GameObject.FindGameObjectWithTag("lines");
            //obj.GetComponent<ParticleL>().ParticleBegin();
            //obj.GetComponent<ParticleSystem>().Play();
            outputAmount = maxOutputAmount;
            
        }
    }

    public void DecreaseOutputEnergy()
    {
        outputAmount -= increaseEnergyValue;
        if(outputAmount <= 0)
        {
            outputAmount = 0;
        }
    }

    void SetDamageValue()
    {
        if(testOutputValue == 0)
        {
            testDamageValue = 0;
            return;
        }
        testDamageValue = maxDamageLevel + 1 - Mathf.Round(testOutputValue);

    }
}
