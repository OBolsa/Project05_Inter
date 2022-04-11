using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour, ICharacterMovementInput
{
    public NPCSystem system;

    public Vector3 MovementInput()
    {
        return system.direction;
    }
}