using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    private bool isOccupied = false;

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Draggable") && !isOccupied)
    {
        // Doğru yerleştirme
        other.transform.position = transform.position;
        isOccupied = true;

        // Animasyon oynat
        other.GetComponent<ObjectAnimation>().PlayCorrectPlacementAnimation();
    }
    else if (other.CompareTag("Draggable"))
    {
        // Yanlış yerleştirme
        Debug.Log("Placement area is already occupied!");
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
