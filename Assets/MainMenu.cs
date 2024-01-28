using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject creditsPanel;
    
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMenuPanel()
    {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    
    public void OpenCreditsPanel()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
