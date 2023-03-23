using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterStats : MonoBehaviour
{
    private FloatingText myText;
    public GameObject loot;
    public GameObject healthBarCanvas;
    public PathManager player;
    public int maxHealth = 100;
    public int currentHealth;
    public int exp = 3;
    private PlayerStats playerStats;
    public float moveTimer = 6f;
    public GameObject myPrefab;
    private AnimationControllerMonster animationControllerMonster;
    Coroutine hurt;
    void Start()
    {
        currentHealth = maxHealth;
        GameObject gameObjectlootGroupController = GameObject.Find("LootGroupController");
        myText = myPrefab.GetComponent<FloatingText>();
        animationControllerMonster = GetComponentInChildren<AnimationControllerMonster>();
    }

    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
        GameObject txt = Instantiate(myPrefab, newPos, Quaternion.identity);
        myText = txt.GetComponent<FloatingText>();
        myText.SetText(damage.ToString());     
        animationControllerMonster.SetHurt();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        DropLoot();                 
        playerStats = player.GetComponent<PlayerStats>();
        playerStats.currentExperience += exp;
        PathManager pathManager = player.GetComponent<PathManager>();
        pathManager.distanceX = 10f;
        player.targetMonster = null;
        Destroy(gameObject.transform.root.gameObject, 0.7f);           
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            animationControllerMonster.SetDeath();
        }

        //GameObject playerObject = GameObject.Find("Player");
        //player = playerObject.GetComponent<PathManager>();

        if (player.GetComponent<PathManager>().targetMonster == gameObject)
        {
            // Player is targeting this monster, set the HealthBarCanvas to active
            healthBarCanvas.SetActive(true);
            SpriteRenderer spriteRenderer = player.GetComponent<PathManager>().targetMonster.GetComponentInChildren<SpriteRenderer>();
            // Stel de sorting layer order in
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = 10;
            }
        }
        else
        {
            // Player is not targeting this monster, set the HealthBarCanvas to inactive
            healthBarCanvas.SetActive(false);
        }
    }

    void DropLoot()
    {
        Vector3 location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.z + 3.5f, gameObject.transform.position.y);
        
        float randomNumber = Random.Range(0, 1);
        if(randomNumber==0)
            location.x -= 1;
        else
            location.x += 1;

        GameObject dropedItem = Instantiate(loot, location, Quaternion.identity);
    }
}
