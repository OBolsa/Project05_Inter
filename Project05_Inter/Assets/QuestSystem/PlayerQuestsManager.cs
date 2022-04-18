using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestsManager : MonoBehaviour
{
    [SerializeField]
    public List<QuestData> PlayerQuestData = new List<QuestData>();

    public bool HaveQuest(QuestLine quest)
    {
        return QuestByQuestID(quest) != null;
    }

    public bool QuestIsComplete(QuestLine quest)
    {
        bool isComplete = false;

        if (!HaveQuest(quest))
            return isComplete;

        if (QuestByQuestID(quest).IsCompleted)
        {
            isComplete = true;
        }

        return isComplete;
    }

    private QuestData QuestByQuestID(QuestLine quest)
    {
        bool findQuest = false;
        int index = 0;

        for (int i = 0; i < PlayerQuestData.Count; i++)
        {
            if(PlayerQuestData[i].QuestID == quest.QuestLineID)
            {
                index = i;
                findQuest = true;
            }
        }

        if (findQuest)
            return PlayerQuestData[index];
        else
            return null;
    }

    private QuestData QuestByQuestID(string quest)
    {
        bool findQuest = false;
        int index = 0;

        for (int i = 0; i < PlayerQuestData.Count; i++)
        {
            if (PlayerQuestData[i].QuestID == quest)
            {
                index = i;
                findQuest = true;
            }
        }

        if (findQuest)
            return PlayerQuestData[index];
        else
            return null;
    }

    public bool CanStartThisQuest(QuestLine quest)
    {
        if (quest.QuestLineCondition == null)
            return true;
        else
            return HaveQuest(quest.QuestLineCondition) && QuestByQuestID(quest).IsCompleted;
    }

    public QuestData GetQuestByID(QuestLine quest)
    {
        return QuestByQuestID(quest);
    }
    
    public bool CanStartThisNode(QuestStepNode quest)
    {
        return QuestByQuestID(quest.QuestLine).CurrentQuestStep == quest.QuestStepCondition;
    }

    public void RaiseNode(QuestStepNode node)
    {
        QuestByQuestID(node.QuestLine).CurrentQuestStep = node.QuestStep;
    }

    public void CompleteQuest(QuestLine quest)
    {
        QuestByQuestID(quest).IsCompleted = true;
    }
}