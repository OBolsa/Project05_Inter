using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator m_Anim;
    [SerializeField] private CharacterController m_CharacterController;
    [SerializeField] private CharacterMovement m_Movement;

    private void Update()
    {
        m_Anim.SetFloat("Move", m_CharacterController.velocity.magnitude);
        m_Anim.SetFloat("Speed", m_Movement.CharacterRealSpeed);
    }
}
