using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterController : Photon.MonoBehaviour
{
    public PhotonView photonView;
    public Animator animator;
    public PathfindingMonster pathfindingMonster;
    private Vector3 targetPosition;
    private bool isMoving = false;
    public GameObject visuals;
    private float startXPos;
    public int startPosMovingRangeX;
    public int endPosMovingRangeX;
    private float timer = 3f;
    private Rigidbody2D rb;
    public float knockbackForce = 0.5f; // stel de kracht van de knockback in


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
            Timer();

            if (isMoving)
            {
                
                pathfindingMonster.MoveMonster(targetPosition);
            

                if (transform.position.x <= targetPosition.x)
                {
                    photonView.RPC("IsRunning", PhotonTargets.AllBuffered);
                }

                if (transform.position.x >= targetPosition.x)
                {
                    photonView.RPC("IsRunning", PhotonTargets.AllBuffered);
                }
            }
    }


    void OnTriggerEnter2D(Collider2D other2D)
    {
        if (photonView.isMine)
        {
            // Voeg knockback toe aan het triggerende object (other.gameObject)           
            if (rb != null)
            {
                Vector3 direction = other2D.transform.position - transform.position; // bereken de richting van de knockback
                rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse); // voeg de knockback-kracht toe              
            }
        }
    }

    private void Timer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            GetNewTarget();
            timer = 3f;
        }
    }

    private void GetNewTarget()
    {

            int spawnX = Random.Range(startPosMovingRangeX, endPosMovingRangeX);
            targetPosition = new Vector3(spawnX, 0, 0);
            isMoving = true;

            if (transform.position.x < targetPosition.x)
            {
                photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
            }
            else if (transform.position.x > targetPosition.x)
            {
                photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
            }         
            else
            {
                photonView.RPC("IsNotRunning", PhotonTargets.AllBuffered);
            }
    }
    [PunRPC]
    public void StopRunningAnimation()
    {
        photonView.RPC("IsNotRunning", PhotonTargets.AllBuffered);
        isMoving = false;
    }
    
    public void IsHit()
    {
        if (photonView.isMine)
        {
            photonView.RPC("GetDamage", PhotonTargets.AllBuffered);
            isMoving = false;
        }
    }
    
    [PunRPC]
    public void Die()
    {
        photonView.RPC("IsDeath", PhotonTargets.AllBuffered);
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
    public void GetDamage()
    {
        if(photonView.isMine)
        {
            animator.SetBool("isHurt", true);
        }
    }

    [PunRPC]
    private void IsDeath()
    {
        animator.SetBool("isDeath", true);
    }
}
