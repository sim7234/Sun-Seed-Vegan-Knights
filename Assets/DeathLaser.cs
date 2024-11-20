using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DeathLaser : MonoBehaviour
{
    [SerializeField]
    private float damageRange;

    [SerializeField]
    private float damage;

    private NavMeshAgent agent;

    private List<GameObject> targets = new List<GameObject>();

    private float hitsperSecond;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        hitsperSecond -= Time.deltaTime;
        if (targets.Count > 0 && hitsperSecond <= 0)
        {
            hitsperSecond = 0.1f;
            agent.destination = targets[0].transform.position;
            foreach (GameObject go in targets)
            {
                if (Vector3.Distance(go.transform.position, transform.position) <= damageRange)
                {
                    go.GetComponent<Health>().TakeDamage(damage * Time.deltaTime * 10);
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targets.Contains(collision.gameObject))
        {
            targets.Remove(collision.gameObject);
        }
    }
}
