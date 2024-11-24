using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomBomb : MonoBehaviour
{

    private int playerIndex;

    [SerializeField]
    private int bloomDamage;

    private void Start()
    {
        Invoke(nameof(GetPlayerIndex), 0.1f);
    }

    private void GetPlayerIndex()
    {
        playerIndex = SaveData.Instance.playerAmount;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BloomRecipient>() != null)
        {
            other.GetComponent<BloomRecipient>().TakeBloomDamage(bloomDamage, playerIndex);
        }
    }

}
