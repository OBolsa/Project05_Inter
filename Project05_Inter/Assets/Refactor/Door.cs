using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class Door : MonoBehaviour, ISaveable
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

    public object CaptureState()
    {
        bool[] door = new bool[m_DoorModel.Length];

        for (int i = 0; i < door.Length; i++)
        {
            door[i] = m_DoorModel[i].activeSelf;
        }

        return new SaveData
        {
            doorsModelsOpen = door
        };
    }

    public void RestoreState(object state)
    {
        var savedState = (SaveData)state;

        for (int i = 0; i < m_DoorModel.Length; i++)
        {
            m_DoorModel[i].SetActive(savedState.doorsModelsOpen[i]);
        }
    }

    [System.Serializable]
    public struct SaveData
    {
        public bool[] doorsModelsOpen;
    }
}