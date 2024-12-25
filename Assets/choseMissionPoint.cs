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

    [SerializeField]
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isReturnPoint)
        {
            if (missionMaster.combatOver && activeIfNull == null)
            {
                collider.enabled = true;
                foreach (SpriteRenderer sprite in sprites)
                {

                    sprite.enabled = true;

                }
            }
            else
            {
                collider.enabled = false;
                foreach (SpriteRenderer sprite in sprites)
                {

                    sprite.enabled = false;

                }
            }
        }
        else
        {
            if (missionMaster.combatOver)
            {
                collider.enabled = true;
                foreach (SpriteRenderer sprite in sprites)
                {

                    sprite.enabled = true;

                }
            }
            else
            {
                collider.enabled = false;
                foreach (SpriteRenderer sprite in sprites)
                {

                    sprite.enabled = false;

                }
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
