using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Game.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }

        public event Action OnShootPressed;

        public event Action OnMeleePressed;

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            OnShootPressed?.Invoke();
        }

        public void OnMelee(InputAction.CallbackContext context)
        {
            if (!context.started)
            {
                return;
            }

            OnMeleePressed?.Invoke();
        }
    }
}

