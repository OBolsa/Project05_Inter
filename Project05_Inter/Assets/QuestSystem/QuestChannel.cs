using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest/QuestChannel")]
public class QuestChannel : ScriptableObject
{
    public delegate void QuestCallback(QuestLine quest);
    public QuestCallback OnQuestRequested;
    public QuestCallback OnQuestStart;
    public QuestCallback OnQuestEnd;

    public delegate void QuestStepCallback(QuestStepNode node);
    public QuestStepCallback OnQuestNodeRequested;
    public QuestStepCallback OnQuestNodeStart;
    public QuestStepCallback OnQuestNodeEnd;

    public void RaiseRequestQuest(QuestLine quest)
    {
        OnQuestRequested?.Invoke(quest);
    }

    public void RaiseQuestStart(QuestLine quest)
    {
        OnQuestStart?.Invoke(quest);
    }

    public void RaiseQuestEnd(QuestLine quest)
    {
        OnQuestEnd?.Invoke(quest);
    }

    public void RaiseRequestQuestStep(QuestStepNode node)
    {
        OnQuestNodeRequested?.Invoke(node);
    }

    public void RaiseQuestNodeStart(QuestStepNode node)
    {
        OnQuestNodeStart?.Invoke(node);
    }

    public void RaiseQuestNodeEnd(QuestStepNode node)
    {
        OnQuestNodeEnd?.Invoke(node);
    }
}
