using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest/Door Channel")]
public class DoorChannel : ScriptableObject
{
    public delegate void DoorCallback(int doorId);
    public DoorCallback OnOpenDoor;
    public DoorCallback OnCloseDoor;

    public void OpenDoor(int doorId)
    {
        OnOpenDoor?.Invoke(doorId);
    }

    public void CloseDoor(int doorId)
    {
        OnCloseDoor?.Invoke(doorId);
    }
}
