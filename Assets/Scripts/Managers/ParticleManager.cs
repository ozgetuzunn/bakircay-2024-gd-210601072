using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem successParticles;

    public void PlaySuccessParticles(Vector3 position)
    {
        successParticles.transform.position = position;
        successParticles.Play();
    }
}
