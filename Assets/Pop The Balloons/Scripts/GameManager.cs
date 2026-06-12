using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Level Configuration")]
    [SerializeField] private LevelData firstLevel;
    [SerializeField] private LevelData HotColorLevel;
    private LevelData currentLevel;
    [SerializeField] private BalloonSpawner balloonSpawner;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject levelSelectionPanel;

    private int currentScore = 0;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (levelSelectionPanel != null) levelSelectionPanel.SetActive(true);
        if (winPanel != null) winPanel.SetActive(false);

        if (balloonSpawner != null) balloonSpawner.enabled = false;

        instructionText.text = "";
        if (scoreText != null) scoreText.text = "";
    }


    // I know this is not optimal but I don't have the time to do a fully modular level selection system.
    public void StartFirstLevel()
    {
        SartLevel(firstLevel);
    }
    public void StartHotColorLevel()
    {
        SartLevel(HotColorLevel);
    }

    private void SartLevel(LevelData level)
    {
        currentLevel = level;
        currentScore = 0;
        isGameOver = false;

        instructionText.text = currentLevel.instructionText;
        UpdateScoreUI();

        if (levelSelectionPanel != null) levelSelectionPanel.SetActive(false);

        if (balloonSpawner != null)
        {
            balloonSpawner.currentLevel = currentLevel;
            balloonSpawner.enabled = true;
        }
    }

    public void RegisterPop(bool isCorrect)
    {
        if (isGameOver) return;

        if (isCorrect)
        {
            currentScore++;
            UpdateScoreUI();

            if (currentScore >= currentLevel.targetScore)
            {
                WinGame();
            }
        }
        else
        {
            // To implement : action when wrong balloon is popped (lose points, buzzer sound...)
            Debug.Log("Wrong balloon popped!");
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{currentScore} / {currentLevel.targetScore}";
        }
    }

    private void WinGame()
    {
        isGameOver = true;
        if (winPanel != null) winPanel.SetActive(true);
        if (balloonSpawner != null) balloonSpawner.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}