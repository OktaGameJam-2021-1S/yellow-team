using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFrog : WalkingEnemy
{
    [SerializeField] Animator animator;
    public bool Attack;

    private void Update()
    {
        switch (walkingState)
        {
            case MovingState.MOVING:
                animator.SetBool("Walking", true);
                break;
            case MovingState.STAYING:
                animator.SetBool("Walking", false);
                break;
        }

        if(touchingPlayer == null)
        {
            animator.SetBool("Attack", false);
        }
        else
        {
            animator.SetBool("Attack", true);

        }
    }
}
