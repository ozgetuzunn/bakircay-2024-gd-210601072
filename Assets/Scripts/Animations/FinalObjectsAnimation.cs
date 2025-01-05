using UnityEngine;
using DG.Tweening;

public class FinalObjectsAnimation : MonoBehaviour
{
    [Header("Animasyon Ayarları")]
    [SerializeField] private float correctScaleFactor = 1.2f;
    [SerializeField] private float correctScaleTime = 0.3f;
    [SerializeField] private float throwDistance = 3f;
    [SerializeField] private float throwTime = 0.5f;
    [SerializeField] private float disappearTime = 0.3f;

    public void PlayCorrectPlacementAnimation(System.Action onComplete = null)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(transform.localScale * correctScaleFactor, correctScaleTime)
            .SetLoops(2, LoopType.Yoyo));
        seq.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    public void PlayIncorrectPlacementAnimation(System.Action onComplete = null)
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f)).normalized;
        Vector3 targetPos = transform.position + randomDir * throwDistance;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetPos, throwTime).SetEase(Ease.OutQuad));
        seq.Join(transform.DOScale(transform.localScale * 0.8f, throwTime));
        seq.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    public void PlayDisappearAnimation()
    {
        transform.DOScale(Vector3.zero, disappearTime)
                 .OnComplete(() => Destroy(gameObject));
    }
}
