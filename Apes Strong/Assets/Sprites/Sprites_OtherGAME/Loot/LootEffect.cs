using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEffect : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
}
