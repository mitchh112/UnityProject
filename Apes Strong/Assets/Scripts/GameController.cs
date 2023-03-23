using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameController : Photon.MonoBehaviour
{
    public Transform nodeTransform;
    public GameObject playerPrefab;
    public GameObject gameCanvas;
    public GameObject sceneCamera;
    public GameObject disconnectUI;
    public TMP_Text pingText;
    public TMP_Text serverNameText;
    private bool Off = false;

    private void Awake()
    {
        gameCanvas.SetActive(true);
    }

    private void Update()
    {
        //pingText.text = "Ping: " + PhotonNetwork.GetPing();
        //serverNameText.text = "Server name: " + PhotonNetwork.room.Name;
        CheckInput();
    }

    private void CheckInput()
    {
        if(Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            Off = false;
        } else if(!Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(true);
            Off = true;
        }
    }
    public void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(0 ,0), Quaternion.identity, 0);
        gameCanvas.SetActive(false);
        sceneCamera.SetActive(false);
    }

    public void LeaveRoom()
    {
        Debug.Log("Leave");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }
}
