using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DeathLaser : MonoBehaviour
{
    private float damageRange = 2;

    private float damage = 100;

    private NavMeshAgent agent;

    [SerializeField]
    private List<GameObject> targets = new List<GameObject>();

    private float hitsperSecond;

    [SerializeField]
    private GameObject NormalVfx;
    [SerializeField]
    private GameObject EpelepticFreindlyVerison;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(SaveData.Instance.epelepticFilterOn)
        {
            EpelepticFreindlyVerison.SetActive(true);
            NormalVfx.SetActive(false);
        }
        else
        {
            EpelepticFreindlyVerison.SetActive(false);
            NormalVfx.SetActive(true);

        }
    }
    private void Update()
    {
        hitsperSecond -= Time.deltaTime;
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    targets.RemoveAt(i);
                }
            }
            //agent.destination = targets[0].transform.position;
            if (hitsperSecond <= 0)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    if (Vector3.Distance(targets[i].transform.position, transform.position) <= damageRange)
                    {
                        if (targets[i].GetComponent<Health>() != null)
                        targets[i].GetComponent<Health>().TakeDamage(damage * Time.deltaTime * 10);
                    }
                }
                hitsperSecond = 0.1f;
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
