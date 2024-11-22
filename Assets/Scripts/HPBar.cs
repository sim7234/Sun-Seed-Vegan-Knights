using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;

    
    private Health health;

    public GameObject parent;
    private void Start()
    {
        health = GetComponent<Health>();
    }


    private void Update()
    {
        hpBar.value = health.GetCurrentHealth() / health.maxHealth;
    }
}
