using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Interactable
{
    [SerializeField]
    private QuestStepNode m_QuestStepNode;
    [SerializeField]
    UnityEvent m_OnInteraction;

    public QuestStepNode QuestStepNode => m_QuestStepNode;

    public void DoInteraction()
    {
        m_OnInteraction.Invoke();
    }
}