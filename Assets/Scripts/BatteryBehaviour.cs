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
    // Start is called before the first frame update
    void Start()
    {
        initialUISize = batteryUI.localScale;
        initialAmountEnergy = amountEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        batteryUI.localScale = new Vector3(initialUISize.x, initialUISize.y * amountEnergy / initialAmountEnergy, initialUISize.z);
        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheelInput > 0)
        {
            IncreaseOutputEnergy();
        }
        else if(mouseWheelInput < 0)
        {
            DecreaseOutputEnergy();
        }
    }

    public bool OutputEnergy()
    {
        amountEnergy -= outputAmount;
        if (amountEnergy < 0)
        {
            amountEnergy = 0;
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
            outputAmount = 1;
        }
    }
}
