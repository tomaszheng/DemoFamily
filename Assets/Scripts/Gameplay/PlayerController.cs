using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Family.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        private Animator m_Animator;
        private PlayerInputHandler m_InputHandler;
        private CharacterController m_CharacterController;

        private bool m_IsMoving = false;
        private bool m_IsMovingHorizontal = false;
        private bool m_IsMoveToBackward = false;
        private bool m_IsMoveToLeft = false;
        private bool m_IsMoveToRight = false;

        // Start is called before the first frame update
        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_InputHandler = GetComponent<PlayerInputHandler>();
            m_CharacterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();

            Vector3 moveInput = m_InputHandler.GetMoveInput();
            Vector3 worldMoveInput = transform.TransformVector(moveInput);
            m_CharacterController.Move(worldMoveInput * Time.deltaTime);
        }

        public void UpdateAnimator()
        {
            Vector3 moveInput = m_InputHandler.GetMoveInput();

            if (m_InputHandler.IsMoveVertical())
            {
                m_IsMoving = true;
                m_IsMoveToBackward = moveInput.z < 0;
            }
            else
            {
                m_IsMoving = false;
                m_IsMoveToBackward = false;
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

            m_Animator.SetBool("Walking", m_IsMoving || m_IsMovingHorizontal);
            m_Animator.SetBool("Backward", m_IsMoveToBackward);
            m_Animator.SetBool("ToLeft", m_IsMoveToLeft);
            m_Animator.SetBool("ToRight", m_IsMoveToRight);
        }
    }
}