using UnityEngine;
using DG.Tweening;

public class ObjectAnimation : MonoBehaviour
{
    public void PlayCorrectPlacementAnimation()
    {
        // Doğru yerleştirme animasyonu: Nesne büyür ve küçülür
        transform.DOScale(Vector3.one * 1.2f, 0.3f).SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => PlayDisappearAnimation());
    }

    public void PlayIncorrectPlacementAnimation()
    {
        // Yanlış yerleştirme animasyonu: Nesne sağa sola sallanır
        transform.DOShakePosition(0.5f, strength: new Vector3(0.5f, 0, 0));
    }

    public void PlayBombPlacementAnimation()
    {
        // Bombanın yanlış yerleştirme animasyonu: Hafif ekran titremesi ve boyut küçülerek kaybolma
        Sequence bombSequence = DOTween.Sequence();

        // Hafif ekran titremesi
        Camera.main.DOShakePosition(0.5f, strength: 0.3f);

        // Boyut küçülerek kaybolma
        bombSequence
            .Append(transform.DOScale(Vector3.zero, 1.0f)) // Boyut küçülme
            .OnComplete(() => Destroy(gameObject)); // Nesneyi yok et
    }

    private void PlayDisappearAnimation()
    {
        // Yukarı zıplama ve rotasyon
        Sequence disappearSequence = DOTween.Sequence();

        disappearSequence
            .Append(transform.DOMoveY(transform.position.y + 2f, 0.5f)) // Yukarı zıplama
            .Join(transform.DORotate(new Vector3(0, 0, 720), 0.5f, RotateMode.FastBeyond360)) // 6-7 tur rotasyon
            .Append(transform.DOScale(Vector3.zero, 0.3f)) // Kaybolma
            .OnComplete(() => Destroy(gameObject)); // Animasyon bittiğinde nesneyi yok et
    }
}
