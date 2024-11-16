using UnityEngine;
using TMPro; // TextMeshPro kütüphanesini ekleyin
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = 90f; // Süre sınırı
    public TextMeshProUGUI timerText; // Text yerine TextMeshProUGUI

    private void Update()
    {
        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0)
        {
            // Süre dolduğunda
            Debug.Log("Time's up! Game Over!");
            SceneManager.LoadScene("GameOverScene"); // Game Over sahnesine geçiş
        }

        // Timer UI'sini güncelle
        timerText.text = "Time: " + Mathf.Ceil(timeLimit).ToString();
    }
}
