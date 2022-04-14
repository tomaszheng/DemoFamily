using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Family.Gameplay
{
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField]
        public Transform Target;

        [Header("Follow Info")]
        [Tooltip("Camera距离观察目标的高度")]
        public float Height = 2f;
        [Tooltip("Camera距离观察目标的距离")]
        public float Distance = 3f;
        [Tooltip("Camera跟随速度")]
        public float Speed = 2f;
        [Tooltip("看向目标的偏移值")]
        public Vector3 Offset = new Vector3(0f, 0f, 0f);

        private Vector3 m_TargetPosition;
        private PlayerInputHandler m_InputHandler;

        void Start()
        {
            m_InputHandler = Target.GetComponent<PlayerInputHandler>();
        }

        void LateUpdate()
        {
            if (!m_InputHandler.GetFreeLookHold()) {
                m_TargetPosition = Target.position + Vector3.up * Height - Target.forward * Distance;
                transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Speed * Time.deltaTime);
            }

            FreeLook();

            transform.LookAt(Target.position + Offset);
        }

        void FreeLook()
        {
            if (!m_InputHandler.GetFreeLookHold())
            {
                return;
            }

            float y = m_InputHandler.GetLookInputsHorizontal();
            float angleY = y * 900f;
            float x = m_InputHandler.GetLookInputsVertical();
            float angleX = x * 900f;

            transform.RotateAround(Target.position, transform.up, angleY);
            if (Mathf.Abs(angleX) < 0.00001)
            {
                transform.RotateAround(Target.position, transform.right, angleX);
            }
        }
    }
}