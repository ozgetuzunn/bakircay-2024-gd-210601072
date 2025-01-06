using UnityEngine;

public class FinalSpawner : MonoBehaviour
{
    [Header("Prefab Dizileri")]
    public GameObject[] wave1Prefabs;
    public GameObject[] wave2Prefabs;
    public GameObject[] wave3Prefabs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private GameObject[] activeObjects;

    public void SpawnWave(int waveNumber)
    {
        Debug.Log($"FinalSpawner SpawnWave: waveNumber={waveNumber}");

        GameObject[] selectedPrefabs = null;
        switch (waveNumber)
        {
            case 1:
                selectedPrefabs = wave1Prefabs;
                break;
            case 2:
                selectedPrefabs = wave2Prefabs;
                break;
            case 3:
                selectedPrefabs = wave3Prefabs;
                break;
            default:
                selectedPrefabs = wave1Prefabs;
                break;
        }

        if (selectedPrefabs == null || selectedPrefabs.Length == 0)
        {
            Debug.LogWarning($"No prefabs found for wave {waveNumber}");
            return;
        }

        activeObjects = new GameObject[selectedPrefabs.Length];
        for (int i = 0; i < selectedPrefabs.Length; i++)
        {
            if (spawnPoints == null || spawnPoints.Length == 0)
            {
                Debug.LogWarning("spawnPoints array is empty.");
                return;
            }
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            activeObjects[i] = Instantiate(selectedPrefabs[i], spawnPoint.position, Quaternion.identity);
            Debug.Log($"Instantiated prefab '{selectedPrefabs[i].name}' at {spawnPoint.position}.");
        }
    }

    public void CheckRemainingObjects()
    {
        Debug.Log("CheckRemainingObjects() called.");
        if (activeObjects == null || activeObjects.Length == 0)
        {
            Debug.LogWarning("activeObjects array is empty. Possibly no wave was spawned?");
            return;
        }

        int remaining = 0;
        for (int i = 0; i < activeObjects.Length; i++)
        {
            if (activeObjects[i] != null)
            {
                remaining++;
                Debug.Log($"CheckRemainingObjects: activeObjects[{i}] = {activeObjects[i].name}");
            }
            else
            {
                Debug.Log($"CheckRemainingObjects: activeObjects[{i}] is null");
            }
        }
        Debug.Log($"remaining={remaining}");

        // BURADAKİ KOŞULU DEĞİŞTİRDİK:
        // "remaining == 0" yerine "remaining <= 1" kontrolü yapıyoruz.
        if (remaining <= 1)
        {
            Debug.Log("remaining <= 1: Forcing wave cleared!");
            FinalGameManager.Instance?.OnWaveCleared();
        }
    }
}
