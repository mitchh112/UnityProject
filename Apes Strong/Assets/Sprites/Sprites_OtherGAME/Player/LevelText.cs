using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;
    private PathManager playerTarget;
    private TMPro.TextMeshProUGUI text;
    private string newText = "Level: ";
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.Find("Player");
        playerTarget = player.GetComponent<PathManager>();
        text = gameObject.GetComponent<TMPro.TextMeshProUGUI>();       
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
            playerStats = player.GetComponent<PlayerStats>();
        if(playerStats!=null)
            newText = "Level: " +playerStats.level.ToString();
        if(text.text!=null)
            text.text = newText;
    }
}
