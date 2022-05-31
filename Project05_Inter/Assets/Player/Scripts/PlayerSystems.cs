using System;
using UnityEngine;
using SaveSystem;

public class PlayerSystems : PlayerStateMachine, ISaveable
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

    public object CaptureState()
    {
        return new SaveData
        {
            xPos = transform.position.x,
            yPos = transform.position.y,
            zPos = transform.position.z,
            xRot = transform.rotation.x,
            yRot = transform.rotation.y,
            zRot = transform.rotation.z
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        GetComponent<CharacterController>().enabled = false;
        transform.SetPositionAndRotation(new Vector3(savedData.xPos, savedData.yPos, savedData.zPos), Quaternion.Euler(savedData.xRot, savedData.yRot, savedData.zRot));
        GetComponent<CharacterController>().enabled = true;
    }

    [Serializable]
    private struct SaveData
    {
        public float xPos;
        public float yPos;
        public float zPos;
        public float xRot;
        public float yRot;
        public float zRot;
    }
}