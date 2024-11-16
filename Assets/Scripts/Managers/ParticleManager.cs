using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem successParticles;
    public ParticleSystem errorParticles; // Hata partikül sistemi

    public void PlaySuccessParticles(Vector3 position)
    {
        successParticles.transform.position = position;
        successParticles.Play();
    }

    public void PlayErrorParticles(Vector3 position)
    {
        if (errorParticles != null) // Hata partikül sistemi atanmış mı kontrol edelim
        {
            errorParticles.transform.position = position;
            errorParticles.Play();
        }
        else
        {
            Debug.LogWarning("Error particles are not assigned in ParticleManager!");
        }
    }
}
