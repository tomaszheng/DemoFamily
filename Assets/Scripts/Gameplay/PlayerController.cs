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

        [Header("Gravity")]
        [Tooltip("Player掉落时所受到的重力")]
        public float Gravity = -10f;
        [Tooltip("Player掉落时下落的最大速度")]
        public float MaxFallSpeed = 10f;

        [Header("Speed")]
        [Tooltip("Player移动速度")]
        public float MoveSpeed = 2f;
        [Tooltip("Player跑步速度")]
        public float RunSpeed = 4f;

        private PlayerInputHandler m_InputHandler;
        private PlayerAnimator m_PlayerAnimator;
        private CharacterController m_CharacterController;

        private float m_FallSpeed = 0f;

        void Start()
        {
            m_InputHandler = GetComponent<PlayerInputHandler>();
            m_PlayerAnimator = GetComponent<PlayerAnimator>();
            m_CharacterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            UpdateRotation();
            UpdateCharacterController();
        }

        public void UpdateRotation()
        {
            if (!m_InputHandler.GetFreeLookHold()) {
                float angle = m_InputHandler.GetLookInputsHorizontal() * RotationSpeed;
                transform.Rotate(new Vector3(0f, angle, 0f), Space.Self);
            }
        }

        public void UpdateCharacterController()
        {
            Vector3 moveInput = m_InputHandler.GetMoveInput();
            Vector3 worldMoveInput = transform.TransformVector(moveInput).normalized;
            float speed = m_PlayerAnimator.IsRunning() ? RunSpeed : MoveSpeed;
            m_CharacterController.Move(worldMoveInput * speed * Time.deltaTime);

            if (m_CharacterController.collisionFlags != CollisionFlags.Below)
            {
                m_FallSpeed += Gravity * Time.deltaTime;
                m_FallSpeed = Mathf.Min(m_FallSpeed, MaxFallSpeed);
                m_CharacterController.Move(transform.up * m_FallSpeed);
            }
            else
            {
                m_FallSpeed = 0f;
            }
        }
    }
}
