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

        void Start()
        {
        }

        void LateUpdate()
        {
            m_TargetPosition = Target.position + Vector3.up * Height - Target.forward * Distance;
            transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Speed * Time.deltaTime);
            transform.LookAt(Target.position + Offset);
        }
    }
}