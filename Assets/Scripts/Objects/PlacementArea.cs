using UnityEngine;
using DG.Tweening;

public class PlacementArea : MonoBehaviour
{
    public string requiredTag; // This area accepts this tag
    public AudioManager audioManager; // Reference to AudioManager
    public ParticleManager particleManager; // Reference to ParticleManager

    private bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        // Get the UIManager from the scene
        UIManager uiManager = FindObjectOfType<UIManager>();

        if (!isOccupied && other.CompareTag(requiredTag))
        {
            // Correct object placed
            other.transform.position = transform.position; // Position the object
            isOccupied = true;

            // Play success feedback
            audioManager.PlaySuccessSound();
            particleManager.PlaySuccessParticles(transform.position);
            other.GetComponent<ObjectAnimation>().PlayCorrectPlacementAnimation();

            // Add score
            GameManager.Instance.AddScore(5);
            uiManager.UpdateUI(); // Update the UI
        }
        else if (!isOccupied)
        {
            // Wrong object placed
            GameManager.Instance.ReduceLife(); // Reduce lives
            GameManager.Instance.AddScore(-10); // Deduct score
            uiManager.UpdateUI(); // Update the UI

            // Play error feedback
            audioManager.PlayErrorSound();
            other.GetComponent<ObjectAnimation>().PlayIncorrectPlacementAnimation();

            // Shake camera for visual feedback
            Camera.main.DOShakePosition(0.5f, strength: new Vector3(0.3f, 0.3f, 0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag) || other.CompareTag("Draggable"))
        {
            // Reset the occupied state
            isOccupied = false;
        }
    }
}
