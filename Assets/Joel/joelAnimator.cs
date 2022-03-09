using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joelAnimator : MonoBehaviour
{
    private Animator animator;
    private int isWalkingHash;
    private int isRunningHash;
    private int isJumpingHash;
    private int isWalkingRightHash;
    private int isWalkingLeftHash;
    private int isWalkingBackwardsHash;
    private int isDodgingRightHash;
    private int isDodgingLeftHash;
    private int isDodgingForwardsHash;
    private int isDodgingBackwardsHash;
    private int isFiringHash;
    private int isReloadingHash;
    private int isPickingUpHash;
    private int isThrowingHash;
    private int isInjuredHash;
    private bool hasJumped = false;
    private bool hasDodgedRight = false;
    private bool hasDodgedLeft = false;
    private bool hasDodgedForwards = false;
    private bool hasDodgedBackwards = false;
    private bool hasReloaded = false;
    private bool hasPickedUp = false;
    private bool hasThrown = false;
    private bool hasBeenInjured;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isDodgingRightHash = Animator.StringToHash("isDodgingRight");
        isDodgingLeftHash = Animator.StringToHash("isDodgingLeft");
        isDodgingForwardsHash = Animator.StringToHash("isDodgingForwards");
        isDodgingBackwardsHash = Animator.StringToHash("isDodgingBackwards");
        isFiringHash = Animator.StringToHash("isFiring");
        isReloadingHash = Animator.StringToHash("isReloading");
        isPickingUpHash = Animator.StringToHash("isPickingUp");
        isThrowingHash = Animator.StringToHash("isThrowingGrenade");
        isInjuredHash = Animator.StringToHash("isInjured");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isWalkingBackwards = animator.GetBool(isWalkingBackwardsHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalkingRight = animator.GetBool(isWalkingRightHash);
        bool isWalkingLeft = animator.GetBool(isWalkingLeftHash);
        bool isDodgingRight = animator.GetBool(isDodgingRightHash);
        bool isDodgingLeft = animator.GetBool(isDodgingLeftHash);
        bool isDodgingForwards = animator.GetBool(isDodgingForwardsHash);
        bool isDodgingBackwards = animator.GetBool(isDodgingBackwardsHash);
        bool isCurrentlyFiring = animator.GetBool(isFiringHash);
        bool isCurrentlyReloading = animator.GetBool(isReloadingHash);
        bool isCurrentlyPickingUp = animator.GetBool(isPickingUpHash);
        bool isCurrentlyThrowing = animator.GetBool(isThrowingHash);
        bool isCurrentlyInjured = animator.GetBool(isInjuredHash);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool forwardPressedArrow = Input.GetKey(KeyCode.UpArrow);
        bool BackwardsPressed = Input.GetKey(KeyCode.S);
        bool BackwardsPressedArrow = Input.GetKey(KeyCode.DownArrow);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool rightPressedArrow = Input.GetKey(KeyCode.RightArrow);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool leftPressedArrow = Input.GetKey(KeyCode.LeftArrow);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetKey(KeyCode.Space);
        bool dodgePressed = Input.GetKey(KeyCode.LeftControl);
        bool isFiringPressed = Input.GetKey(KeyCode.Mouse0);
        bool isReloadingPressed = Input.GetKey(KeyCode.R);
        bool isPickingUpPressed = Input.GetKey(KeyCode.E);
        bool isThrowingPressed = Input.GetKey(KeyCode.Mouse1);

        //detect if the player is walking forwards
        if (!isWalking & (forwardPressed || forwardPressedArrow))
            animator.SetBool(isWalkingHash, true);
        //detect is the played was walking and stopped walking
        if (isWalking & (!forwardPressed && !forwardPressedArrow))
            animator.SetBool(isWalkingHash, false);
        //detect if player was walking and pressed left shift
        if (!isRunning & ((forwardPressed || forwardPressedArrow) && runPressed))
            animator.SetBool(isRunningHash, true);
        //detect if the player stops running or stops walking
        if (isRunning & ((!forwardPressed && !forwardPressedArrow) || !runPressed))
            animator.SetBool(isRunningHash, false);
        //detect if the player is walking backwards
        if (!isWalkingBackwards & (BackwardsPressed || BackwardsPressedArrow))
            animator.SetBool(isWalkingBackwardsHash, true);
        //detect is the played was walking backwards and stopped walking
        if (isWalkingBackwards & (!BackwardsPressed && !BackwardsPressedArrow))
            animator.SetBool(isWalkingBackwardsHash, false);
        //detect if player was walking backwards and pressed left shift
        if (!isRunning & ((BackwardsPressed || BackwardsPressedArrow) && runPressed))
            animator.SetBool(isRunningHash, true);
        //detect if the player stops running or stops walking backwards
        if (isRunning & ((!BackwardsPressed && !BackwardsPressedArrow) || !runPressed))
            animator.SetBool(isRunningHash, false);
        //detect if the player is walking to the right
        if (!isWalkingRight & (rightPressed || rightPressedArrow))
            animator.SetBool(isWalkingRightHash, true);
        //detect is the player was walking right and stopped walking right
        if (isWalkingRight & (!rightPressed && !rightPressedArrow))
            animator.SetBool(isWalkingRightHash, false);
        //detect if the player is walking to the left
        if (!isWalkingLeft & (leftPressed || leftPressedArrow))
            animator.SetBool(isWalkingLeftHash, true);
        //detect is the player was walking left and stopped walking left
        if (isWalkingLeft & (!leftPressed && !leftPressedArrow))
            animator.SetBool(isWalkingLeftHash, false);
        //detect if the player was walking right and dodged
        if(!isDodgingRight & (rightPressed || rightPressedArrow) & dodgePressed)
        {
            animator.SetBool(isDodgingRightHash, true);
            StartCoroutine(goingToDodgeRight());
        }
        //detect if the player was walking left and dodged
        if (!isDodgingLeft & (leftPressed || leftPressedArrow) & dodgePressed)
        {
            animator.SetBool(isDodgingLeftHash, true);
            StartCoroutine(goingToDodgeLeft());
        }
        //detect if the player was walking forwards and dodged
        if (!isDodgingForwards & ((forwardPressed || forwardPressedArrow) & dodgePressed))
        {
            animator.SetBool(isDodgingForwardsHash, true);
            StartCoroutine(goingToDodgeForwards());
        }
        //detect if the player was walking backwards and dodged
        if (!isDodgingBackwards & (BackwardsPressed || BackwardsPressedArrow) & dodgePressed)
        {
            animator.SetBool(isDodgingBackwardsHash, true);
            StartCoroutine(goingToDodgeBackwards());
        }
        if (hasDodgedRight == true)
        {
            animator.SetBool(isDodgingRightHash, false);
            hasDodgedRight = false;
        }
        if (hasDodgedLeft == true)
        {
            animator.SetBool(isDodgingLeftHash, false);
            hasDodgedLeft = false;
        }
        if (hasDodgedForwards == true)
        {
            animator.SetBool(isDodgingForwardsHash, false);
            hasDodgedForwards = false;
        }
        if (hasDodgedBackwards == true)
        {
            animator.SetBool(isDodgingBackwardsHash, false);
            hasDodgedBackwards = false;
        }
        //detect if the player pressed jump
        if (jumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            StartCoroutine(goingToJump());
        }
        if (hasJumped == true)
        {
            animator.SetBool(isJumpingHash, false);
            hasJumped = false;
        }
        if(!isCurrentlyFiring & isFiringPressed)
        {
            animator.SetBool(isFiringHash, true);
        }
        if(isCurrentlyFiring & !isFiringPressed)
        {
            animator.SetBool(isFiringHash, false);
        }
        if(!isCurrentlyReloading & isReloadingPressed)
        {
            animator.SetBool(isReloadingHash, true);
            StartCoroutine(goingToReload());
        }
        if (hasReloaded == true)
        {
            animator.SetBool(isReloadingHash, false);
            hasReloaded = false;
        }
        if(!isCurrentlyPickingUp & isPickingUpPressed)
        {
            animator.SetBool(isPickingUpHash, true);
            StartCoroutine(goingToPickUp());
        }
        if (hasPickedUp == true)
        {
            animator.SetBool(isPickingUpHash, false);
            hasPickedUp = false;
        }
        if (!isCurrentlyThrowing & isThrowingPressed)
        {
            animator.SetBool(isThrowingHash, true);
            StartCoroutine(goingToThrow());
        }
        if (hasThrown == true)
        {
            animator.SetBool(isThrowingHash, false);
            hasThrown = false;
        }
    }
    IEnumerator goingToJump()
    {
        yield return new WaitForSeconds(0.5f);
        hasJumped = true;
    }
    IEnumerator goingToDodgeRight()
    {
        yield return new WaitForSeconds(0.5f);
        hasDodgedRight = true;
    }
    IEnumerator goingToDodgeLeft()
    {
        yield return new WaitForSeconds(0.5f);
        hasDodgedLeft = true;
    }
    IEnumerator goingToDodgeForwards()
    {
        yield return new WaitForSeconds(0.5f);
        hasDodgedForwards = true;
    }
    IEnumerator goingToDodgeBackwards()
    {
        yield return new WaitForSeconds(0.5f);
        hasDodgedBackwards = true;
    }
    IEnumerator goingToReload()
    {
        yield return new WaitForSeconds(1f);
        hasReloaded = true;
    }
    IEnumerator goingToPickUp()
    {
        yield return new WaitForSeconds(1.5f);
        hasPickedUp = true;
    }
    IEnumerator goingToThrow()
    {
        yield return new WaitForSeconds(3f);
        hasThrown = true;
    }
    IEnumerator goingToInjur()
    {
        yield return new WaitForSeconds(0.3f);
        hasBeenInjured = true;
    }
    public void dying()
    {
        animator.Play("Death");
    }
    public void injured()
    {
        if (!animator.GetBool(isInjuredHash))
        {
            animator.SetBool(isInjuredHash, true);
            StartCoroutine(goingToInjur());
        }
        if (hasBeenInjured == true)
        {
            animator.SetBool(isInjuredHash, false);
            hasBeenInjured = false;
        }
    }

}
