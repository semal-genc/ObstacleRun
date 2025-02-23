using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI lose_score;
    [SerializeField] TMPro.TextMeshProUGUI high_skor;
    Manager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<Manager>();
        ScorePanel();
        HighScorePanel();
    }

    void ScorePanel()
    {
        lose_score.text = "SCORE : " + manager.point;
    }

    void HighScorePanel()
    {
        high_skor.text = "HIGH SCORE : " + PlayerPrefs.GetInt("high_score").ToString();
    }
}
