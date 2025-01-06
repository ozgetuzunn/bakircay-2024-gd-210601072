using UnityEngine;

public class ObjectSelfDestruct : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log($"ObjectSelfDestruct OnDestroy: {gameObject.name} is being destroyed.");

        // Nesne yok edildiğinde FinalSpawner'ı bul
        FinalSpawner spawner = FindObjectOfType<FinalSpawner>();
        if (spawner != null)
        {
            Debug.Log("ObjectSelfDestruct OnDestroy: Calling spawner.CheckRemainingObjects()");
            spawner.CheckRemainingObjects();
        }
        else
        {
            Debug.LogWarning("ObjectSelfDestruct OnDestroy: FinalSpawner not found in scene!");
        }
    }
}
