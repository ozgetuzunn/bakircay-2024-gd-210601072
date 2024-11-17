using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public void StartGame()
    {
        // GameManager ile Level1'e geçiş yap
        FindObjectOfType<GameManager>().LoadNextLevel();
    }
}
