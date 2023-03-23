using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectController : MonoBehaviour
{
    LootGroupController lootGroupController;
    private GameObject coin;
    void Awake()
    {
        coin = GameObject.Find("Coin");
        GameObject lootGroupControllerGameObject = GameObject.Find("LootGroupController");
        lootGroupController = lootGroupControllerGameObject.GetComponent<LootGroupController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Monster")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    void OnMouseOver()
    {
        Transform parentTransform = gameObject.transform.parent;
        if(Input.GetMouseButton(0))
            //lootGroupController.DeleteCoin(parentTransform.gameObject);
            Destroy(parentTransform.gameObject);       
    }
}
