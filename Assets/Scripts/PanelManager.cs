using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public GameObject finish_panel;
    public GameObject menuPanel;
    public GameObject menu;
    GecisReklami gecisReklami;

    private void Start()
    {
        gecisReklami = FindObjectOfType<GecisReklami>();
    }

    public void TryAgain()
    {
        menu.SetActive(true);
        if (gecisReklami != null)
        {
            gecisReklami.GecisReklamiGoster(1);  // Reklamdan sonra sahne 1'e geç
        }
        else
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
    }

    public void Menu()
    {
        if (menuPanel != null)
        {
            finish_panel.SetActive(false);
            menuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void MainMenu()
    {
        if (gecisReklami != null)
        {
            gecisReklami.GecisReklamiGoster(0);  // Reklamdan sonra sahne 0'a geç
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Resume()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
