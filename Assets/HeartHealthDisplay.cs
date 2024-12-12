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

        for (int i = 0; i < health.currentHealth; i++)
        {
            if (i <= hearts.Count - 1)
            {
                hearts[i].SetActive(true);
                if (health.currentHealth < pastHealth)
                {
                    if (loseHeartPs != null)
                    {
                        for (int j = 0; j < pastHealth - health.currentHealth; j++)
                        {
                            GameObject newHeartlostPs = Instantiate(loseHeartPs, transform.position, Quaternion.identity);
                            Destroy(newHeartlostPs, 0.5f);
                        }
                    }
                }
            }
        }
        pastHealth = Mathf.RoundToInt(health.currentHealth);
    }

    public void OnTakeDamage()
    {
        UpdateDisplay();
        if (hideHeartsCoroutine != null)
        {
            StopCoroutine(hideHeartsCoroutine);
        }
        hideHeartsCoroutine = StartCoroutine(HideHeartsAfterDelay());
    }

    public void OnHeal()
    {
        UpdateDisplay();
        if (hideHeartsCoroutine != null)
        {
            StopCoroutine(hideHeartsCoroutine);
        }
        hideHeartsCoroutine = StartCoroutine(HideHeartsAfterDelay());
    }

    private IEnumerator HideHeartsAfterDelay()
    {
        yield return new WaitForSeconds(3f);
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