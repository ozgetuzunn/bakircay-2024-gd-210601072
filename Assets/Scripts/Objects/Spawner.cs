using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Prefablerin listesi
    public Transform[] spawnPoints; // Spawn noktalarının listesi

    private GameObject[] activeObjects; // Aktif nesnelerin takibi
    private int totalObjects;

    private void Start()
    {
        SpawnObjects(); // Oyun başında nesneleri spawn et
    }

    public void SpawnObjects()
    {
        // Spawn edilecek toplam nesne
        totalObjects = objectPrefabs.Length;

        activeObjects = new GameObject[totalObjects];

        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            activeObjects[i] = Instantiate(objectPrefabs[i], spawnPoint.position, Quaternion.identity);
        }
    }

    public void CheckRemainingObjects()
    {
        int remaining = 0;

        foreach (var obj in activeObjects)
        {
            if (obj != null) remaining++;
        }

        if (remaining == 0)
        {
            // Tüm nesneler yok edildiğinde yeniden spawn et
            SpawnObjects();
        }
    }
}
