using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        // Fare ile nesneyi tıklayınca mesafeyi hesapla
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        // Sürükleme sırasında nesneyi takip ettir
        transform.position = GetMouseWorldPosition() + offset;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Fare pozisyonunu dünya koordinatlarına çevir
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}