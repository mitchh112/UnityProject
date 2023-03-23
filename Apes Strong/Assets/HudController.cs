using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HudController : MonoBehaviour
{
    [SerializeField]public TMP_Text timeText;
    [SerializeField] public TMP_Text serverNameText;
    private DateTime now;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime()
    {
        now = DateTime.Now;
        timeText.text = now.ToLongTimeString();
    }

    public void SetNameOfRoom()
    {
        if (PhotonNetwork.inRoom)
        {
            string roomName = "Server " +PhotonNetwork.room.Name;
            serverNameText.text = roomName;
        }
    }
}
