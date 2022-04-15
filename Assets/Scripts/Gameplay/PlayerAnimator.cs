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

            if (m_IsWalking || m_IsRunning || m_IsSmiting || m_IsSlashing) {
                Debug.Log(string.Format("{0}, {1}, {2}, {3}", m_IsWalking, m_IsRunning, m_IsSmiting, m_IsSlashing));
            }

            m_Animator.SetBool("Walking", m_IsWalking);
            m_Animator.SetBool("Running", m_IsRunning);
            m_Animator.SetBool("Smiting", m_IsSmiting);
            m_Animator.SetBool("Slashing", m_IsSlashing);
        }

        public bool IsRunning()
        {
            return m_IsRunning;
        }
    }
}
