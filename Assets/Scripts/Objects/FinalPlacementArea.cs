using UnityEngine;
using DG.Tweening;

public class FinalPlacementArea : MonoBehaviour
{
    [Header("Efekt ve Ses")]
    public AudioManager audioManager;
    public ParticleManager particleManager;

    [Header("Puanlama")]
    public int correctScore = 10;
    public int wrongScore = -5;

    private GameObject occupantObject;
    private int occupantPairID = -1;

    private void OnTriggerEnter(Collider other)
    {
        // FinalUIManager kullan
        var uiManager = FinalUIManager.Instance;
        if (uiManager == null) Debug.LogWarning("FinalUIManager not found!");

        var incomingPair = other.GetComponent<PairID>();
        if (incomingPair == null)
        {
            // PairID yok = yanlış
            HandleWrongObject(other.gameObject, uiManager);
            return;
        }

        if (occupantObject == null)
        {
            occupantObject = other.gameObject;
            occupantPairID = incomingPair.pairID;

            occupantObject.transform.position = transform.position;

            // Doğru yerleştirme animasyonu (henüz eşleşme tamam değil)
            var anim = occupantObject.GetComponent<FinalObjectsAnimation>();
            anim?.PlayCorrectPlacementAnimation();

            audioManager?.PlaySuccessSound();
            particleManager?.PlaySuccessParticles(transform.position);
        }
        else
        {
            // 2. nesne geldi
            int incomingID = incomingPair.pairID;
            if (incomingID == occupantPairID)
            {
                // Eşleşme başarılı
                HandleMatch(occupantObject, other.gameObject, uiManager);
            }
            else
            {
                // Yanlış eşleşme
                HandleWrongObject(other.gameObject, uiManager);
            }
        }
    }

    private void HandleMatch(GameObject first, GameObject second, FinalUIManager uiManager)
    {
        var anim1 = first.GetComponent<FinalObjectsAnimation>();
        var anim2 = second.GetComponent<FinalObjectsAnimation>();

        anim1?.PlayCorrectPlacementAnimation(() =>
        {
            anim1.PlayDisappearAnimation();
        });

        anim2?.PlayCorrectPlacementAnimation(() =>
        {
            anim2.PlayDisappearAnimation();
        });

        // **FinalGameManager**'a skor ekle
        FinalGameManager.Instance.AddScore(correctScore);

        uiManager?.UpdateUI();

        audioManager?.PlaySuccessSound();
        particleManager?.PlaySuccessParticles(transform.position);

        occupantObject = null;
        occupantPairID = -1;
    }

    private void HandleWrongObject(GameObject wrongObj, FinalUIManager uiManager)
    {
        var anim = wrongObj.GetComponent<FinalObjectsAnimation>();
        anim?.PlayIncorrectPlacementAnimation();

        // **FinalGameManager**'a skor düş
        FinalGameManager.Instance.AddScore(wrongScore);
        FinalGameManager.Instance.ReduceLife(1);

        uiManager?.UpdateUI();

        audioManager?.PlayErrorSound();
        particleManager?.PlayErrorParticles(wrongObj.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == occupantObject)
        {
            occupantObject = null;
            occupantPairID = -1;
        }
    }
}
