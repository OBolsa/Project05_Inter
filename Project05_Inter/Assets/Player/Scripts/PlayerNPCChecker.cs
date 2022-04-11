using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNPCChecker : MonoBehaviour
{
    public float radiusCheck;
    public LayerMask NPCLayer;
    public NPCSystem targetNPC;

    private void Update()
    {
        CheckArround();
    }

    public void DisplayNPCCanvas(bool active)
    {
        if (targetNPC == null)
            return;

        targetNPC.interactionCanvas.SetActive(active);
    }

    private void CheckArround()
    {
        Collider[] arround = Physics.OverlapSphere(transform.position, radiusCheck, NPCLayer);


        if (arround.Length > 0)
        {
            for (int i = 0; i < arround.Length; i++)
            {
                if (targetNPC == null)
                {
                    targetNPC = arround[i].GetComponent<NPCSystem>();
                }

                if (Vector3.Distance(transform.position, arround[i].transform.position) < Vector3.Distance(transform.position, targetNPC.transform.position))
                {
                    DisplayNPCCanvas(false);
                    targetNPC = arround[i].GetComponent<NPCSystem>();
                }
            }

            DisplayNPCCanvas(true);
        }
        else
        {
            if (targetNPC != null)
                DisplayNPCCanvas(false);

            targetNPC = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusCheck);
    }
}