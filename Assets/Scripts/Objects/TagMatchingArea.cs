using UnityEngine;
using DG.Tweening;
public class TagMatchingArea : MonoBehaviour
{
    public string requiredTag; // PlacementArea'nın kabul edeceği Tag

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            // Doğru küp yerleştirildiğinde
            Debug.Log($"{requiredTag} object placed correctly!");
            other.transform.position = transform.position; // Küpü yerine sabitle
        }
        else
        {
            // Yanlış küp yerleştirildiğinde
            Debug.Log("Incorrect object! Try again.");
            // Görsel geri bildirim için kırmızı ekran titremesi
            Camera.main.DOShakePosition(0.5f, strength: 0.3f);
        }
    }
}
