using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Match/Match Channel")]
public class MatchChannel : ScriptableObject
{
    public delegate void MatchCallback();
    public MatchCallback OnMatchStart;
    public MatchCallback OnMatchEnd;

    public void RaiseMatchStart()
    {
        OnMatchStart?.Invoke();
    }

    public void RaiseMatchEnd()
    {
        OnMatchEnd?.Invoke();
    }
}