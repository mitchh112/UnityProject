using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : Photon.MonoBehaviour
{
    public GameObject monsterPrefab;
    public int maxAmountOfMonsters;
    private List<GameObject> monsters = new List<GameObject>();
    public float RespawnTime;
    public float platformRightCords = 8.5f;
    public float platformLeftCords = -15.25f;
    private PhotonView view;

    // Start is called before the first frame update

    void Start()
    {
        view = GetComponent<PhotonView>();
        for (int i = 0; i < maxAmountOfMonsters; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(platformLeftCords, platformRightCords), 0, 0);
            monsters.Add(PhotonNetwork.Instantiate(monsterPrefab.name, newPos, Quaternion.identity, 0));
        }
    }

    private void Update()
    {

        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].Equals(null))
            {
                monsters.Remove(monsters[i]);
                InvokeRepeating("NewMonster", RespawnTime, 0f);
            }
        }
    }

    void NewMonster()
    {
        view.RPC("NewMonster", PhotonTargets.AllViaServer,platformLeftCords, platformRightCords, monsterPrefab, monsters);
    }

    [PunRPC]
    void NewMonster(int platformLeftCords, int platformRightCords, GameObject monsterPrefab)
    {
        Vector3 newPos = new Vector3(Random.Range(platformLeftCords, platformRightCords), 0, 0);
        monsters.Add(PhotonNetwork.Instantiate(monsterPrefab.name, newPos, Quaternion.identity, 0));
    }
}
