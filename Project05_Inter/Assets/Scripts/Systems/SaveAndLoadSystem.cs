using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public class SaveAndLoadSystem : MonoBehaviour
    {
        private string SavePath => $"{Application.persistentDataPath}/save.txt";
        //private string SavePath => "C:/Users/Vitor/AppData/LocalLow/Skyduck/save.txt";

        [ContextMenu("Save")]
        private void Save()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }

        [ContextMenu("Load")]
        private void Load()
        {
            var state = LoadFile();
            RestoreState(state);
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                return new Dictionary<string, object>();
            }

            using (FileStream stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();

                Dictionary<string, object> result = new Dictionary<string, object>();

                try
                {
                    result = (Dictionary<string, object>)formatter.Deserialize(stream);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                    //throw;
                }

                return result;

                //return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.ID] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.ID, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}