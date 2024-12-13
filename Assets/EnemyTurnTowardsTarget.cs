using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnTowardsTarget : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;


    private Pathfinding pathfinding;

    private void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }
    private void Update()
    {
        if((transform.position - pathfinding.target[pathfinding.finalTarget].transform.position).x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
