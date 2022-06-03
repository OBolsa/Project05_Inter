using UnityEngine;

[System.Serializable]
public class QuestData
{
    [SerializeField]
    private string m_QuestID;
    public string QuestID => m_QuestID;

    public int CurrentQuestStep;
    public bool IsCompleted;

    public QuestData(string questId)
    {
        m_QuestID = questId;
        CurrentQuestStep = 0;
        IsCompleted = false;
    }
}