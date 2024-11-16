using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip successClip;
    public AudioClip errorClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySuccessSound()
    {
        audioSource.PlayOneShot(successClip);
    }

    public void PlayErrorSound()
    {
        audioSource.PlayOneShot(errorClip);
    }
}
