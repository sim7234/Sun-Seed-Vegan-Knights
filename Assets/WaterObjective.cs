using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaterObjective : MonoBehaviour
{
    [SerializeField] private float maxWater = 1500f;
    private float currentWater = 0f;

    [SerializeField] private float waterDepletionRate = 5f;
    private bool isComplete = false;

    [SerializeField] private TMP_Text waterProgressText;

    private void Start()
    {
        currentWater = 250; // Initialize with starting water
        UpdateProgressUI();
        StartCoroutine(DepleteWater());
    }

    private void UpdateProgressUI()
    {
        if (waterProgressText != null)
        {
            waterProgressText.text = $"{Mathf.FloorToInt(currentWater)} / {Mathf.FloorToInt(maxWater)}";
            Debug.Log($"Display updated: {currentWater} / {maxWater}");
        }
    }

public void AddWater(float amount)
{
    if (!isComplete)
    {
        Debug.Log($"WaterObjective: Adding water. Amount: {amount}, Current before: {currentWater}");
        currentWater += amount;
        currentWater = Mathf.Clamp(currentWater, 0, maxWater);
        UpdateProgressUI();

        if (currentWater >= maxWater)
        {
            CompleteObjective();
        }
    }
}

    private void CompleteObjective()
    {
        isComplete = true;
        StopCoroutine(DepleteWater());
        Debug.Log("Objective complete!");
    }

    private IEnumerator DepleteWater()
    {
        while (!isComplete)
        {
            yield return new WaitForSeconds(1f);

            if (currentWater >= maxWater || currentWater <= 0)
            {
                continue;
            }
            currentWater -= waterDepletionRate;
            currentWater = Mathf.Max(currentWater, 0);
            UpdateProgressUI();

            if (currentWater <= 0)
            {
                break;
            }
        }
    }
}