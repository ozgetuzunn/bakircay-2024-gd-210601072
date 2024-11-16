using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlacementArea : MonoBehaviour
{
    public UIManager uiManager;
    private bool isOccupied = false;

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Draggable") && !isOccupied)
    {
        // Doğru yerleştirme
        other.transform.position = transform.position;
        isOccupied = true;

        uiManager.AddScore(5); // 5 puan ekle
        other.GetComponent<ObjectAnimation>().PlayCorrectPlacementAnimation();
    }
    else if (other.CompareTag("Draggable"))
    {
        // Yanlış yerleştirme
        Debug.Log("Placement area is already occupied!");
        uiManager.ReduceLife(); // Can azalt
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
