using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlacementArea : MonoBehaviour
{
    public UIManager uiManager;
    public AudioManager audioManager;
    private bool isOccupied = false;
    public ParticleManager particleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable") && !isOccupied)
        {
            other.transform.position = transform.position;
            isOccupied = true;

            uiManager.AddScore(5);
            audioManager.PlaySuccessSound(); // Başarılı ses
            particleManager.PlaySuccessParticles(other.transform.position);
            other.GetComponent<ObjectAnimation>().PlayCorrectPlacementAnimation();
        }
        else if (other.CompareTag("Draggable"))
        {
            uiManager.ReduceLife();
            audioManager.PlayErrorSound(); // Hata sesi
            other.GetComponent<ObjectAnimation>().PlayIncorrectPlacementAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            // Alan boşaltıldığında
            isOccupied = false;
        }
    }
}
