using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_CanvasManager : MonoBehaviour
{
    [Header("NPC Atributtes")]
    public NPCSystem npc;

    [Header("Canvas Atributtes")]
    public GameObject interactDisplay;
    public GameObject killDisplay;
    public GameObject deadDisplay;

    private void OnEnable()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        switch (npc.interactionState)
        {
            default:
            case 0:
                interactDisplay.SetActive(true);
                killDisplay.SetActive(false);
                deadDisplay.SetActive(false);
                break;
            case 1:
                interactDisplay.SetActive(false);
                killDisplay.SetActive(true);
                deadDisplay.SetActive(false);
                break;
            case 2:
                interactDisplay.SetActive(false);
                killDisplay.SetActive(false);
                deadDisplay.SetActive(true);
                break;
        }
    }
}