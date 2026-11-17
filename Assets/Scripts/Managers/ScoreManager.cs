using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreText;
    private int currentCoins = 0;

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddCoins(int amount)
    {
        if (gameManager.GameOver) return;
        
        currentCoins += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = currentCoins.ToString("N0");
    }
}
