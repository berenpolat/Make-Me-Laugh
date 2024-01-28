using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text budgetText;
    [SerializeField] private float maxHappiness = 100;
    [SerializeField] private float happinessDropRate;
    [SerializeField] private Slider happinessBar;
    [SerializeField] private Image childImagePlace;
    [SerializeField] private Image happinessBarFiller;
    [SerializeField] private Sprite childHappySprite;
    [SerializeField] private Sprite childAngrySprite;
    [SerializeField] private Sprite childSadSprite;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameFinishedPanel;
    
    private float currentPoints;
    private float currentMoney = 0;
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
        InvokeRepeating(nameof(DecreaseHappinessPerSecond), 1f, 1f);
    }

    public void DisplayBudget()
    {
        budgetText.text = currentMoney.ToString();
    }


    public void BearSpawned(EnemyAI bear)
    {
        bear.GameFinished += OnGameFinished;
    }

    private void OnGameFinished(EnemyAI obj)
    {
        Time.timeScale = 0;
        gameFinishedPanel.SetActive(true);
    }

    public void DisplayHappiness()
    {
        happinessBar.value = currentHappiness / 100;
        if (currentHappiness <= 33)
        {
            if(childImagePlace.sprite == childSadSprite)
                return;
            childImagePlace.sprite = childSadSprite;
            happinessBarFiller.color = Color.Lerp(Color.yellow, Color.red, 1f);
        }
        else if (currentHappiness <= 66)
        {
            if(childImagePlace.sprite == childAngrySprite)
                return;
            childImagePlace.sprite = childAngrySprite;
            happinessBarFiller.color = Color.Lerp(Color.green, Color.yellow, 1f);
        }
        else
        {
            if(childImagePlace.sprite == childHappySprite)
                return;
            childImagePlace.sprite = childHappySprite;
            happinessBarFiller.color = Color.Lerp(Color.yellow, Color.green, 1f);
        }
    }
    
    public bool DoesPlayerHaveEnoughMoney(int requestedPurchaseAmount)
    {
        return currentMoney >= requestedPurchaseAmount;
    }

    public void BuyStuff(int requestedPurchaseAmount)
    {
        if (DoesPlayerHaveEnoughMoney(requestedPurchaseAmount))
        {
            CurrentMoney -= requestedPurchaseAmount;
        }
    }

    private void DecreaseHappinessPerSecond()
    {
        currentHappiness -= happinessDropRate;
        DisplayHappiness();
        if (currentHappiness <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    public void ReturnToTheMainMenu()
    {
        
    }

    public void PlayAgain()
    {
        
    }
}
