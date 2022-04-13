using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Family.Gameplay
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private void Start() 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
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

        public bool IsMoveVertical()
        {
            if (CanProcessInput())
            {
                return Input.GetButton(InputConstants.k_AxisNameVertical);
            }
            return false;
        }

        public float GetLookInputsHorizontal()
        {
            return GetMouseOrStickLookAxis(InputConstants.k_MouseAxisNameHorizontal,
                InputConstants.k_AxisNameJoystickLookHorizontal);
        }

        public float GetLookInputsVertical()
        {
            return GetMouseOrStickLookAxis(InputConstants.k_MouseAxisNameVertical,
                InputConstants.k_AxisNameJoystickLookVertical);
        }

        public float GetMouseOrStickLookAxis(string mouseInputName, string stickInputName)
        {
            if (CanProcessInput())
            {
                // Check if this look input is coming from the mouse
                bool isGamepad = Input.GetAxis(stickInputName) != 0f;
                float i = isGamepad ? Input.GetAxis(stickInputName) : Input.GetAxisRaw(mouseInputName);

                if (isGamepad)
                {
                    // since mouse input is already deltaTime-dependant, only scale input with frame time if it's coming from sticks
                    i *= Time.deltaTime;
                }
                else
                {
                    // reduce mouse input amount to be equivalent to stick movement
                    i *= 0.01f;
                }

                return i;
            }

            return 0f;
        }
    }
}