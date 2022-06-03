using UnityEngine;
using TMPro;
using SaveSystem;

public class UiObjective : MonoBehaviour, ISaveable
{
    [SerializeField]
    private TMP_Text m_ObjectiveDisplay;
    [SerializeField]
    private ObjectiveChannel m_ObjectiveChannel;

    private void Awake()
    {
        m_ObjectiveChannel.OnUpdateObjective += UpdateText;
    }

    private void OnDestroy()
    {
        m_ObjectiveChannel.OnUpdateObjective -= UpdateText;
    }

    public void UpdateText(string text)
    {
        m_ObjectiveDisplay.text = text;
    }

    public object CaptureState()
    {
        return new SaveDate
        {
            questDisplay = m_ObjectiveDisplay.text
        };
    }

    public void RestoreState(object state)
    {
        var savedState = (SaveDate)state;

        UpdateText(savedState.questDisplay);
    }

    [System.Serializable]
    public struct SaveDate
    {
        public string questDisplay;
    }
}