using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGroupController : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;
    public GameObject LootUpgrade;
    private List<GameObject> coinsList = new List<GameObject>();
    private List<GameObject> lootBagList = new List<GameObject>();
    private GameObject[] coins;
    private GameObject[] lootBag;


    void Awake()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
    }
    public int amountEarnedCoins = 0;
    void Update()
    {
        if (coinsList.Count == 5)
        {
                Vector3 location = FindCenterPoint(coins);

                if (location != new Vector3(0, 0, 0))
                    Instantiate(LootUpgrade, location, Quaternion.identity);

                foreach (GameObject go in coinsList)
                {
                    Destroy(go);
                }
                amountEarnedCoins += 5;
                coinsList.Clear();
                           
        }

        if (lootBagList.Count == 10)
        {

                Vector3 location = FindCenterPoint(lootBag);
                if (location != new Vector3(0, 0, 0))
                    Instantiate(LootUpgrade, location, Quaternion.identity);

                foreach (GameObject go in lootBagList)
                {
                    Destroy(go);
                }

                lootBagList.Clear();

        }
    }

    private void SetCoins(List<GameObject> coins)
    {
        float x = coins[0].transform.position.x;
        for (int i = 0; i < coins.Count; i++)
        {          
            if(coins[0].transform.position.x != coins[i].transform.position.x)
            {
                coins[i].transform.position = new Vector3(x,coins[i].transform.position.y,coins[i].transform.position.y);
            }          
        }
    }


    public void CheckForLootCoins()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");

        for (int i = 0; i < coins.Length; i++)
        {
            if (!coinsList.Contains(coins[i]))
            {
                coinsList.Add(coins[i]);
            }
        }
    }

    public void CheckForLootBags()
    {
        lootBag = GameObject.FindGameObjectsWithTag("LootBag");

        for (int i = 0; i < lootBag.Length; i++)
        {
            if (!lootBagList.Contains(lootBag[i]))
            {
                lootBagList.Add(lootBag[i]);
            }
        }
    }

    public Vector2 FindCenterPoint(GameObject[] objects)
    {
        float minX = Mathf.Infinity;
        float maxX = -Mathf.Infinity;

        // Find the minimum and maximum x-coordinates of the objects
        foreach (GameObject obj in objects)
        {
            if (obj.transform.position.x < minX)
            {
                minX = obj.transform.position.x;
            }
            if (obj.transform.position.x > maxX)
            {
                maxX = obj.transform.position.x;
            }
        }

        // Calculate the center point
        float centerX = (minX + maxX) / 2f;
        float centerY = objects[0].transform.position.y; // Assuming all objects are on the same y-axis

        return new Vector2(centerX, centerY);
    }
    
    public void DeleteCoin(GameObject coin)
    {
        RemoveFromArray(coin, coins);
       
    }

    public void RemoveFromArray(GameObject gameObject, GameObject[] gameObjects)
    {
        int index = System.Array.IndexOf(gameObjects, gameObject);
        if (index > -1)
        {
            System.Array.Copy(gameObjects, index + 1, gameObjects, index, gameObjects.Length - index - 1);
            System.Array.Resize(ref gameObjects, gameObjects.Length - 1);
        }
    }


}
