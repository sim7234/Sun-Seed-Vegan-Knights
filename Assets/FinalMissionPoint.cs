using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FinalMissionPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject musicBox;

    [SerializeField]
    private AudioClip musicClip;

    public void FinalMission()
    {
        musicBox.GetComponent<AudioSource>().clip = musicClip;
        musicBox.GetComponent<AudioSource>().enabled = false;
        musicBox.GetComponent<AudioSource>().enabled = true;
    }
}
