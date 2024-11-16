using UnityEngine;
using DG.Tweening;

public class PlacementArea : MonoBehaviour
{
    public UIManager uiManager;
    public AudioManager audioManager;
    public ParticleManager particleManager;

    private bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable") && !isOccupied)
        {
            // Nesneyi yerleştir
            other.transform.position = transform.position;
            isOccupied = true;

            // Başarılı işlem
            uiManager.AddScore(5);
            audioManager.PlaySuccessSound();
            particleManager.PlaySuccessParticles(other.transform.position);
            other.GetComponent<ObjectAnimation>().PlayCorrectPlacementAnimation();
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
}
