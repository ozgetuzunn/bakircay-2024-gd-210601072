using UnityEngine;

public class ObjectSelfDestruct : MonoBehaviour
{
    private void OnDestroy()
    {
        // Nesne yok edildiğinde Spawner'ı bul
        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner != null)
        {
            spawner.CheckRemainingObjects();
        }
    }
}
