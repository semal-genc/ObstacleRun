using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GecisReklami gecisReklami;

    private void Start()
    {
        gecisReklami = FindObjectOfType<GecisReklami>();
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void Play()
    {
        if (gecisReklami != null)
        {
            gecisReklami.GecisReklamiGoster(1);  // Reklam kapan�nca sahne 1'e ge�ecek
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
