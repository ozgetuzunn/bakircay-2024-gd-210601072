// ParticleManager.cs
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem successParticles;
    public ParticleSystem errorParticles;

    public void PlaySuccessParticles(Vector3 position)
    {
        if (successParticles != null)
        {
            successParticles.transform.position = position;
            successParticles.Play();
        }
    }

    public void PlayErrorParticles(Vector3 position)
    {
        if (errorParticles != null)
        {
            errorParticles.transform.position = position;
            errorParticles.Play();
        }
    }
}
