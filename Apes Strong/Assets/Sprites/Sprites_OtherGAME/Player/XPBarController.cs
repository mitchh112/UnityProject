using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XPBarController : MonoBehaviour
{
    public Image XPFill;
    public GameObject player;
    private PlayerStats playerStats;
    private float maxXP;
    private float currentHealth;
    private void Start()
    {
        player = GameObject.Find("Player");
        XPFill = GetComponent<Image>();
    }

    private void Update()
    {
        playerStats = player.GetComponent<PlayerStats>();
        maxXP = playerStats.expPerLevel[playerStats.level];
        XPFill.fillAmount = playerStats.currentExperience / maxXP;
    }
}
