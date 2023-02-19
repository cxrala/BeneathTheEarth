using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class Player : Mover {

    public SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerInput playerInput;
    private InputAction horizontalAction;
    private InputAction verticalAction;
    private InputAction menuAction;
    private bool moving;

    // Arki - 0
    // Hila - 1
    // Chonk - 2
    // Konkon - 3
    public bool isAlive = true;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        horizontalAction = playerInput.actions["LeftRight"];
        verticalAction = playerInput.actions["UpDown"];
        menuAction = playerInput.actions["Menu"];
    }

    private void Update() {
        float x = horizontalAction.ReadValue<float>();
        float y = verticalAction.ReadValue<float>();

        animator.SetFloat("horizontal", x);
        animator.SetFloat("vertical", y);
        if (x != 0 || y != 0) {
            if (!moving) {
                animator.SetTrigger("startMove");
                moving = true;
                animator.SetBool("isMove", true);
            }
            if (Math.Abs(x) > Math.Abs(y)) {
                if (x < 0) {
                    animator.SetInteger("direction", 3);
                } else {
                    animator.SetInteger("direction", 1);
                }
            } else {
                if (y < 0) {
                    animator.SetInteger("direction", 2);
                } else {
                    animator.SetInteger("direction", 0);
                }
            }
        } else {
            if (moving) {
                animator.SetTrigger("endMove");
                moving = false;
                animator.SetBool("isMove", false);
            }
        };

        if (isAlive) UpdateVelocity(new Vector3(x, y, 0));
    }

    public void SwapCharacter(int characterID) {
        animator.SetInteger("character", characterID);
        animator.SetTrigger("changeCharacter");
        PlayerData.instance.characterID = characterID;
    }

    public void SetupAnimator(int characterID)
    {
        IEnumerator WaitUntilReady()
        {
            while (animator == null)
            {
                yield return null;
            }
            animator.SetInteger("character", characterID);
        }
        StartCoroutine(WaitUntilReady());
    }

    public int GetCharacterID() {
        return PlayerData.instance.characterID;
    }

    public void Respawn() {
        PlayerData.instance.Respawn();
    }

    public void StopPlayerMovement()
    {
        this.ySpeed = 0.0f;
        this.xSpeed = 0.0f;
    }

    public void ResumePlayerMovement()
    {
        this.ySpeed = 0.75f;
        this.xSpeed = 1.0f;
    }
}
