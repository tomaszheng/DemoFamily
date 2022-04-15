using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Family.Gameplay
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator m_Animator;
        private PlayerInputHandler m_InputHandler;

        private bool m_IsMoving = false;
        private bool m_IsWalking = false;
        private bool m_IsRunning = false;
        private bool m_IsMovingHorizontal = false;
        private bool m_IsMoveBackward = false;
        private bool m_IsMoveToLeft = false;
        private bool m_IsMoveToRight = false;
        private bool m_IsSmiting = false;
        private bool m_IsSlashing = false;
        private int m_SlashIndex = 0;

        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_InputHandler = GetComponent<PlayerInputHandler>();
        }

        void Update()
        {

            Vector3 moveInput = m_InputHandler.GetMoveInput();

            m_IsRunning = m_InputHandler.GetRunHold();
            m_IsWalking = false;
            m_IsMoveBackward = false;
            m_IsSmiting = false;
            m_IsSlashing = false;
            m_SlashIndex = 0;

            if (m_InputHandler.GetMoveBackwardHold())
            {
                m_IsRunning = false;
                m_IsWalking = false;
                m_IsMoveBackward = true;
            }
            else if (m_InputHandler.GetMoveForwardHold())
            {
                m_IsWalking = m_IsRunning ? false : true;
                m_IsMoveBackward = false;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                m_IsSmiting = true;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                m_IsSlashing = true;
                m_SlashIndex = Mathf.FloorToInt(Random.Range(1f, 2.9f));
            }

            if (m_IsSmiting || m_IsSlashing) 
            {
                m_IsWalking = false;
                m_IsRunning = false;
            }

            if (m_InputHandler.IsMoveHorizontal())
            {
                m_IsMovingHorizontal = true;
                m_IsMoveToLeft = moveInput.x < 0;
                m_IsMoveToRight = moveInput.x > 0;
            }
            else
            {
                m_IsMovingHorizontal = false;
                m_IsMoveToLeft = false;
                m_IsMoveToRight = false;
            }

            m_Animator.SetBool("Walking", m_IsWalking);
            m_Animator.SetBool("Running", m_IsRunning);
            m_Animator.SetBool("Smiting", m_IsSmiting);
            m_Animator.SetBool("Slashing", m_IsSlashing);
            m_Animator.SetInteger("SlashSelector", m_SlashIndex);
        }

        public bool IsRunning()
        {
            return m_IsRunning;
        }
    }
}
