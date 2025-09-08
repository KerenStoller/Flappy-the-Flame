using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance {get; private set;}
    
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject startGameButton;
    private int _score;
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        Application.targetFrameRate = 60;
        startGameButton.SetActive(true);
        _gameOverScreen.SetActive(false);
        Pause();
    }

    public void Play()
    {
        startGameButton.SetActive(false);
        _gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        Dragon.Instance.enabled = true;  // calls OnEnable in Dragon
        _score = 0;
        _scoreText.text = _score.ToString();
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        Dragon.Instance.enabled = false;
    }

    [ContextMenu("Increase Score")]
    public void IncreaseScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
    
    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        _gameOverScreen.SetActive(false);
        
        // Destroy all existing towers
        Tower[] existingTowers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
        foreach (Tower tower in existingTowers)
        {
            Destroy(tower.gameObject);
        }
        
        Play();
    }

    [ContextMenu("Game Over")]
    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        Pause();
    }
}
