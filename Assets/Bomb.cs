using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField]
    private GameObject hitBox;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitBox.SetActive(true);
        Destroy(gameObject, 0.5f);
    }
}
