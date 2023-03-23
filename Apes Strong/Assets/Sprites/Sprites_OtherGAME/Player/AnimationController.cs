using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public int test = 3;
    private float previousPositionX;
    private float previousPositionY;
    private Animator animator;
    private Animator monsterAnimator;
    public GameObject monster;
    private PlayerStats playerStats;
    private MonsterStats monsterstats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        previousPositionX = transform.GetComponentInParent<Transform>().position.x;
        previousPositionY = transform.GetComponentInParent<Transform>().position.y;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(monster!=null)
        {
            monsterAnimator = monster.GetComponentInChildren<Animator>();
        }
        

        IsMovingVertical();
        IsMovingHorizontal();
        UpdateHealth();
    }

    public void StartAttackAnimationPlayer()
    {
        animator.SetTrigger("Attack");
    }

    public void StopAttackAnimationPlayer()
    {
        animator.ResetTrigger("Attack");
    }

    private void UpdateHealth()
    {
        if (monster != null)
        {
            monsterstats = monster.GetComponentInParent<MonsterStats>();
        }
    }

    private void IsMovingHorizontal()
    {
        float currentPositionX = transform.GetComponentInParent<Transform>().position.x;
        if (currentPositionX != previousPositionX)
        {
            if(!animator.GetBool("Walking"))
                animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }      
        previousPositionX = currentPositionX;
    }

    private void IsMovingVertical()
    {
        float currentPositionY = transform.GetComponentInParent<Transform>().position.y;
        if (currentPositionY != previousPositionY)
        {
            animator.SetBool("Climbing", true);
        }
        else
        {
            animator.SetBool("Climbing", false);
        }        
        previousPositionY = currentPositionY;
    }
}
