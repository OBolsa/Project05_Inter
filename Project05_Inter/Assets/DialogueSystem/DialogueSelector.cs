using System.Collections.Generic;
using UnityEngine;

public class DialogueSelector : MonoBehaviour
{
    [SerializeField]
    private Interactable[] m_Dialogs;
    [SerializeField]
    private PlayerQuestsManager m_QuestsManager;
    [SerializeField]
    private List<QuestLine> m_NPCQuests = new List<QuestLine>();

    public PlayerQuestsManager QuestsManager => m_QuestsManager;

    private void Awake()
    {
        foreach(Interactable item in m_Dialogs)
        {
            if(item.QuestStepNode != null)
            {
                m_NPCQuests.Add(item.QuestStepNode.QuestLine);
            }
        }
    }

    public Interactable PossibleDialogLine()
    {
        foreach (Interactable item in m_Dialogs)
        {
            if(item.QuestStepNode != null)
            {
                bool CanTalkAboutThisQuest = m_QuestsManager.HaveQuest(item.QuestStepNode.QuestLine) && m_QuestsManager.GetQuestByID(item.QuestStepNode.QuestLine).CurrentQuestStep == item.QuestStepNode.QuestStep;

                if (CanTalkAboutThisQuest)
                {
                    return m_Dialogs[DialogIndexByQuest(item.QuestStepNode.QuestLine)];
                }
            }
        }

        return m_Dialogs[0].QuestStepNode == null ? m_Dialogs[0] : null;
    }

    public int DialogIndexByQuest(QuestLine quest)
    {
        for (int i = 0; i < m_Dialogs.Length; i++)
        {
            if(m_Dialogs[i].QuestStepNode != null && m_Dialogs[i].QuestStepNode.QuestLine == quest && m_Dialogs[i].QuestStepNode.QuestStep == m_QuestsManager.GetQuestByID(quest).CurrentQuestStep)
            {
                return i;
            }
        }

        return 0;
    }

    public Interactable DialogByQuest(QuestLine quest)
    {
        foreach (Interactable item in m_Dialogs)
        {
            if (item.QuestStepNode == quest)
                return item;
        }

        return null;
    }

    public void SetQuestManager(PlayerQuestsManager questManager)
    {
        m_QuestsManager = questManager;
    }
}