using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private DoorChannel m_DoorChannel;
    [SerializeField]
    private GameObject[] m_DoorModel;
    [SerializeField]
    private int m_DoorId;

    private void Awake()
    {
        m_DoorChannel.OnOpenDoor += OpenDoor;
        m_DoorChannel.OnCloseDoor += CloseDoor;
    }

    private void OnDestroy()
    {
        m_DoorChannel.OnOpenDoor -= OpenDoor;
        m_DoorChannel.OnCloseDoor -= CloseDoor;
    }

    public void OpenDoor(int doorId)
    {
        if(m_DoorId == doorId)
        {
            foreach (GameObject door in m_DoorModel)
                door.SetActive(false);
        }
    }

    public void CloseDoor(int doorId)
    {
        if(m_DoorId == doorId)
        {
            foreach (GameObject door in m_DoorModel)
                door.SetActive(true);
        }
    }
}