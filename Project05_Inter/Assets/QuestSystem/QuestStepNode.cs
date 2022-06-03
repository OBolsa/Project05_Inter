using UnityEngine;

public abstract class QuestStepNode : ScriptableObject
{
    [SerializeField]
    private QuestLine m_QuestLine;
    [SerializeField]
    private int m_QuestStepCondition;
    [SerializeField]
    private int m_QuestStep;
    [SerializeField]
    private string m_QuestStepName;
    [SerializeField]
    private string m_QuestDisplayObjective;
    [SerializeField]
    [TextArea(0, 5)] private string m_QuestStepDescription;

    public QuestLine QuestLine => m_QuestLine;
    public int QuestStepCondition => m_QuestStepCondition;
    public int QuestStep => m_QuestStep;
    public string QuestStepName => m_QuestStepName;
    public string QuestStepDescription => m_QuestStepDescription;
    public string QuestStepOjbective => m_QuestDisplayObjective;

    public abstract bool CanBeFollowedByNode(QuestStepNode node);
    public abstract void Accept(IQuestNodeVisitor visitor);
}