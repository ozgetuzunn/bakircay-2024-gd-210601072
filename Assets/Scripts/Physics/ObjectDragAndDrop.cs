using UnityEngine;

public class ObjectDragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;

    // Yükseklik sınırı için minimum değer
    [SerializeField] private float minYPosition = 1.0f; // Ground seviyesindeki Y değeri

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
        Vector3 targetPosition = GetMouseWorldPosition() + offset;

        // Y pozisyonunu minimum seviyeye sabitle
        targetPosition.y = Mathf.Max(targetPosition.y, minYPosition);

        transform.position = targetPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Fare pozisyonunu dünya koordinatlarına çevir
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
