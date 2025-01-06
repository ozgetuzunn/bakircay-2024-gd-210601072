using UnityEngine;
using DG.Tweening;

public class FinalObjectsAnimation : MonoBehaviour
{
    public float correctScaleFactor = 1.2f;
    public float correctScaleTime = 0.3f;
    public float throwDistance = 3f;
    public float throwTime = 0.5f;
    public float disappearTime = 0.3f;

    public void PlayCorrectPlacementAnimation(System.Action onComplete = null)
    {
        Debug.Log($"FinalObjectsAnimation: {gameObject.name} PlayCorrectPlacementAnimation()");

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(transform.localScale * correctScaleFactor, correctScaleTime).SetLoops(2, LoopType.Yoyo));
        seq.OnComplete(() =>
        {
            Debug.Log($"FinalObjectsAnimation: {gameObject.name} correct animation complete.");
            onComplete?.Invoke();
        });
    }

    public void PlayIncorrectPlacementAnimation(System.Action onComplete = null)
    {
        Debug.Log($"FinalObjectsAnimation: {gameObject.name} PlayIncorrectPlacementAnimation()");

        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f)).normalized;
        Vector3 targetPos = transform.position + randomDir * throwDistance;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetPos, throwTime).SetEase(Ease.OutQuad));
        seq.Join(transform.DOScale(transform.localScale * 0.8f, throwTime));
        seq.OnComplete(() =>
        {
            Debug.Log($"FinalObjectsAnimation: {gameObject.name} incorrect animation complete.");
            onComplete?.Invoke();
        });
    }

    public void PlayDisappearAnimation()
    {
        Debug.Log($"FinalObjectsAnimation: {gameObject.name} PlayDisappearAnimation() -> Will destroy object.");

        transform.DOScale(Vector3.zero, disappearTime)
                 .OnComplete(() =>
                 {
                     Debug.Log($"FinalObjectsAnimation: {gameObject.name} destroyed.");
                     Destroy(gameObject);
                 });
    }
}
