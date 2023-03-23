using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarTarget : MonoBehaviour
{
    public GameObject hpbar;
    private MonsterStats monsterStats;
    // Start is called before the first frame update
    void Start()
    {
        
        monsterStats = GetComponent<MonsterStats>();
        hpbar.SetActive(false);
    }

    // Update is called once per frame
    public void Show()
    {
        hpbar.SetActive(true);
    }

    public void Hide()
    {
        hpbar.SetActive(false);
    }
}
