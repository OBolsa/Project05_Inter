using UnityEngine;

public abstract class DialogueNode : ScriptableObject
{
    [SerializeField]
    private NarrationLine m_DialogueLine;
    [SerializeField]
    private QuestStepNode m_QuestNode;

    public NarrationLine DialogueLine => m_DialogueLine;
    public QuestStepNode QuestNode => m_QuestNode;

    public abstract bool CanBeFollowedByNode(DialogueNode node);
    public abstract void Accept(IDialogueNodeVisitor visitor);
    public abstract void EndNode();
}
