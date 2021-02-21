using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesScript : MonoBehaviour

{

    public enum Consumables : int
    {
        None = 1,
        SecondHealth,
        FastStart
    }

    public Consumables TypeConsumables = Consumables.None;
    public int quantity;


    private void Awake()
    {
        switch (TypeConsumables)
        {
            case Consumables.SecondHealth:
                quantity = PlayerPrefs.GetInt("QuantitySecondHealth", 0);
                break;
            case Consumables.FastStart:
                quantity = PlayerPrefs.GetInt("QuantityFastStart", 0);
                break;
        }
    }

    public void PlusConsumables(int value)
    {
        quantity += value;
        switch (TypeConsumables)
        {
            case Consumables.SecondHealth:
                PlayerPrefs.SetInt("QuantitySecondHealth", quantity);
                break;
            case Consumables.FastStart:
                PlayerPrefs.SetInt("QuantityFastStart", quantity);
                break;
        }
    }
    public bool MinusConsumables(int value)
    {
        if(quantity - value >= 0)
        {
            quantity -= value;
            switch (TypeConsumables)
            {
                case Consumables.SecondHealth:
                    PlayerPrefs.SetInt("QuantitySecondHealth", quantity);
                    break;
                case Consumables.FastStart:
                    PlayerPrefs.SetInt("QuantityFastStart", quantity);
                    break;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public int ReturnQuantity()
    {
        return quantity;
    }
}
