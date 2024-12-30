using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAmountOfRespawns : MonoBehaviour
{
    [SerializeField]
    private int amount;

    private void Start()
    {
        SaveData.Instance.playerDeathsBeforeGameOver = amount;
    }
}
