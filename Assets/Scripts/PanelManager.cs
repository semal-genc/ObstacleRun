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

    public void TryAgain()
    {
        Time.timeScale = 1;
        menu.SetActive(true);
        SceneManager.LoadScene(1);
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
        SceneManager.LoadScene(0);
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
