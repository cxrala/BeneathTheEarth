using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class Player : Mover {

    public SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private PlayerInput m_playerInput;
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
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_playerInput = GetComponent<PlayerInput>();
        horizontalAction = m_playerInput.actions["LeftRight"];
        verticalAction = m_playerInput.actions["UpDown"];
        menuAction = m_playerInput.actions["Menu"];
    }

    private void Update() {
        float x = horizontalAction.ReadValue<float>();
        float y = verticalAction.ReadValue<float>();

        m_animator.SetFloat("horizontal", x);
        m_animator.SetFloat("vertical", y);
        if (x != 0 || y != 0) {
            if (!moving) {
                m_animator.SetTrigger("startMove");
                moving = true;
                m_animator.SetBool("isMove", true);
            }
            if (Math.Abs(x) > Math.Abs(y)) {
                if (x < 0) {
                    m_animator.SetInteger("direction", 3);
                } else {
                    m_animator.SetInteger("direction", 1);
                }
            } else {
                if (y < 0) {
                    m_animator.SetInteger("direction", 2);
                } else {
                    m_animator.SetInteger("direction", 0);
                }
            }
        } else {
            if (moving) {
                m_animator.SetTrigger("endMove");
                moving = false;
                m_animator.SetBool("isMove", false);
            }
        };

        if (isAlive) UpdateVelocity(new Vector3(x, y, 0));
    }

    public void SwapCharacter(int characterID) {
        m_animator.SetInteger("character", characterID);
        m_animator.SetTrigger("changeCharacter");
        PlayerData.instance.characterID = characterID;
    }

    public void SetupAnimator(int characterID)
    {
        IEnumerator WaitUntilReady()
        {
            while (m_animator == null)
            {
                yield return null;
            }
            m_animator.SetInteger("character", characterID);
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
