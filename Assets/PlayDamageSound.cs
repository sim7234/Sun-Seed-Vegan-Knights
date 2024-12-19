using UnityEngine;

public class PlayDamageSound : MonoBehaviour
{
    [SerializeField] AudioClip SoundOnHit;
    AudioSource saveDataAudioSource;
    void Start()
    {
        saveDataAudioSource = FindAnyObjectByType<SaveData>().GetComponentInChildren<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            saveDataAudioSource.PlayOneShot(SoundOnHit);
        }
    }
}
