using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject[] hearts;
    public TextMeshProUGUI motivationText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // UIManager'ın taşınmasını sağlar
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Skor UI güncelleme
        scoreText.text = $"Score: {GameManager.Instance.score}";

        // Can UI güncelleme
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < GameManager.Instance.lives);
        }
    }

    public void AddScore(int points)
    {
        GameManager.Instance.AddScore(points); // GameManager üzerinden skor ekle
        UpdateUI();

        if (points > 0)
        {
            ShowMotivationText();
        }
        else if (points < 0)
        {
            motivationText.text = "Yiyecek koyun!";
            PlayFadeAnimation(motivationText);
        }
    }

    public void ReduceLife()
    {
        GameManager.Instance.ReduceLife(); // Can azaltma işlemi
        UpdateUI();
    }

    private void ShowMotivationText()
    {
        string[] motivationMessages = {
            "Harika!", "Mükemmel!", "Muhteşem!", "Devam et!",
            "Çok İyi!", "Fantastik!", "Sen Harikasın!", "Durdurulamazsın!"
        };
        int randomIndex = Random.Range(0, motivationMessages.Length);

        motivationText.text = motivationMessages[randomIndex];
        PlayFadeAnimation(motivationText);
    }

    private void PlayFadeAnimation(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        text.DOFade(0, 1).OnComplete(() =>
        {
            text.gameObject.SetActive(false);
            text.DOFade(1, 0); // Opaklığı geri yükle
        });
    }
}
