using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerMonster : MonoBehaviour
{
    private float previousPositionX;
    private float previousPositionY;
    private Animator animator;
    private AnimationState currentState;
    private AnimationState previousState;
    private bool isDeath = false;
    private bool isHurt = false;
    private float attackSpeed;
    private float deathAnimationTime;
    private string monsterName;

    // States van de animatiecontroller
    public enum AnimationState
    {
        Idle,
        Walk,
        Hurt,
        Death
    }

    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sortingOrder = 10;
        string substringToRemove = "(Clone)";
        string name = transform.parent.name;
        monsterName = name.Replace(substringToRemove, "");
        GameObject player = GameObject.Find("Player");
        attackSpeed = player.GetComponent<PlayerStats>().attackSpeed;
        // Haal de Animator component op van het object waarop deze script is gehecht
        animator = GetComponent<Animator>();

        // Begin in de Idle state
        ChangeState(AnimationState.Idle);

        // Haal positie van parent op
        previousPositionX = transform.GetComponentInParent<Transform>().position.x;

    }

    void Update()
    {
        // Overgang tussen states op basis van de input van de speler
        if (isDeath)
        {
            ChangeState(AnimationState.Death);
        }
        else if (isHurt)
        {
            ChangeState(AnimationState.Hurt);
        }      
         else if (IsMovingHorizontal())
        {
            ChangeState(AnimationState.Walk);
        }
        else if (!IsMovingHorizontal())
        {
            ChangeState(AnimationState.Idle);
        }
        
    }

    // Verander de huidige state van de animatiecontroller
    private void ChangeState(AnimationState newState)
    {
        if (currentState == newState)
        {
            return;
        }

        previousState = currentState;
        currentState = newState;

        // Speel de bijbehorende animatie af
        switch (currentState)
        {
            case AnimationState.Idle:
                string animationName = "Idle-" + monsterName;
                animator.Play(animationName);
                break;

            case AnimationState.Walk:
                animationName = "Walk-" + monsterName;
                animator.Play(animationName);
                break;

            case AnimationState.Death:
                animationName = "Die-" + monsterName;
                animator.Play(animationName);
                break;

            case AnimationState.Hurt:
                animationName = "Hurt-" + monsterName;
                animator.Play(animationName);
                // Start een coroutine die na 0.5 seconden de HurtStateExit() methode zal aanroepen
                StartCoroutine(HurtStateExit(0.1f));
                break;

        }
    }

    private bool IsMovingHorizontal()
    {
        bool isMovingHorizontal = false;
        float currentPositionX = transform.GetComponentInParent<Transform>().position.x;
        if (currentPositionX != previousPositionX)
        {
            isMovingHorizontal = true;
        }

        previousPositionX = currentPositionX;
        return isMovingHorizontal;
    }

    public void SetHurt() 
    {
        isHurt = true;    
    }

    IEnumerator HurtStateExit(float time)
    {
        // Wacht de opgegeven tijd voordat we doorgaan met de rest van de coroutine
        yield return new WaitForSeconds(time);
        // Zet de isHurt variabele terug naar false
        isHurt = false;
        // Keer terug naar de vorige state
        ChangeState(previousState);
    }

    public void SetDeath()
    {
        isDeath = true;
    }

    public float GetDeathAnimationTime()
    {
        return deathAnimationTime;
    }
}
