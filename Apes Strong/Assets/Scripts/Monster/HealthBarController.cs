using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthFill;
    public GameObject monster;
    private MonsterStats monsterStats;
    private float maxHealth;
    private float currentHealth;
    private void Start()
    {
        monsterStats = monster.GetComponent<MonsterStats>();
        maxHealth = monsterStats.maxHealth;  
        healthFill = GetComponent<Image>();
    }

    private void Update()
    {
        if(monster!=null){
            monsterStats = monster.GetComponent<MonsterStats>();
            currentHealth = monsterStats.currentHealth;
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }
}
