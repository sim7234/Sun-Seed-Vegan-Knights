using System;
using UnityEngine;

public class WaterSoundController : MonoBehaviour
{

    AudioSource waterSound;
    [SerializeField] AudioClip waterSoundClip;

    [HideInInspector] public int amountToPlay;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        waterSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (amountToPlay > 0)
            playSound();
    }

    public void playSound()
    {


        if (amountToPlay == 0)
            return;


        float rnd = UnityEngine.Random.Range(0.9f, 1.1f);

        if (amountToPlay > 5)
        {
            amountToPlay = 5;

            timer += Time.deltaTime;

            if (timer > 0.1f)
            {
                waterSound.PlayOneShot(waterSoundClip);

            }
        }

        waterSound.pitch = rnd;
        waterSound.PlayOneShot(waterSoundClip);


        if (amountToPlay > 0)
        {
            timer = 0;
            amountToPlay--;
        }
            
    }


}
