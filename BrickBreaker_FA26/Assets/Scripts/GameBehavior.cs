using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreUI;
    private int _score;
    
    public static GameBehavior Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("New instance initialized...");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate instance found and deleted...");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetGame();
    }

    void ResetGame()
    {
            Score = 0;
    }

    public void ScorePoint(int points)
    {
        Score++;
    }

    public int Score
    {
        set
        {
            _score = value;
            _scoreUI.text = _score.ToString();
        }

        get => _score;
    }
}
