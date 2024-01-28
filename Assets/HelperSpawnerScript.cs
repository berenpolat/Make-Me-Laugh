using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class HelperSpawnerScript : MonoBehaviour
{
    public GameObject healerPrefab;
    private bool buttonUsed = false;
    public Button helperButton;
    
    private ColorBlock theColor;
 
    // Use this for initialization
    void Awake () {
        helperButton = GetComponent<Button>();

    }
    public void SpawnHealer()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null && !buttonUsed)
        {
            healerPrefab.SetActive(true);

        }
        
    }
}