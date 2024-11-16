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

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";

        // Puan animasyonu
        motivationText.text = points > 0 ? "Wonderful!" : "Great!";
        motivationText.gameObject.SetActive(true);
        motivationText.DOFade(0, 1).OnComplete(() => motivationText.gameObject.SetActive(false));
    }

    public void ReduceLife()
    {
        if (lives > 0)
        {
            lives--;
            hearts[lives].SetActive(false);

            // Kırmızı ekran titremesi
            Camera.main.DOShakePosition(0.5f, strength: 0.3f);
        }

        if (lives == 0)
        {
            Debug.Log("Game Over!");
            // Oyunu bitirme işlemleri burada yapılabilir.
        }
    }
}
