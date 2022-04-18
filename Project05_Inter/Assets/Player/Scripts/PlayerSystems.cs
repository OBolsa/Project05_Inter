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
        //bool haveNpc = npcChecker.targetNPC != null;
        //bool isADiferentNpc = npcChecker.targetNPC.gameObject != atualInteractedNPC;

        //if (haveNpc)
        //{
        //    if (isADiferentNpc)
        //    {
        //        atualText = -1;
        //        interfaceManager.dialogueText.text = "";

        //        if (atualInteractedNPC != null)
        //            atualInteractedNPC.GetComponent<NPCSystem>().isTalking = false;

        //        Dialogue(false);
        //    }

        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        State.Interact();
        //    }

        //}
        //else
        //{
        //    atualText = -1;
        //    interfaceManager.dialogueText.text = "";

        //    if(atualInteractedNPC != null)
        //        atualInteractedNPC.GetComponent<NPCSystem>().isTalking = false;

        //    Dialogue(false);
        //}

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

    public void DialogueController()
    {
        //if(npcChecker.targetNPC.interactionState == 0)
        //{
        //    if (atualText == -1)
        //    {
        //        npcChecker.targetNPC.StopMove();
        //        npcChecker.targetNPC.isTalking = true;

        //        atualText = 0;
        //        interfaceManager.dialogueText.text = npcChecker.targetNPC.configs.firstDialogueOption[atualText];
        //        interfaceManager.dialogueName.text = npcChecker.targetNPC.configs.npcName;
        //        Dialogue(true);
        //    }
        //    else
        //    {
        //        atualText += 1;

        //        if (atualText < npcChecker.targetNPC.configs.firstDialogueOption.Length)
        //        {
        //            interfaceManager.dialogueText.text = npcChecker.targetNPC.configs.firstDialogueOption[atualText];
        //        }
        //        else
        //        {
        //            npcChecker.targetNPC.isTalking = false;
        //            atualText = -1;
        //            npcChecker.targetNPC.interactionState = 1;
        //            npcChecker.targetNPC.canvasManager.UpdateDisplay();
        //            Dialogue(false);
        //        }
        //    }
        //}
        //else if (npcChecker.targetNPC.interactionState == 1)
        //{
        //    npcChecker.targetNPC.SetState(new NPC_Dead(npcChecker.targetNPC));
        //}
    }
}