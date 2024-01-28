using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperSpawnerScript : MonoBehaviour
{
    public GameObject healerPrefab;
    private bool buttonUsed = false;
    public Button helperButton;
    public GameManager gm;
    
    private ColorBlock theColor;
 
    // Use this for initialization
    void Awake () {
        helperButton = GetComponent<UnityEngine.UI.Button>();
    }
    public void SpawnHealer()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null && !buttonUsed)
        {
            gm.BuyStuff(50);
            healerPrefab.SetActive(true);
        }
        
    }
}