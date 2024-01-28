using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text budgetText;
    [SerializeField] private float maxHappiness = 100;
    [SerializeField] private float currentMoney = 100;
    [SerializeField] private float happinessDropRate;
    [SerializeField] private Slider happinessBar;
    [SerializeField] private Image childImagePlace;
    [SerializeField] private Image happinessBarFiller;
    [SerializeField] private Sprite childHappySprite;
    [SerializeField] private Sprite childAngrySprite;
    [SerializeField] private Sprite childSadSprite;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameFinishedPanel;
    [SerializeField] private GameObject buttonPanels;
    
    private float currentPoints;
    private float currentHappiness;

    [SerializeField] private UnityEngine.UI.Button TurretButton1;
    [SerializeField] private UnityEngine.UI.Button TurretButton2;
    [SerializeField] private UnityEngine.UI.Button HelperButton;
    [SerializeField] private UnityEngine.UI.Button ShotGunButton;
    [SerializeField] private UnityEngine.UI.Button PistolButton;
    [SerializeField] private UnityEngine.UI.Button AKButton;



    
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
        set 
        {
            currentHappiness = value;
            DisplayHappiness();
            if(currentHappiness <= 0)
            {
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        _instance = this;
        currentHappiness = maxHappiness;
        DisplayBudget();
        InvokeRepeating(nameof(DecreaseHappinessPerSecond), 1f, 1f);
    }

    private void Update()
    {
        DisplayBudget();
        if (currentMoney >= 100)
        {
            TurretButton1.interactable = true;
            TurretButton2.interactable = true;
            PistolButton.interactable = true;
            HelperButton.interactable = true;
            AKButton.interactable = true;
            ShotGunButton.interactable = true;
        }
        if (currentMoney < 100 && currentMoney > 50)
        {
            TurretButton1.interactable = false;
            TurretButton2.interactable = false;
            PistolButton.interactable = true;
            HelperButton.interactable = true;
            AKButton.interactable = true;
            ShotGunButton.interactable = true;
        }
        else if (currentMoney < 50 && currentMoney > 20)
        {
            TurretButton1.interactable = false;
            TurretButton2.interactable = false;
            PistolButton.interactable = true;
            HelperButton.interactable = false;
            AKButton.interactable = true;
            ShotGunButton.interactable = true;
        }
        else if (currentMoney < 20 && currentMoney > 10)
        {
            TurretButton1.interactable = false;
            TurretButton2.interactable = false;
            PistolButton.interactable = true;
            HelperButton.interactable = false;
            AKButton.interactable = false;
            ShotGunButton.interactable = false;
        }
        else if (currentMoney < 10)
        {
            TurretButton1.interactable = false;
            TurretButton2.interactable = false;
            PistolButton.interactable = false;
            HelperButton.interactable = false;
            AKButton.interactable = false;
            ShotGunButton.interactable = false;
        }
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
        buttonPanels.SetActive(false);
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
            buttonPanels.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

    public void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
