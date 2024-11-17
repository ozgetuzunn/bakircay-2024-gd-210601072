using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject uiCanvas; // Canvas referansı

    public int score = 0; // Genel skor
    public int lives = 3; // Genel can
    private int currentLevelIndex = 0; // Tutorial sahnesinden başlayarak seviye sırasını takip eder
    private string[] levels = { "Level1", "Level2", "Level3" };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // GameManager'ı koru

            if (uiCanvas != null)
            {
                DontDestroyOnLoad(uiCanvas); // Canvas'ı taşımayı sağla
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager instance is null!");
            }
            return instance;
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void ReduceLife()
    {
        if (lives > 0)
        {
            lives--;
        }

        if (lives == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("EndScreen"); // Can bittiğinde EndScreen'e geçiş yap
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex < levels.Length)
        {
            SceneManager.LoadScene(levels[currentLevelIndex]);
            currentLevelIndex++;
        }
        else
        {
            Debug.Log("Tüm seviyeler tamamlandı! Tebrikler!");
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void RestartGame()
    {
        score = 0;
        lives = 3;
        currentLevelIndex = 0;
        SceneManager.LoadScene("Tutorial");
    }
}
