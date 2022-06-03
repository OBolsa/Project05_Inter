using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ojbective/Objective Channel")]
public class ObjectiveChannel : ScriptableObject
{
    public delegate void ObjectiveCallback(string objective);
    public ObjectiveCallback OnUpdateObjective;

    public void UpdateObjective(string objective)
    {
        OnUpdateObjective?.Invoke(objective);
    }
}
