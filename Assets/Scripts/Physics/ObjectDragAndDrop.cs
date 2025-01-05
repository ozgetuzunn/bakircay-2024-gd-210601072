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

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 targetPosition = GetMouseWorldPosition() + offset;

        // Pozisyonları sınırla
        targetPosition.x = Mathf.Clamp(targetPosition.x, minXPosition, maxXPosition);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minYPosition, maxYPosition);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZPosition, maxZPosition);

        transform.position = targetPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
