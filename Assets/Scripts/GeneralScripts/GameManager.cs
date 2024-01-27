using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float currentPoints;
    private float currentMoney;
    [SerializeField] private Text budgetText;
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

    private void Awake()
    {
        _instance = this;
        budgetText.text = "0";
    }

    public void UpdateBudget(float budget)
    {
        budgetText.text = currentMoney.ToString();
    }

    public bool DoesPlayerHaveEnoughMoney(int requestedPurchaseAmount)
    {
        return currentMoney >= requestedPurchaseAmount;
    }
}
