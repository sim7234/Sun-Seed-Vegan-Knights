using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitExplode : MonoBehaviour
{

    AudioSource audioSource;

    [SerializeField] AudioClip explosionAudioClip;

    private void Start()
    {
        audioSource = FindAnyObjectByType<SaveData>().GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            audioSource.PlayOneShot(explosionAudioClip);
            Destroy(gameObject);
        }
    }
}
