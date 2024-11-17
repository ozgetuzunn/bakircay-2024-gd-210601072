using UnityEngine;
using DG.Tweening;

public class MovingPlacementArea : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    public float range = 3f; // Movement range
    public bool moveHorizontally = true; // Should it move horizontally or vertically?

    public string requiredTag; // Tag this area accepts
    public AudioManager audioManager; // Reference to AudioManager
    public ParticleManager particleManager; // Reference to ParticleManager

    private Vector3 startPos;
    private bool isOccupied = false;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (moveHorizontally)
        {
            // Horizontal movement
            transform.position = startPos + new Vector3(Mathf.PingPong(Time.time * speed, range), 0, 0);
        }
        else
        {
            // Vertical movement
            transform.position = startPos + new Vector3(0, 0, Mathf.PingPong(Time.time * speed, range));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        UIManager uiManager = FindObjectOfType<UIManager>(); // Find the UIManager in the scene

        if (!isOccupied && other.CompareTag(requiredTag))
        {
            // When the correct object is placed
            other.transform.position = transform.position; // Position the object
            isOccupied = true;

            // Play success feedback
            audioManager.PlaySuccessSound();
            particleManager.PlaySuccessParticles(transform.position);
            other.GetComponent<ObjectAnimation>().PlayCorrectPlacementAnimation();

            // Add score
            GameManager.Instance.AddScore(5);
            uiManager.UpdateUI(); // Update UI
        }
        else if (!isOccupied && other.CompareTag("Draggable"))
        {
            // When the wrong object is placed
            GameManager.Instance.ReduceLife(); // Reduce life
            GameManager.Instance.AddScore(-10); // Reduce score
            uiManager.UpdateUI(); // Update UI

            // Play error feedback
            audioManager.PlayErrorSound();
            other.GetComponent<ObjectAnimation>().PlayIncorrectPlacementAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag) || other.CompareTag("Draggable"))
        {
            // Reset the occupied state when the object leaves the area
            isOccupied = false;
        }
    }
}
