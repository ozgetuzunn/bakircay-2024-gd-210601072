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
    public int currentWave = 1;

    // 10 dalga:
    public int maxWaveCount = 10;
    public bool infiniteWaves = false;

    // ========== YETENEK EKLEMELERİ =============
    [HideInInspector] public bool isScoreMultiplierActive = false; // Yetenek1: Skor Katlayıcı

    // Final sahnede NESNELERİ üretmek için:
    public FinalSpawner finalSpawner;

    private void Awake()
    {
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
        int finalAmount = amount;

        // Eğer skor katlayıcı aktifse, bu eklemeyi 2x yapıyoruz.
        if (isScoreMultiplierActive && amount > 0) // Sadece pozitif skor arttırmalarda katla
        {
            finalAmount = amount * 2;
            // Bir kez kullanıldıktan sonra kapatalım
            isScoreMultiplierActive = false;
            Debug.Log("Skor katlayıcı kullanıldı. Sonraki eşleşmede devre dışı kalacak.");
        }

        score += finalAmount;
        Debug.Log($"FinalGameManager AddScore: amount={finalAmount}, new score={score}");
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

    // ========== Ek Can Fonksiyonu ===========
    public void AddLife(int amount)
    {
        lives += amount;
        Debug.Log($"FinalGameManager AddLife: amount={amount}, new lives={lives}");
        FinalUIManager.Instance?.UpdateUI();
    }
    // =======================================

    public void OnWaveCleared()
    {
        Debug.Log($"FinalGameManager OnWaveCleared: Wave {currentWave} is cleared.");

        if (infiniteWaves)
        {
            // Sonsuz dalga modu
            currentWave++;
            Debug.Log($"FinalGameManager OnWaveCleared: Next wave is {currentWave} (infinite mode).");
            finalSpawner.SpawnWave(currentWave);
        }
        else
        {
            // Sınırlı dalga (burada 10 dalga)
            currentWave++;
            Debug.Log($"FinalGameManager OnWaveCleared: Next wave is {currentWave} (limited mode).");

            // currentWave, maxWaveCount (10)'u geçerse oyunu kazanmış oluyoruz.
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

        // Dalga atlaması tamamlandıktan sonra UI güncelle
        FinalUIManager.Instance?.UpdateUI();
    }
}
