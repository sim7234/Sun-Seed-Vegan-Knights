using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choseMissionPoint : MonoBehaviour
{
    [SerializeField]
    private MissionMaster missionMaster;

    [SerializeField]
    private GameObject missionPoint;

    [SerializeField]
    private bool isReturnPoint = false;

    [SerializeField]
    private GameObject activeIfNull;

    private Collider2D collider;

    private SpriteRenderer sprite;
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isReturnPoint)
        {
            if (missionMaster.combatOver && activeIfNull == null)
            {
                collider.enabled = true;
                sprite.enabled = true;
            }
            else
            {
                collider.enabled = false;
                sprite.enabled = false;
            }
        }
        else
        {
            if (missionMaster.combatOver)
            {
                collider.enabled = true;
                sprite.enabled = true;
            }
            else
            {
                collider.enabled = false;
                sprite.enabled = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            missionMaster.NewStageByTrigger(missionPoint);
            Destroy(gameObject);
        }
    }
}
