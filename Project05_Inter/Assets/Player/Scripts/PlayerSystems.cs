using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystems : PlayerStateMachine
{
    [Header("Player Atributes")]
    public Rigidbody rb;
    public PlayerNPCChecker npcChecker;
    public UiElementsManager interfaceManager;
    public GameObject atualInteractedNPC;
    public int atualText = -1;

    [Header("External Scripts")]
    public CharacterMovement pMovement;
    public TextBoxSystem textBox;

    private void Start()
    {
        SetState(new PlayerState_Start(this));
    }

    private void Update()
    {
        if (pMovement.IsMoving)
            State.Move();
    }

    public void Dialogue(bool condition)
    {
        if (condition)
        {
            atualInteractedNPC = npcChecker.targetNPC.gameObject;
        }
        else
        {
            atualInteractedNPC = null;
        }

        interfaceManager.dialogueObject.SetActive(condition);
    }
}