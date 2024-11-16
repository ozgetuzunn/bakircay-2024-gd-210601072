using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject[] hearts;
    public TextMeshProUGUI motivationText;

    private int score = 0;
    private int lives = 3;

    // Motivasyon mesajları dizisi (arttırıldı)
    private string[] motivationMessages = {
    "Harika!",
    "Mükemmel!",
    "Muhteşem!",
    "Devam et!",
    "Çok İyi!",
    "Fantastik!",
    "Sen Harikasın!",
    "Durdurulamazsın!"
};

    private int currentMessageIndex = 0;

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = $"{score}";

        if (points > 0)
        {
            ShowMotivationText();
        }
        else if (points < 0)
        {
            // Bomb yerleştirme mesajı
            motivationText.text = "Yiyecek koyun!";
            motivationText.gameObject.SetActive(true);

            // Animasyon oynat ve kapat
            motivationText.DOFade(0, 1).OnComplete(() =>
            {
                motivationText.gameObject.SetActive(false);
                motivationText.DOFade(1, 0); // Opaklığı geri yükle
            });
        }
    }


    private void ShowMotivationText()
    {
        // Şu anki mesajı göster
        motivationText.text = motivationMessages[currentMessageIndex];
        motivationText.gameObject.SetActive(true);

        // Animasyon oynat ve kapat
        motivationText.DOFade(0, 1).OnComplete(() =>
        {
            motivationText.gameObject.SetActive(false);
            motivationText.DOFade(1, 0); // Opaklığı geri yükle
        });

        // Bir sonraki mesaja geç
        currentMessageIndex = (currentMessageIndex + 1) % motivationMessages.Length;
    }

    public void ReduceLife()
    {
        if (lives > 0)
        {
            lives--;
            hearts[lives].SetActive(false);

            // Kırmızı ekran titremesi
            Camera.main.DOShakePosition(0.5f, strength: 0.3f);
            // Ekrana kırmızı ton efekti eklenebilir
        }

        if (lives == 0)
        {
            Debug.Log("Game Over!");
            // Oyunu bitirme işlemleri burada yapılabilir.
        }
    }
}
