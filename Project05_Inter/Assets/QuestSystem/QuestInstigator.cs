using UnityEngine;

public class QuestInstigator : MonoBehaviour
{
    public DialogueSequencer DialogueSequencer;
    public PlayerQuestsManager PlayerQuestsManager;

    private void Awake()
    {
        DialogueSequencer = GetComponent<DialogueInstigator>().DialogueSequencer;
        PlayerQuestsManager = GetComponent<PlayerQuestsManager>();

        DialogueSequencer.OnDialogueStart += StartQuest;
        DialogueSequencer.OnDialogueNodeStart += RaiseQuestStep;
    }

    private void OnDestroy()
    {
        DialogueSequencer.OnDialogueStart -= StartQuest;
        DialogueSequencer.OnDialogueNodeStart -= RaiseQuestStep;
    }

    public void StartQuest(Dialogue dialogue)
    {
        if (dialogue.FirstNode.QuestNode == null)
            return;

        if(!PlayerQuestsManager.HaveQuest(dialogue.FirstNode.QuestNode.QuestLine) && PlayerQuestsManager.CanStartThisQuest(dialogue.FirstNode.QuestNode.QuestLine))
        {
            PlayerQuestsManager.PlayerQuestData.Add(new QuestData(dialogue.FirstNode.QuestNode.QuestLine.QuestLineID, dialogue.FirstNode.QuestNode.QuestStepOjbective));
            PlayerQuestsManager.UpdateQuestObjective(dialogue.FirstNode.QuestNode.QuestStepOjbective);
        }
        else if (PlayerQuestsManager.HaveQuest(dialogue.FirstNode.QuestNode.QuestLine) && PlayerQuestsManager.CanStartThisNode(dialogue.FirstNode.QuestNode))
        {
            PlayerQuestsManager.GetQuestByID(dialogue.FirstNode.QuestNode.QuestLine).CurrentQuestStep = dialogue.FirstNode.QuestNode.QuestStep;
            PlayerQuestsManager.UpdateQuestObjective(dialogue.FirstNode.QuestNode.QuestStepOjbective);
        }
    }

    public void RaiseQuestStep(DialogueNode node)
    {
        if (node.QuestNode == null)
            return;

        if(PlayerQuestsManager.HaveQuest(node.QuestNode.QuestLine) && PlayerQuestsManager.CanStartThisNode(node.QuestNode))
        {
            PlayerQuestsManager.RaiseNode(node.QuestNode);
        }
    }
}