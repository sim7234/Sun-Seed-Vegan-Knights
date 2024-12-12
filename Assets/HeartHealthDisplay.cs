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
    private Coroutine hideHeartsCoroutine;

    private void Start()
    {
        health = GetComponent<Health>();
        //InvokeRepeating(nameof(UpdateDisplay), 0.1f, 0.1f);
        pastHealth = Mathf.RoundToInt(health.currentHealth);
        HideHearts();
    }

    private void UpdateDisplay()
    {
        foreach (var heart in hearts)
        {
            heart.SetActive(false);
        }

        for (int i = 0; i < Mathf.RoundToInt(health.currentHealth); i++)
        {
            if (i <= hearts.Count - 1)
            {
                hearts[i].SetActive(true);
            }
        }
    }

    public void OnTakeDamage()
    {
        if (Mathf.RoundToInt(health.currentHealth) < pastHealth)
        {
            TriggerLoseHeartEffects();
        }
        UpdateDisplay();
        pastHealth = Mathf.RoundToInt(health.currentHealth);

        if (hideHeartsCoroutine != null)
        {
            StopCoroutine(hideHeartsCoroutine);
        }
        hideHeartsCoroutine = StartCoroutine(HideHeartsAfterDelay());
    }

    public void OnHeal()
    {
        UpdateDisplay();
        pastHealth = Mathf.RoundToInt(health.currentHealth);

        if (hideHeartsCoroutine != null)
        {
            StopCoroutine(hideHeartsCoroutine);
        }
        hideHeartsCoroutine = StartCoroutine(HideHeartsAfterDelay());
    }

    private void TriggerLoseHeartEffects()
    {
        if (loseHeartPs != null)
        {
            for (int j = 0; j < pastHealth - Mathf.RoundToInt(health.currentHealth); j++)
            {
                GameObject newHeartLostPs = Instantiate(loseHeartPs, transform.position, Quaternion.identity);
                Destroy(newHeartLostPs, 0.5f);
            }
        }
    }

    private IEnumerator HideHeartsAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        HideHearts();
    }

    private void HideHearts()
    {
        foreach (var heart in hearts)
        {
            heart.SetActive(false);
        }
    }
}