using UnityEngine;

public class FinalSpawner : MonoBehaviour
{
    [Header("Prefab Dizileri")]
    public GameObject[] wave1Prefabs;
    public GameObject[] wave2Prefabs;
    public GameObject[] wave3Prefabs;
    public GameObject[] wave4Prefabs;
    public GameObject[] wave5Prefabs;
    public GameObject[] wave6Prefabs;
    public GameObject[] wave7Prefabs;
    public GameObject[] wave8Prefabs;
    public GameObject[] wave9Prefabs;
    public GameObject[] wave10Prefabs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private GameObject[] activeObjects;

    public void SpawnWave(int waveNumber)
    {
        Debug.Log($"FinalSpawner SpawnWave: waveNumber={waveNumber}");

        // Hangi dalga ise ona uygun prefab dizisini seçiyoruz.
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
            case 4:
                selectedPrefabs = wave4Prefabs;
                break;
            case 5:
                selectedPrefabs = wave5Prefabs;
                break;
            case 6:
                selectedPrefabs = wave6Prefabs;
                break;
            case 7:
                selectedPrefabs = wave7Prefabs;
                break;
            case 8:
                selectedPrefabs = wave8Prefabs;
                break;
            case 9:
                selectedPrefabs = wave9Prefabs;
                break;
            case 10:
                selectedPrefabs = wave10Prefabs;
                break;
            default:
                // 10'dan büyük dalgalar gelirse,
                // yine wave10Prefabs kullanabilir ya da kendi mantığınızı ekleyebilirsiniz.
                Debug.LogWarning($"Dalga sayısı ({waveNumber}) 10'u aştı! " +
                                 "Burada isterseniz infinite wave mantığı ekleyebilirsiniz.");
                selectedPrefabs = wave10Prefabs;
                break;
        }

        // Seçili prefab dizisi boş mu?
        if (selectedPrefabs == null || selectedPrefabs.Length == 0)
        {
            Debug.LogWarning($"No prefabs found for wave {waveNumber}");
            return;
        }

        // Prefableri saklayacağımız dizi
        activeObjects = new GameObject[selectedPrefabs.Length];

        // Prefableri rasgele spawn noktasında instantiate ediyoruz
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

        // "remaining <= 1" kontrolü yapıyoruz (örneğin Boss dalgasında son bir enemy kalsa bile dalga bitsin diye).
        if (remaining <= 1)
        {
            Debug.Log("remaining <= 1: Forcing wave cleared!");
            FinalGameManager.Instance?.OnWaveCleared();
        }
    }
}
