using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomBomb : MonoBehaviour
{
    [SerializeField]
    private int playerIndex;

    [SerializeField]
    private int bloomDamage;

    [SerializeField]
    private bool turnOfOnStart;

    private void Start()
    {
        Invoke(nameof(GetPlayerIndex), 0.05f);
    }
    private void GetPlayerIndex()
    {
        playerIndex = SaveData.Instance.playerAmount;
        if(turnOfOnStart)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BloomRecipient>() != null)
        {
            other.GetComponent<BloomRecipient>().TakeBloomDamage(bloomDamage, playerIndex);
        }
    }
}