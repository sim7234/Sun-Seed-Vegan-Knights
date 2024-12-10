using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartHealthDisplay : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> hearts = new List<GameObject>();

    private Health health;

    private int pastHealth;

    [SerializeField]
    private GameObject loseHeartPs;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    public void UpdateDisplay()
    {
        foreach (var heart in hearts)
        {
            heart.SetActive(false);
        }
        for (int i = 0; i < health.currentHealth; i++)
        {
            
            Debug.Log(i);
            hearts[i].SetActive(true);
            if (health.currentHealth != pastHealth)
            {
                if(loseHeartPs != null)
                {
                    for (int j = 0; j < pastHealth - health.currentHealth; j++)
                    {
                        GameObject newHeartlostPs = Instantiate(loseHeartPs, transform.position, Quaternion.identity);
                        Destroy(newHeartlostPs, 0.5f);
                    }
                }

                pastHealth =  Mathf.RoundToInt(health.currentHealth);
            }
        }
    }

}