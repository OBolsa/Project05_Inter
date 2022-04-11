using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Character Movement")]
    public float characterSpeed;
    public bool IsMoving { get { return inputDirection.magnitude > 0.1f; } }
    private Vector3 inputDirection;
    private CharacterController controller;
    private ICharacterMovementInput movementInput;

    [Header("Gravity System")]
    public float gravityScale;
    private float characterGravity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        movementInput = GetComponent<ICharacterMovementInput>();
    }

    private void Update()
    {
        DoGravity();
    }

    public void DoMovement()
    {
        UpdateInput();
        inputDirection *= characterSpeed;

        controller.Move(inputDirection * Time.deltaTime);
    }

    public void DoMovement(float moveSpeed)
    {
        UpdateInput();
        inputDirection *= moveSpeed;

        controller.Move(inputDirection * Time.deltaTime);
    }

    private void DoGravity()
    {
        if (controller.isGrounded)
        {
            characterGravity = 0;
        }
        else
        {
            characterGravity -= gravityScale;
        }

        inputDirection.y = characterGravity;

        controller.Move(inputDirection * Time.deltaTime);
    }

    private void UpdateInput()
    {
        inputDirection = movementInput.MovementInput();
    }
}