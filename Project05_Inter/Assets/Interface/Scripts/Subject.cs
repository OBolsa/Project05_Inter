using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Subject
{
    [SerializeField] public List<NarrativeLines> Narratives;
    [SerializeField] public bool OpenOptionsBoxWhenEndSubject;
}