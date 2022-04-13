using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Family.Gameplay
{
    public class PlayerController : MonoBehaviour
    {

        [Header("Rotation")]
        [Tooltip("Rotation speed for moving the camera")]
        public float RotationSpeed = 200f;

        public float Gravity = -10f;

        public float RotationMultiplier
        {
            get
            {
                return 1f;
            }
        }

        private Animator m_Animator;
        private PlayerInputHandler m_InputHandler;
        private CharacterController m_CharacterController;

        private bool m_IsMoving = false;
        private bool m_IsMovingHorizontal = false;
        private bool m_IsMoveToBackward = false;
        private bool m_IsMoveToLeft = false;
        private bool m_IsMoveToRight = false;

        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_InputHandler = GetComponent<PlayerInputHandler>();
            m_CharacterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            UpdateAnimator();
            UpdateRotation();
            UpdateCharacterController();
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

        public void UpdateRotation()
        {
            transform.Rotate(
                    new Vector3(0f, (m_InputHandler.GetLookInputsHorizontal() * RotationSpeed * RotationMultiplier),
                        0f), Space.Self);
        }

        public void UpdateCharacterController()
        {
            Vector3 moveInput = m_InputHandler.GetMoveInput();
            Vector3 worldMoveInput = transform.TransformVector(moveInput);
            m_CharacterController.Move(worldMoveInput * Time.deltaTime);

            if (m_CharacterController.collisionFlags != CollisionFlags.Below)
            {
                m_CharacterController.Move(transform.up * Gravity * Time.deltaTime);
            }
        }
    }
}
