using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DestructiveEnvironment : MonoBehaviour
{
    [SerializeField]
    private GameObject particleEffect;

    [SerializeField]
    private Sprite otherSprite;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    [SerializeField]
    private GameObject waterDrop;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Damage>() != null)
        {
            DeathDestroy();
        }
    }

    private void DeathDestroy()
    {
        spriteRenderer.sprite = otherSprite;
        GameObject newEffect = Instantiate(particleEffect, transform.position + transform.up, Quaternion.identity);

        Destroy(newEffect, 1f);

        audioSource.pitch = Random.Range(0.90f, 1.1f);
        audioSource.Play();

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        SpawnWater();
    }
    private void SpawnWater()
    {
        if (waterDrop != null)
        {
            for (int i = 0; i < Random.Range(0, 4); i++)
            {
                Instantiate(waterDrop, transform.position, Quaternion.identity);
            }
        }
    }
}