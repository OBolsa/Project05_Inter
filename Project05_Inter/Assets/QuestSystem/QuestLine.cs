using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Quest/QuestLine")]
public class QuestLine : ScriptableObject
{
    [SerializeField]
    private string m_QuestLineID;
    [SerializeField]
    private QuestLine m_QuestLineCondition;
    [SerializeField]
    private string m_QuestLineName;
    [SerializeField]
    private QuestStepNode m_FirstQuestStepNode;

    public string QuestLineID =>  m_QuestLineID;
    public QuestLine QuestLineCondition => m_QuestLineCondition;
    public string FirstQuestLineName => m_QuestLineName;
    public QuestStepNode QuestStepNode => m_FirstQuestStepNode;  

    public void GenerateQuestID()
    {
        if (m_QuestLineName == "")
        {
            Debug.Log("You need a quest name to generate a quest ID.");
            return;
        }

    }
}