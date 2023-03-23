using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int attackDamage = 3;
    public float attackSpeed = 2;
    public float attackRange = 1;
    public int currentExperience = 0;
    public List<int> expPerLevel;
    public int level = 0;

    void Update()
    {
        if(currentExperience >= expPerLevel[level])
        {
            currentExperience = 0;
            level++;
        }
    }

}
