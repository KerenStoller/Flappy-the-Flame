using UnityEngine;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance {get; private set;}
    
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverScreen;
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
        gameOverScreen.SetActive(false);
        Pause();
    }

    public void Play()
    {
        startGameButton.SetActive(false);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        Dragon.Instance.enabled = true;  // calls OnEnable in Dragon
        _score = 0;
        scoreText.text = _score.ToString();
    }
    
    private void Pause()
    {
        Time.timeScale = 0f;
        Dragon.Instance.enabled = false;
    }

    [ContextMenu("Increase Score")]
    public void IncreaseScore()
    {
        _score++;
        scoreText.text = _score.ToString();
    }
    
    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        
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
        gameOverScreen.SetActive(true);
        Pause();
    }
}
