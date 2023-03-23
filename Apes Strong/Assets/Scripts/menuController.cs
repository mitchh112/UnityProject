using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class menuController : Photon.MonoBehaviour
{
    [SerializeField] private int minNumberNickname = 3;
    [SerializeField] private int maxNumberNickname = 10;
    [SerializeField] private int minNumberRoomname = 3;
    [SerializeField] private int maxNumberRoomname = 10;
    [SerializeField] private int minNumberRoomnameJoin = 3;
    [SerializeField] private int maxNumberRoomnameJoin = 10;
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject CreateButton;
    [SerializeField] private GameObject CreateBackButton;
    [SerializeField] private GameObject JoinBackButton;
    [SerializeField] private GameObject JoinButton;
    [SerializeField] private TMP_Text UsernameInput;
    [SerializeField] private GameObject UsernameInputObject;
    [SerializeField] private TMP_Text CreateGameInput;
    [SerializeField] private GameObject CreateGameInputObject;
    [SerializeField] private TMP_Text JoinGameInput;
    [SerializeField] private GameObject JoinGameInputObject;
    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private GameObject ConfirmButton;
    [SerializeField] private GameObject CreateConfirmButton;
    [SerializeField] private GameObject JoinConfirmButton;
    [SerializeField] private SpriteSwitcher spriteSwitcher;
    [SerializeField] private Animator animator;


    private int spriteIndexer = 0;
    private Image rightArrowImage;
    private Image leftArrowImage;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
        rightArrowImage = rightArrow.GetComponent<Image>();
        leftArrowImage = leftArrow.GetComponent<Image>();       
    }

    private void Update()
    {
        if (spriteIndexer == 0)
        {
            rightArrowImage.color = Color.red;
        }

        if (spriteIndexer == 5)
        {
            leftArrowImage.color = Color.red;
        }
    }

    private void Start()
    {
        spriteSwitcher.SwitchSprite(spriteList[spriteIndexer]);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);     
        Debug.Log("Connected");
        loadingScreen.SetActive(false);
    }

    public void ChangeUserNameInput()
    {
        if(UsernameInput.text.Length >= minNumberNickname && UsernameInput.text.Length  <= maxNumberNickname)
        {
            ConfirmButton.SetActive(true);
        }
        else
        {
            ConfirmButton.SetActive(false);
        }
    }

    public void ChangeCreateRoomInput()
    {
        if (CreateGameInput.text.Length >= minNumberRoomname && CreateGameInput.text.Length <= maxNumberRoomname)
        {
            CreateConfirmButton.SetActive(true);
        }
        else
        {
            CreateConfirmButton.SetActive(false);
        }
    }

    public void ChangeJoinRoomInput()
    {
        if (JoinGameInput.text.Length >= minNumberRoomnameJoin && JoinGameInput.text.Length <= maxNumberRoomnameJoin)
        {
            JoinConfirmButton.SetActive(true);
        }
        else
        {
            JoinConfirmButton.SetActive(false);
        }
    }

    public void ConfirmNickname()
    {
        CreateButton.SetActive(true);
        JoinButton.SetActive(true);
    }

    public void CreateButtonClicked()
    {
        CreateBackButton.SetActive(true);
        CreateGameInputObject.SetActive(true);
        CreateConfirmButton.SetActive(true);
        JoinButton.SetActive(false);
    }

    public void JoinButtonClicked()
    {
        JoinBackButton.SetActive(true);
        JoinGameInputObject.SetActive(true);
        JoinConfirmButton.SetActive(true);
        JoinButton.SetActive(false);
        CreateButton.SetActive(false);
    }

    public void BackCreate()
    {
        CreateBackButton.SetActive(false);
        CreateGameInputObject.SetActive(false);
        CreateConfirmButton.SetActive(false);
        JoinButton.SetActive(true);
    }

    public void BackJoin()
    {
        JoinBackButton.SetActive(false);
        JoinGameInputObject.SetActive(false);
        JoinConfirmButton.SetActive(false);
        CreateButton.SetActive(true);
    }

    public void SetUserName()
    {
        PhotonNetwork.playerName = UsernameInput.text;
     }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainWorld");
    }   

    public void NextSprite()
    {
        if (spriteIndexer != 5)
        {
            spriteIndexer++;
            animator.SetInteger("spriteIndexer", spriteIndexer);
            spriteSwitcher.SwitchSprite(spriteList[spriteIndexer]);
            rightArrowImage.color = new Color(126, 189, 231);
            leftArrowImage.color = new Color(126, 189, 231);
        } 
    }

    public void PreviousSprite()
    {
        if (spriteIndexer != 0)
        {
            spriteIndexer--;
            spriteSwitcher.SwitchSprite(spriteList[spriteIndexer]);
            animator.SetInteger("spriteIndexer", spriteIndexer);
            rightArrowImage.color = new Color(126, 189, 231);
            leftArrowImage.color = new Color(126, 189, 231);
        } 
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                // Als we in de editor zijn, gebruik dan de knop "Stop" in de Editor om het spel af te sluiten
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Als we in een build zijn, gebruik dan Application.Quit() om het spel af te sluiten
            Application.Quit();
        #endif
    }

    public void QuickGame()
    {
        PhotonNetwork.playerName = "Quick";
        PhotonNetwork.CreateRoom("test", new RoomOptions() { MaxPlayers = 5 }, null);
    }
}
