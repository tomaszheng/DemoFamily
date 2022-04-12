using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Family.AI
{
    public class PatrolRoute : MonoBehaviour
    {
        [Tooltip("巡逻路线上的点")]
        public List<Transform> RouteNodes = new List<Transform>();

        public Vector3 GetRoutePosition(int nodeIndex) 
        {
            if (!IsValidIndex(nodeIndex)) 
            {
                return Vector3.zero;
            }
            return RouteNodes[nodeIndex].position;
        }

        public Vector3 GetFirstPosition() 
        {
            return RouteNodes[0] ? RouteNodes[0].position : Vector3.zero;
        }

        public bool IsValidIndex(int nodeIndex)
        {
            return nodeIndex >= 0 && nodeIndex < RouteNodes.Count && !!RouteNodes[nodeIndex];
        }

        public float GetDistanceToNode(Vector3 src, int nodeIndex)
        {
            if (!IsValidIndex(nodeIndex))
            {
                return -1f;
            }
            return (RouteNodes[nodeIndex].position - src).magnitude;
        }
    }
}