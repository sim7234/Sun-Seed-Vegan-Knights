using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnTowardsTarget : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;


    private Pathfinding pathfinding;

    [SerializeField] public bool canFlip;

    private void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        canFlip = true;
    }
    private void Update()
    {
        if (canFlip)
        {
            if ((transform.position - pathfinding.target[pathfinding.finalTarget].transform.position).x < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }  
    }
}
