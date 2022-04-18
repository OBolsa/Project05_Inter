using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Talk_NewTalk", menuName = "Create/Scriptable Object/New Talk"), System.Serializable]
public class TalkConfig : ScriptableObject
{
    [SerializeField] public List<Subject> Subjects;
}