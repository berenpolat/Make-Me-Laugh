using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text budgetText;
    [SerializeField] private float maxHappiness = 100;
    [SerializeField] private float happinessDropRate;
    private float currentPoints;
    private float currentMoney;
    private float currentHappiness;
    private static GameManager _instance;
    public static GameManager Instance
    
    {
        get
        {
            if(!_instance)
                Debug.LogError("GameManager is null!");
            return _instance;
        }
    }

    public float CurrentPoints
    {
        get => currentPoints;
        set => currentPoints = value;
    }

    public float CurrentMoney
    {
        get => currentMoney;
        set => currentMoney = value;
    }

    public float CurrentHappiness
    {
        get => currentHappiness;
        set => currentHappiness = value;
    }

    private void Awake()
    {
        _instance = this;
        currentHappiness = maxHappiness;
        budgetText.text = "0";
        InvokeRepeating("DecreaseHappinessPerSecond", 1f, 1f);
    }

    public void UpdateBudget(float budget)
    {
        budgetText.text = currentMoney.ToString();
    }

    public bool DoesPlayerHaveEnoughMoney(int requestedPurchaseAmount)
    {
        return currentMoney >= requestedPurchaseAmount;
    }

    private void DecreaseHappinessPerSecond()
    {
        currentHappiness -= happinessDropRate;
        //TODO: If currentHappiness <= 0 GameOver
    }
}
