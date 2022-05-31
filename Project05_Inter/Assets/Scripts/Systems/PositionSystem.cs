using System;
using System.Collections;
using UnityEngine;
using SaveSystem;

public class PositionSystem : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        return new SaveData
        {
            xPosition = transform.position.x,
            yPosition = transform.position.y,
            zPosition = transform.position.z
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        //GetComponent<CharacterController>().enabled = false;

        transform.position = new Vector3(saveData.xPosition, saveData.yPosition, saveData.zPosition);

        //GetComponent<CharacterController>().enabled = true;
    }

    [Serializable]
    private struct SaveData
    {
        public float xPosition;
        public float yPosition;
        public float zPosition;
    }
}