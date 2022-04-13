using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Family.Gameplay
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public bool CanProcessInput()
        {
            return true;
        }

        public Vector3 GetMoveInput()
        {
            if (CanProcessInput()) 
            {
                float x = Input.GetAxisRaw(InputConstants.k_AxisNameHorizontal);
                float z = Input.GetAxisRaw(InputConstants.k_AxisNameVertical);
                Vector3 move = new Vector3(x, 0f, z);
                move = Vector3.ClampMagnitude(move, 1);
                return move;
            }
            return Vector3.zero;
        }

        public bool IsMoveHorizontal()
        {
            if (CanProcessInput())
            {
                return Input.GetButton(InputConstants.k_AxisNameHorizontal);
            }
            return false;
        }

        public bool IsMoveHorizontalUp()
        {
            if (CanProcessInput())
            {
                return Input.GetButtonUp(InputConstants.k_AxisNameHorizontal);
            }
            return false;
        }

        public bool IsMoveVertical()
        {
            if (CanProcessInput())
            {
                return Input.GetButton(InputConstants.k_AxisNameVertical);
            }
            return false;
        }

        public bool IsMoveVerticalUp()
        {
            if (CanProcessInput())
            {
                return Input.GetButtonUp(InputConstants.k_AxisNameVertical);
            }
            return false;
        }
    }
}