using UnityEngine;
using DG.Tweening;

public class ObjectAnimation : MonoBehaviour
{
    public void PlayCorrectPlacementAnimation()
    {
        // Doğru yerleştirme animasyonu: Nesne büyür ve küçülür
        transform.DOScale(Vector3.one * 1.2f, 0.3f).SetLoops(2, LoopType.Yoyo);
    }

    public void PlayIncorrectPlacementAnimation()
    {
        // Yanlış yerleştirme animasyonu: Nesne sağa sola sallanır
        transform.DOShakePosition(0.5f, strength: new Vector3(0.5f, 0, 0));
    }
}
