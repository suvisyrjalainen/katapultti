using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText; // Vedä UI-teksti tähän Inspectorissa

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Pisteet: " + score);
        UpdateUI();
    }
    
    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Pisteet: " + score;
    }
    
}
