using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBehaviour : MonoBehaviour
{
    public float amountEnergy;
    public float outputAmount;
    public float increaseEnergyValue;
    public RectTransform batteryUI;
    Vector3 initialUISize;
    float initialAmountEnergy;
    float oldOutputAmount;
    LineCreaterBehaviour lineCreater;
    // Start is called before the first frame update
    void Start()
    {
        oldOutputAmount = 0;
        initialUISize = batteryUI.localScale;
        initialAmountEnergy = amountEnergy;
        lineCreater = GameObject.Find("First").GetComponent<LineCreaterBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        batteryUI.localScale = new Vector3(initialUISize.x, initialUISize.y * amountEnergy / initialAmountEnergy, initialUISize.z);
        oldOutputAmount = outputAmount;
        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheelInput > 0)
        {
            IncreaseOutputEnergy();
            if(oldOutputAmount == 0)
            {
                //lineCreater.CreateLine();
                lineCreater.isCreate = true;
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
    }

    public void DecreaseOutputEnergy()
    {
        outputAmount -= increaseEnergyValue;
        if(outputAmount <= 0)
        {
            outputAmount = 0;
        }
    }
}
