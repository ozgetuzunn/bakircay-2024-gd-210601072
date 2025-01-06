using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalUIManager : MonoBehaviour
{
    private static FinalUIManager instance;
    public static FinalUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("FinalUIManager instance is null! Did you add it to the scene?");
            }
            return instance;
        }
    }

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI waveText;

    [Header("Buttons (Optional)")]
    public Button resetButton;
    public Button ability1Button;
    public Button ability2Button;

    private void Awake()
    {
        // Singleton yaklaşımı
        if (instance == null)
        {
            instance = this;
            Debug.Log("FinalUIManager Awake: Instance set.");
        }
        else
        {
            Debug.Log("FinalUIManager Awake: Duplicate instance found, destroying this object.");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Debug.Log("FinalUIManager Start: Calling UpdateUI()");
        UpdateUI();

        if (resetButton != null)
            resetButton.onClick.AddListener(ResetGame);

        if (ability1Button != null)
            ability1Button.onClick.AddListener(() => UseAbility(1));

        if (ability2Button != null)
            ability2Button.onClick.AddListener(() => UseAbility(2));
    }

    public void UpdateUI()
    {
        if (FinalGameManager.Instance == null)
        {
            Debug.LogWarning("FinalUIManager UpdateUI: FinalGameManager.Instance is null!");
            return;
        }

        scoreText.text = $"Score: {FinalGameManager.Instance.score}";
        livesText.text = $"Lives: {FinalGameManager.Instance.lives}";
        waveText.text = $"Wave: {FinalGameManager.Instance.currentWave}";

        Debug.Log($"FinalUIManager UpdateUI: Score={FinalGameManager.Instance.score}, "
                + $"Lives={FinalGameManager.Instance.lives}, Wave={FinalGameManager.Instance.currentWave}");
    }

    private void ResetGame()
    {
        Debug.Log("FinalUIManager ResetGame: Reloading FinalScene...");
        SceneManager.LoadScene("FinalScene");
    }

    private void UseAbility(int abilityIndex)
    {
        Debug.Log("FinalUIManager UseAbility: Ability " + abilityIndex + " used!");
        // 5sn cooldown vb. eklenebilir
    }
}
