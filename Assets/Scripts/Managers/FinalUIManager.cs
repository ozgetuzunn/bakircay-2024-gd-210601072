using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

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
    public TextMeshProUGUI cooldownText1; // Yetenek 1 için geri sayım
    public TextMeshProUGUI cooldownText2; // Yetenek 2 için geri sayım
    public TextMeshProUGUI infoMessageText; // Bilgilendirme mesajı

    [Header("Buttons")]
    public Button resetButton;
    public Button ability1Button; // Skor Katlayıcı
    public Button ability2Button; // Ek Can

    [Header("Cooldown Settings")]
    public float abilityCooldown = 5f;

    private bool ability1OnCooldown = false;
    private bool ability2OnCooldown = false;

    private void Awake()
    {
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

        // Cooldown text başlangıçta boş olsun
        cooldownText1.text = "";
        cooldownText2.text = "";
        infoMessageText.text = ""; // Bilgi mesajı başlangıçta boş
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

        if (abilityIndex == 1 && !ability1OnCooldown)
        {
            FinalGameManager.Instance.isScoreMultiplierActive = true;
            ShowInfoMessage("Skor 2'ye katlanacak!"); // Bilgi mesajı
            StartCoroutine(HandleAbilityCooldown(ability1Button, cooldownText1, 1));
            ability1Button.transform.DOPunchScale(Vector3.one * 0.5f, 0.3f, 5, 1);
        }
        else if (abilityIndex == 2 && !ability2OnCooldown)
        {
            FinalGameManager.Instance.AddLife(1);
            ShowInfoMessage("+1 Can Eklendi!"); // Bilgi mesajı
            UpdateUI();
            StartCoroutine(HandleAbilityCooldown(ability2Button, cooldownText2, 2));
            ability2Button.transform.DOPunchScale(Vector3.one * 0.5f, 0.3f, 5, 1);
        }
    }

    private IEnumerator HandleAbilityCooldown(Button abilityButton, TextMeshProUGUI cooldownText, int abilityIndex)
    {
        abilityButton.interactable = false;
        float cooldown = abilityCooldown;

        if (abilityIndex == 1) ability1OnCooldown = true;
        if (abilityIndex == 2) ability2OnCooldown = true;

        while (cooldown > 0)
        {
            cooldownText.text = $"{cooldown:F1}s";
            cooldown -= Time.deltaTime;
            yield return null;
        }

        cooldownText.text = "";
        abilityButton.interactable = true;

        if (abilityIndex == 1) ability1OnCooldown = false;
        if (abilityIndex == 2) ability2OnCooldown = false;

        Debug.Log($"Ability {abilityIndex} cooldown finished. Button re-enabled.");
    }

    private void ShowInfoMessage(string message)
    {
        infoMessageText.text = message;
        StartCoroutine(ClearInfoMessageAfterDelay(3f));
    }

    private IEnumerator ClearInfoMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        infoMessageText.text = "";
    }
}
