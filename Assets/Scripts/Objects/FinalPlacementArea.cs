using UnityEngine;
using DG.Tweening;

public class FinalPlacementArea : MonoBehaviour
{
    [Header("Efekt ve Ses")]
    public AudioManager audioManager;         // Ses efektleri
    public ParticleManager particleManager;   // Partikül efektleri

    [Header("Puanlama")]
    public int correctScore = 10;            // Doğru eşleşmede verilecek puan
    public int wrongScore = -5;              // Yanlış yerleştirme / nesnede çıkarılacak puan

    // Alanda şu an bulunan nesne
    private GameObject occupantObject;
    private int occupantPairID = -1;          // occupant’ın pairID değeri

    private void OnTriggerEnter(Collider other)
    {
        // Burada UIManager erişimine ihtiyacın olabilir:
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogWarning("UIManager not found in the scene!");
        }

        // Nesnenin PairID scripti var mı?
        PairID incomingPair = other.GetComponent<PairID>();
        if (incomingPair == null)
        {
            // Bu nesnenin pairID yok, o zaman yanlış olarak değerlendirebiliriz.
            HandleWrongObject(other.gameObject, uiManager);
            return;
        }

        // occupantObject boş ise => bu ilk nesne
        if (occupantObject == null)
        {
            // occupant olarak kaydet
            occupantObject = other.gameObject;
            occupantPairID = incomingPair.pairID;

            // Nesneyi tam alana yerleştirelim (merkezine sabitleme vb.)
            occupantObject.transform.position = transform.position;

            // Doğru yerleştirme animasyonu (ama henüz eşleşme tamamlanmadı, tek nesne)
            FinalObjectsAnimation anim = occupantObject.GetComponent<FinalObjectsAnimation>();
            anim?.PlayCorrectPlacementAnimation();

            // Ses / partikül
            audioManager?.PlaySuccessSound();
            particleManager?.PlaySuccessParticles(transform.position);
        }
        else
        {
            // Alanda zaten bir occupant var, bu gelen nesne ikinci nesne:
            int incomingID = incomingPair.pairID;

            if (incomingID == occupantPairID)
            {
                // => Eşleşme Başarılı
                HandleMatch(occupantObject, other.gameObject, uiManager);
            }
            else
            {
                // => Yanlış Eşleşme
                HandleWrongObject(other.gameObject, uiManager);
            }
        }
    }

    private void HandleMatch(GameObject first, GameObject second, UIManager uiManager)
    {
        // Her ikisinde de animasyon oynatıp kaybetmelerini sağlayacağız
        FinalObjectsAnimation anim1 = first.GetComponent<FinalObjectsAnimation>();
        FinalObjectsAnimation anim2 = second.GetComponent<FinalObjectsAnimation>();

        // Doğru eşleşme animasyonu: 
        anim1?.PlayCorrectPlacementAnimation(() =>
        {
            anim1.PlayDisappearAnimation();
        });

        anim2?.PlayCorrectPlacementAnimation(() =>
        {
            anim2.PlayDisappearAnimation();
        });

        // Skor ekle
        GameManager.Instance.AddScore(correctScore);
        uiManager?.UpdateUI();

        // Ses ve partikül efektleri
        audioManager?.PlaySuccessSound();
        particleManager?.PlaySuccessParticles(transform.position);

        // occupant’ı sıfırla
        occupantObject = null;
        occupantPairID = -1;
    }

    private void HandleWrongObject(GameObject wrongObj, UIManager uiManager)
    {
        // Yanlış yerleştirme animasyonu
        FinalObjectsAnimation anim = wrongObj.GetComponent<FinalObjectsAnimation>();

        anim?.PlayIncorrectPlacementAnimation();

        // Skor düş
        GameManager.Instance.AddScore(wrongScore);

        // Can da azaltabiliriz (isteğe bağlı)
        GameManager.Instance.ReduceLife();

        // UI güncelle
        uiManager?.UpdateUI();

        // Hata sesi ve partikül
        audioManager?.PlayErrorSound();
        particleManager?.PlayErrorParticles(wrongObj.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        // occupant alanı boşaltıyorsa (yanlış nesne çıkarsa vs.)
        if (other.gameObject == occupantObject)
        {
            occupantObject = null;
            occupantPairID = -1;
        }
    }
}
