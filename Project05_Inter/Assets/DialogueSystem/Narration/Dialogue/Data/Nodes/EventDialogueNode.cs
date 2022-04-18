using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Node/Event")]
public class EventDialogueNode : DialogueNode
{
    [SerializeField]
    private DialogueNode m_NextNode;
    [SerializeField]
    UnityEvent m_OnEndNode;
    public DialogueNode NextNode => m_NextNode;

    public override bool CanBeFollowedByNode(DialogueNode node)
    {
        return m_NextNode == node;
    }

    public override void Accept(IDialogueNodeVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override void EndNode()
    {
        m_OnEndNode?.Invoke();
    }
}