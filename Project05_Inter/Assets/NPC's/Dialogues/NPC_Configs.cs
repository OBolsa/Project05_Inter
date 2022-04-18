using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC_", menuName = "Scriptable Object/Create New/NPC")]
public class NPC_Configs : ScriptableObject
{
    [Header("NPC Atributtes")]
    public string npcName;
    public Material npcMaterial;
    public bool walk;

    [Header("Dialogues")]
    public TalkConfig talk;
}
