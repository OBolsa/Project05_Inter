using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest/Step/Node/BasicNode")]
public class BasicQuestStepNode : QuestStepNode
{
    [SerializeField]
    private QuestStepNode m_NextNode;
    public QuestStepNode NextNode => m_NextNode;

    public override bool CanBeFollowedByNode(QuestStepNode node)
    {
        return m_NextNode == node;
    }

    public override void Accept(IQuestNodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}