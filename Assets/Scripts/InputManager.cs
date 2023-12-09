using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput input;
    private PlayerMovement player;
    public Vector2 moveVector;

    private void Awake () {
        input = new PlayerInput();
        player = GetComponent<PlayerMovement>();
    }
    private void OnEnable() {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Jump.performed += OnJumpPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void onDisable() {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    }

     private void OnJumpPerformed (InputAction.CallbackContext value) {
        player.Jump();
    }

    private void OnMovementPerformed (InputAction.CallbackContext value) {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCanceled (InputAction.CallbackContext value) {
        moveVector = value.ReadValue<Vector2>();
    }
    
}
