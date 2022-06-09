using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class TriggersManager : MonoBehaviour, ISaveable
{
    [SerializeField] private GameObject[] m_Triggers;

    public object CaptureState()
    {
        bool[] capture = new bool[m_Triggers.Length];

        for (int i = 0; i < m_Triggers.Length; i++)
        {
            capture[i] = m_Triggers[i].activeSelf;
        }

        return new SaveData
        {
            isActive = capture
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        for (int i = 0; i < savedData.isActive.Length; i++)
        {
            m_Triggers[i].SetActive(savedData.isActive[i]);
        }
    }

    [System.Serializable]
    public struct SaveData
    {
        public bool[] isActive;
    }
}
