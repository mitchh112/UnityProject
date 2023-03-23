using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFist : Photon.MonoBehaviour
{
    [SerializeField] private GameObject waterFistPrefab;

    // Update is called once per frame
    public void SpawnJab()
    {
            Vector3 spawnLocation;

                Quaternion currentRotation = transform.rotation; // Get the current rotation of the object
                if(currentRotation.eulerAngles.y==190f)
                {
                    spawnLocation = new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z);
                }
                else 
                {
                    spawnLocation = new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z);
                }
            
            PhotonNetwork.Instantiate(waterFistPrefab.name, spawnLocation, transform.rotation,0);
    }

    public void SpawnCross()
    {
            Vector3 spawnLocation;

            Quaternion currentRotation = transform.rotation; // Get the current rotation of the object
            if (currentRotation.eulerAngles.y == 190f)
            {
                spawnLocation = new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z);
            }
            else
            {
                spawnLocation = new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z);
            }

            PhotonNetwork.Instantiate(waterFistPrefab.name, spawnLocation, transform.rotation, 0); 
    }
}
