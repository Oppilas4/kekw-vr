using UnityEngine;
using UnityEngine.UI;

public class DishScoreManager : MonoBehaviour
{
    private float playerScore = 0f;

    // Reference to a UI text element to display the score (you can link this in the Unity Editor)
    public Text scoreText;

    // Singleton pattern to ensure only one instance of ScoreManager exists
    private static DishScoreManager _instance;
    public static DishScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject managerObject = new GameObject("ScoreManager");
                _instance = managerObject.AddComponent<DishScoreManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        // Ensure there's only one instance of ScoreManager in the scene
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateScore(float points)
    {
        playerScore += points;
        Debug.Log("dish points: " + points);
        UpdateScoreUI();
    }

    public void DeductPoints(float points)
    {
        playerScore -= points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        Debug.Log("Score: " + playerScore.ToString("F2"));
        // Update the UI text element with the current score
        if (scoreText != null)
        {
            
            scoreText.text = "Score: " + playerScore.ToString("F2"); // Displaying score with 2 decimal places
        }
    }
}
