using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerGlow : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer glow1;

    [SerializeField]
    private SpriteRenderer glow2;

    [SerializeField]
    private SpriteRenderer directinalTriangle;


    [SerializeField]
    private TextMeshProUGUI playerText;


    private int playerIndex;

  
    private void Start()
    {
        Invoke(nameof(GetPlayerIndex), 0.1f);
    }

    private void GetPlayerIndex()
    {
        playerIndex = GetComponent<PlayerMovement>().playerIndex;

        SetPlayerGlowByIndex();
    }

    private void SetPlayerGlowByIndex()
    {
        switch (playerIndex)
        {
            case 1:
                glow1.color = Color.green;
                glow2.color = Color.green;
                directinalTriangle.color = Color.green;
                playerText.color = Color.green;
                break;
            case 2:
                glow1.color = Color.red;
                glow2.color = Color.red;
                directinalTriangle.color = Color.red;
                playerText.color = Color.red;
                break;
            case 3:
                glow1.color = Color.cyan;
                glow2.color = Color.cyan;
                directinalTriangle.color = Color.cyan;
                playerText.color = Color.cyan;
                break;
            case 4:
                glow1.color = Color.yellow;
                glow2.color = Color.yellow;
                directinalTriangle.color = Color.yellow;
                playerText.color = Color.yellow;
                break;
            default:
                glow1.color = Color.white;
                glow2.color = Color.white;
                directinalTriangle.color = Color.white;
                playerText.color = Color.white;
                break;
        }

        playerText.SetText("Player: " + playerIndex);
    }
}
