using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private KeyCode moveLeft = KeyCode.A;
    private KeyCode moveRight = KeyCode.D;
    private KeyCode moveUp = KeyCode.W;
    private KeyCode moveDown = KeyCode.S;

    // Animations
    private AnimationState animationState = AnimationState.NONE;
    private Animator animator;
    public enum AnimationState
    {
        NONE = 0,
        IDLE = 1,
        WALK = 2
    };

    void Start()
    {
        animationState = AnimationState.IDLE;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimator();
    }
    void PlayerAnimator()
    {
        if (Input.GetKey(moveLeft) || Input.GetKey(moveRight) || Input.GetKey(moveDown) || Input.GetKey(moveUp) ) 
        {
            animationState = AnimationState.WALK;
        }
        else
        {
            animationState = AnimationState.IDLE;
        }

        switch (animationState)
        {
            case AnimationState.NONE:
                animator.SetBool("isIdling", false);
                animator.SetBool("isWalking", false);
                break;
            case AnimationState.IDLE:
                animator.SetBool("isIdling", true);
                animator.SetBool("isWalking", false);
                break;
            case AnimationState.WALK:
                animator.SetBool("isIdling", false);
                animator.SetBool("isWalking", true);
                break;
            default:
                break;
        }
    }
}
