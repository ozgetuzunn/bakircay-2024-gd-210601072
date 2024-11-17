using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void CompleteLevel()
    {
        // Seviye tamamlandığında GameManager'dan bir sonraki seviyeye geç
        FindObjectOfType<GameManager>().LoadNextLevel();
    }
}
