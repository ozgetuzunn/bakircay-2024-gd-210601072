using UnityEngine;

public class ObjectDragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;

    [Header("Y Constraints")]
    [SerializeField] private float minYPosition = 1.0f;
    [SerializeField] private float maxYPosition = 5.0f;

    [Header("X Constraints")]
    [SerializeField] private float minXPosition = -10.0f;
    [SerializeField] private float maxXPosition = 10.0f;

    [Header("Z Constraints")]
    [SerializeField] private float minZPosition = -10.0f;
    [SerializeField] private float maxZPosition = 10.0f;

    [Header("Drag Settings")]
    [Tooltip("Nesnenin fare konumuna yaklaşma hızı.")]
    [SerializeField] private float dragSpeed = 10f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        // Nesneyi tıklayınca farenin tam merkezine “zıplamasını” istiyorsan offset'i sıfırla.
        // Eğer offset'i korumak istersen buradaki satırı yorum satırına al:
        offset = Vector3.zero;
        //offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        Vector3 targetPosition = mousePos + offset;

        // Eksen sınırlarını uygula
        targetPosition.x = Mathf.Clamp(targetPosition.x, minXPosition, maxXPosition);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minYPosition, maxYPosition);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZPosition, maxZPosition);

        // Doğrudan atamak yerine Lerp ile yumuşak geçiş yap
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dragSpeed);
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Fare pozisyonunu dünya koordinatlarına çevir
        Vector3 mouseScreenPosition = Input.mousePosition;
        // Z değerini, objenin kamera hizasındaki z konumu olarak ayarla
        mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
