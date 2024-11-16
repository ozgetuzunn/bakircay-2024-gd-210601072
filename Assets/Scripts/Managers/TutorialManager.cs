using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public void StartGame()
    {
        // Oyunu başlatmak için rastgele bir seviye sahnesine geçiş yap
        string[] levels = { "Level1", "Level2", "Level3" };
        string randomLevel = levels[Random.Range(0, levels.Length)];
        SceneManager.LoadScene(randomLevel);
    }
}
