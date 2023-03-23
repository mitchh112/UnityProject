using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerController : Photon.MonoBehaviour
{
    public PhotonView photonView;
    public GameObject playerCamera;
    public Animator animator;
    public  TMP_Text playerNameText;
    public Pathfinding pathfinding;
    private Vector3 targetPosition;
    private bool isMoving = false;
    public GameObject visuals;

    public void Awake()
    {
        if(photonView.isMine)
        {         
            playerCamera.SetActive(true);
            playerNameText.text = PhotonNetwork.playerName;
        } else {
            playerNameText.text = photonView.owner.NickName;
            playerNameText.color = Color.cyan;
        }
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        if (photonView.isMine)
        {           
            if(Input.GetKeyDown(KeyCode.Q) && !isMoving)
            {
                
                photonView.RPC("IsAttacking", PhotonTargets.AllBuffered);
                //animator.SetTrigger("attack-0");            
            }

            if (Input.GetMouseButtonDown(0))
            {             
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);                           
                isMoving = true;
                if (transform.position.x < targetPosition.x)
                {
                    photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
                }
                else if (transform.position.x > targetPosition.x)
                {
                    photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
                }
                
            }

            if (isMoving)
            {
                pathfinding.MovePlayer(targetPosition);

                if (transform.position.x <= targetPosition.x)
                {
                    photonView.RPC("IsRunning", PhotonTargets.AllBuffered);
                    //animator.SetBool("isRunning", true);
                }
                if (transform.position.x >= targetPosition.x)
                {
                    photonView.RPC("IsRunning", PhotonTargets.AllBuffered);        
                    //animator.SetBool("isRunning", true);
                }
            } else {
                photonView.RPC("IsNotRunning", PhotonTargets.AllBuffered);
                //animator.SetBool("isRunning", false);
            }
        }       
    }
    
    public void StopRunningAnimation()
    {
        photonView.RPC("IsNotRunning", PhotonTargets.AllBuffered);
        isMoving = false;
    }

    [PunRPC]
    private void FlipTrue()
    {
        Quaternion currentRotation = transform.rotation; // Get the current rotation of the object
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, 190f, currentRotation.eulerAngles.z); // Create a new rotation with flipped Z rotation
        visuals.transform.rotation = newRotation;
    }
    [PunRPC]
    private void FlipFalse()
    {
        Quaternion currentRotation = transform.rotation; // Get the current rotation of the object
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, 0f, currentRotation.eulerAngles.z); // Create a new rotation with flipped Z rotation
        visuals.transform.rotation = newRotation;
    }
    [PunRPC]
    private void IsRunning()
    {
        animator.SetBool("isRunning", true);
    }
    [PunRPC]
    private void IsNotRunning()
    {
        animator.SetBool("isRunning", false);
    }
    [PunRPC]
    private void IsAttacking()
    {
        animator.SetTrigger("attack-0");
    }
}
