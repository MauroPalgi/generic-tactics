using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;


    [SerializeField] private bool move = false;
    [SerializeField] private bool attack = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StarMoving()
    {
        move = true;
        animator.SetBool("isWalking", true);
    }

    public void StopMoving()
    {
        move = false;
        animator.SetBool("isWalking", false);
    }
    public void StartAttacking()
    {
        attack = true;
        animator.SetBool("isAttacking", true);
    }
    public void StopAttacking()
    {
        attack = false;
        animator.SetBool("isAttacking", false);
    }

    private void Update()
    {

        animator.SetBool("isWalking", move);
        animator.SetBool("isAttacking", attack);
    }

    private void LateUpdate()
    {
        if (attack == true)
        {
            attack = false;
            animator.SetBool("isAttacking", false);

        }
    }
}
