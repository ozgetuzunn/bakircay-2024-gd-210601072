using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement; // Sahne yeniden yükleme için gerekli

public class TutorialPlacementArea : MonoBehaviour
{
    public UIManager uiManager;
    public AudioManager audioManager;
    public ParticleManager particleManager;
    public TextMeshProUGUI tutorialText; // Tutorial metni
    public GameObject startGameButton; // Start Game butonu
    public GameObject retryButton; // Retry Button

    private bool isOccupied = false;
    private int placedObjects = 0; // Yerleştirilen nesnelerin sayısı
    public int totalObjectsToPlace = 2; // Yerleştirilmesi gereken toplam nesne

    private void Start()
    {
        // Tutorial başlangıç metni
        tutorialText.text = "Hoş Geldin.\nYiyecekleri tabağa sürükle ve bırak!";
        startGameButton.SetActive(false); // Başlangıçta Start Game butonu devre dışı
        retryButton.SetActive(false); // Başlangıçta Retry butonu devre dışı
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomb"))
        {
            // Bomb yerleştirildiğinde -10 puan cezası ve 1 can eksilt
            uiManager.AddScore(-10);
            uiManager.ReduceLife(); // Kalpleri azalt
            tutorialText.text = "Yiyecek Koyun!";
            audioManager.PlayErrorSound();
            particleManager.PlayErrorParticles(other.transform.position);
            other.GetComponent<ObjectAnimation>().PlayBombPlacementAnimation(); // Bombanın animasyonunu tetikle
        }
        else if (other.CompareTag("Draggable") && !isOccupied)
        {
            // Doğru nesne yerleştirildiğinde
            other.transform.position = transform.position;
            isOccupied = true;

            uiManager.AddScore(5);
            audioManager.PlaySuccessSound();
            particleManager.PlaySuccessParticles(other.transform.position);
            other.GetComponent<ObjectAnimation>().PlayCorrectPlacementAnimation();

            placedObjects++;
            UpdateTutorialText();
        }
        else if (other.CompareTag("Draggable"))
        {
            // Yanlış yerleştirme
            uiManager.ReduceLife();
            audioManager.PlayErrorSound();
            other.GetComponent<ObjectAnimation>().PlayIncorrectPlacementAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            isOccupied = false;
        }
    }

    private void UpdateTutorialText()
    {
        // İlk yerleştirmede metni değiştir
        if (placedObjects == 1)
        {
            tutorialText.text = "Tüm yiyecekleri yerleştir!";
        }

        // Tüm nesneler yerleştirildiyse
        if (placedObjects >= totalObjectsToPlace)
        {
            tutorialText.text = "Artık hazırsın! Hadi başlayalım."; // Yeni metin
            startGameButton.SetActive(true); // Start Game butonunu etkinleştir
            retryButton.SetActive(true); // Retry butonunu etkinleştir
        }
    }

    // Retry butonu için metot
    public void RetryTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükle
    }
}
