using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private AnimationState animationState = AnimationState.NONE;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (animationState)
        {
            case AnimationState.NONE:
                animator.SetBool("isIdling", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isThrowing", false);
                break;
            case AnimationState.IDLE:
                animator.SetBool("isIdling", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isThrowing", false);
                break;
            case AnimationState.WALK:
                animator.SetBool("isIdling", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isThrowing", false);
                break;
            case AnimationState.THROW:
                animator.SetBool("isIdling", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isThrowing", true);
                break;
            default:
                break;
        }
    }
    public enum AnimationState
{
    NONE = 0,
    IDLE = 1,
    WALK = 2,
    THROW = 3
};
}
