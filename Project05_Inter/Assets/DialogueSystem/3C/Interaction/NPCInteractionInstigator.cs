using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionInstigator : MonoBehaviour
{
    public List<DialogueSelector> m_NearbyNPCInteractables = new List<DialogueSelector>();
    [SerializeField]
    private PlayerQuestsManager m_QuestsManager;

    public PlayerQuestsManager QuestsManager => m_QuestsManager;

    public bool HasNearbyInteractables()
    {
        return m_NearbyNPCInteractables.Count != 0;
    }

    private void Update()
    {
        if (HasNearbyInteractables() && Input.GetButtonDown("Jump"))
        {
            //Ideally, we'd want to find the best possible interaction (ex: by distance & orientation).
            //m_NearbyInteractables[0].DoInteraction();
            m_NearbyNPCInteractables[0].PossibleDialogLine().DoInteraction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DialogueSelector dialogueSelector = other.GetComponent<DialogueSelector>();

        if (dialogueSelector != null)
        {
            dialogueSelector.SetQuestManager(m_QuestsManager);
            m_NearbyNPCInteractables.Add(dialogueSelector);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DialogueSelector dialogueSelector = other.GetComponent<DialogueSelector>();

        if (dialogueSelector != null)
        {
            m_NearbyNPCInteractables.Remove(dialogueSelector);
        }
    }
}