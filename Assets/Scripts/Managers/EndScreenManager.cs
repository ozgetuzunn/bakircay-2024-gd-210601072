using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    private void Start()
    {
        finalScoreText.text = $"Final Score: {GameManager.Instance.score}";
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
}
