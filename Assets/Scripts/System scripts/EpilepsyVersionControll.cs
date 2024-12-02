using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpilepsyVersionControll : MonoBehaviour
{
    [SerializeField]
    private GameObject NormalVersion;

    [SerializeField]
    private GameObject EpilepsyVersion;

    private void Start()
    {
        if (SaveData.Instance.epelepticFilterOn)
        {
            EpilepsyVersion.SetActive(true);
            NormalVersion.SetActive(false);
        }
        else
        {
            EpilepsyVersion.SetActive(false);
            NormalVersion.SetActive(true);
        }
    }
}
