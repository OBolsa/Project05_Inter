using UnityEngine;

[System.Serializable]
public class QuestData
{
    private QuestLine m_questLine;

    public QuestLine QuestLine => m_questLine;
    public string QuestID => m_questLine.QuestLineID;

    public int CurrentQuestStep;
    public bool IsCompleted;

    public QuestData(QuestLine quest)
    {
        m_questLine = quest;
        CurrentQuestStep = 0;
        IsCompleted = false;
    }
}
