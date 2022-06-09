using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
    
public class DialogueSelector : MonoBehaviour, ISaveable
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
        List<Interactable> possibleInteractions = new List<Interactable>();

        foreach (Interactable item in m_Dialogs)
        {
            if(item.QuestStepNode != null)
            {
                bool CanTalkAboutThisQuest = m_QuestsManager.HaveQuest(item.QuestStepNode.QuestLine) && m_QuestsManager.GetQuestByID(item.QuestStepNode.QuestLine).CurrentQuestStep == item.QuestStepNode.QuestStep;

                if (CanTalkAboutThisQuest)
                {
                    possibleInteractions.Add(item);
                }
            }
        }

        if(possibleInteractions.Count > 0)
        {
            return m_Dialogs[DialogIndexByQuest(possibleInteractions[possibleInteractions.Count - 1].QuestStepNode.QuestLine)];
        }
        else
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

    public object CaptureState()
    {
        return new SaveData
        {
            xPos = transform.position.x,
            yPos = transform.position.y,
            zPos = transform.position.z
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        transform.position = new Vector3(savedData.xPos, savedData.yPos, savedData.zPos);
    }

    [System.Serializable]
    private struct SaveData
    {
        public float xPos;
        public float yPos;
        public float zPos;
    }
}