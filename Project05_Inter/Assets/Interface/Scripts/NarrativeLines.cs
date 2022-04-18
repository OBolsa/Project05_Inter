using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NarrativeLines
{
    [SerializeField, TextArea(0, 6)] public List<string> Text;
    [SerializeField] public bool OpenOptionsBoxWhenEndNarrative;
    [SerializeField] public List<string> Questions;
    [SerializeField] public bool EndConversationHere;
}
