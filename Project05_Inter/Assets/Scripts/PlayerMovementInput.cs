using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour, ICharacterMovementInput
{
    public Vector3 MovementInput()
    {
        Vector3 playerInput = new Vector3();
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.z = Input.GetAxisRaw("Vertical");
        playerInput.Normalize();

        return playerInput;
    }
}
