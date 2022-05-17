 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest/Teleport Channel")]
public class TeleportChannel : ScriptableObject
{
    public delegate void TeleportCallback(int teleportId);
    public TeleportCallback OnTeleport;

    public void RaiseTeleport(int teleportId)
    {
        OnTeleport?.Invoke(teleportId);
    }
}
