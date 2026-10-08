using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreUI;
    private int _score;
    
    public static GameBehavior Instance;
    private Utilities.GameState _state;

    public Utilities.GameState State
    {
        get => _state;
        
        set
        {
            _state = value;
            _pauseUI.enabled = State == Utilities.GameState.Pause;
        }
    }
    
    [SerializeField] private TMP_Text _pauseUI;

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballParent;
    private GameObject _ball;

    [SerializeField] private int _winningScore = 33;

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
        ResetScore();
        ResetPoint();
        State = Utilities.GameState.Play;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            State = State == Utilities.GameState.Play ? Utilities.GameState.Pause : Utilities.GameState.Play;
        }

        if (!_ball)
        {
            ResetPoint();
        }
    }
    
    void ResetScore()
    {
            Score = 0;
    }

    public void ResetPoint()
    {
        _ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity, _ballParent);
    }    
    public void ScorePoint(int points)
    {
        Score++;
        
        if (Score >= _winningScore)
        {
            ResetScore();
        }
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
    
    //ISSUE: Sometimes when I start the game, the ball doesn't reset and I have 2 balls in one game
    //This only happens part of the time, so idk how to tackle fixing this... :(
}
