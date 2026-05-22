using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using System;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    public GameObject gameOverScreen;
    private int survivedLevelsCount;
    void Start()
    {
        PlayerLife.OnPlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
        Time.timeScale=1;
    }

    void OnDestroy()
    {
        PlayerLife.OnPlayerDied -= GameOverScreen;
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale=0;

    }

    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
