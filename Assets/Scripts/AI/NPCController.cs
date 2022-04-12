using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Family.AI
{
    public class NPCController : MonoBehaviour
    {
        [Header("Patrol")]
        [Tooltip("此NPC路线")]
        public PatrolRoute PatrolRoute;

        [Tooltip("NPC在路线的起始位置")]
        public int FirstPatrolRouteIndex = 0;

        public NavMeshAgent NavMeshAgent { get; private set; }

        private int m_PatrolRouteIndex = 0;
        private bool m_PatrolRouteReverse = false;

        // Start is called before the first frame update
        void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();

            if (PatrolRoute) 
            {
                m_PatrolRouteIndex = (FirstPatrolRouteIndex + 1) % PatrolRoute.RouteNodes.Count;
                GetComponent<Transform>().position = PatrolRoute.GetFirstPosition();
                SetNavDestination(PatrolRoute.GetRoutePosition(m_PatrolRouteIndex));
            }

            GetComponent<Animator>().SetBool("Walking", true);
        }

        public void SetNavDestination(Vector3 destination) 
        {
            if (NavMeshAgent) {
                NavMeshAgent.SetDestination(destination);
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdatePatrolRouteIndex();
            SetNavDestination(PatrolRoute.GetRoutePosition(m_PatrolRouteIndex));
        }

        private void UpdatePatrolRouteIndex()
        {
            if (PatrolRoute && PatrolRoute.GetDistanceToNode(transform.position, m_PatrolRouteIndex) < 2f)
            {
                if (!m_PatrolRouteReverse && m_PatrolRouteIndex == PatrolRoute.RouteNodes.Count - 1)
                {
                    m_PatrolRouteReverse = true;
                }
                else if (m_PatrolRouteReverse && m_PatrolRouteIndex == 0) 
                {
                    m_PatrolRouteReverse = false;
                }
                if (m_PatrolRouteReverse)
                {
                    m_PatrolRouteIndex = m_PatrolRouteIndex - 1;
                }
                else 
                {
                    m_PatrolRouteIndex = m_PatrolRouteIndex + 1;
                }
            }
        }
    }
}
