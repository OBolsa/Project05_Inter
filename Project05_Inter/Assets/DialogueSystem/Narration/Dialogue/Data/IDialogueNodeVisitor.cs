public interface IDialogueNodeVisitor
{
    void Visit(BasicDialogueNode node);
    void Visit(EventDialogueNode node);
    void Visit(ChoiceDialogueNode node);
}