using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DishScoreManager : MonoBehaviour
{
    private float playerScore = 0f;

    // Reference to a UI text element to display the score (you can link this in the Unity Editor)
    public TextMeshProUGUI scoreText;

    public List<GameObject> starGameObjects;
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
    }

    public void EndGamePoints()
    {
        if (scoreText != null)
        {

            scoreText.text = "Score: " + playerScore.ToString("F2"); // Displaying score with 2 decimal places
        }

        // Calculate the number of stars based on the score
        int stars = Mathf.CeilToInt(playerScore / (800f / 5f)); // Assuming 1000 points = 5 stars
        stars = Mathf.Clamp(stars, 0, 5); // Ensure the number of stars does not exceed 5

        // Activate/Deactivate stars based on the score
        for (int i = 0; i < starGameObjects.Count; i++)
        {
            starGameObjects[i].SetActive(i < stars);
        }
    }
}
