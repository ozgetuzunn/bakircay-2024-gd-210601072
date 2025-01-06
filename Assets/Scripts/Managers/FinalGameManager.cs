using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGameManager : MonoBehaviour
{
    private static FinalGameManager instance;
    public static FinalGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("FinalGameManager instance is null! Did you add it to the scene?");
            }
            return instance;
        }
    }

    [Header("Basic Stats")]
    public int score = 0;    // Toplam skor
    public int lives = 3;    // Can sayısı

    [Header("Wave / Level Mechanics")]
    public int currentWave = 1;      // Şu anki dalga
    public int maxWaveCount = 3;     // Kaç dalga sonuna kadar devam etsin?
    public bool infiniteWaves = false; // Sonsuz dalga seçeneği istersen

    // Final sahnede NESNELERİ üretmek için:
    public FinalSpawner finalSpawner;

    private void Awake()
    {
        // Singleton yaklaşımı
        if (instance == null)
        {
            instance = this;
            Debug.Log("FinalGameManager Awake: Instance set.");
        }
        else
        {
            Debug.Log("FinalGameManager Awake: Duplicate instance found, destroying this object.");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Debug.Log($"FinalGameManager Start: currentWave={currentWave}");

        // Sahne başlar başlamaz ilk dalga spawn
        if (finalSpawner != null)
        {
            Debug.Log($"FinalGameManager: Calling SpawnWave({currentWave})");
            finalSpawner.SpawnWave(currentWave);
        }
        else
        {
            Debug.LogWarning("FinalGameManager: finalSpawner is not assigned in the Inspector.");
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"FinalGameManager AddScore: amount={amount}, new score={score}");
        FinalUIManager.Instance?.UpdateUI();
    }

    public void ReduceLife(int amount = 1)
    {
        lives -= amount;
        Debug.Log($"FinalGameManager ReduceLife: amount={amount}, new lives={lives}");

        if (lives <= 0)
        {
            Debug.Log("Game Over! Lives = 0");
            SceneManager.LoadScene("FinalGameOver");
            return;
        }

        FinalUIManager.Instance?.UpdateUI();
    }

    /// <summary>
    /// Wave bittiğinde Spawner tarafından çağrılacak.
    /// </summary>
    public void OnWaveCleared()
    {
        Debug.Log($"FinalGameManager OnWaveCleared: Wave {currentWave} is cleared.");

        if (infiniteWaves)
        {
            // Sonsuz dalga
            currentWave++;
            Debug.Log($"FinalGameManager OnWaveCleared: Next wave is {currentWave} (infinite mode).");
            finalSpawner.SpawnWave(currentWave);
        }
        else
        {
            // Sınırlı dalga
            currentWave++;
            Debug.Log($"FinalGameManager OnWaveCleared: Next wave is {currentWave} (limited mode).");

            if (currentWave > maxWaveCount)
            {
                Debug.Log("Tüm dalgalar tamamlandı! Tebrikler!");
                SceneManager.LoadScene("FinalWinScene");
            }
            else
            {
                finalSpawner.SpawnWave(currentWave);
            }
        }

        // UI güncelle
        Debug.Log($"FinalGameManager OnWaveCleared: currentWave is now {currentWave}. Updating UI.");
        FinalUIManager.Instance?.UpdateUI();
    }
}
