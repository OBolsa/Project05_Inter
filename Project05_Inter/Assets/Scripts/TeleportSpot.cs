using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpot : MonoBehaviour
{
    [SerializeField]
    private int m_TeleportId;
    [SerializeField]
    private GameObject m_Target;
    [SerializeField]
    private TeleportChannel m_TeleportChannel;

    private void Start()
    {
        m_TeleportChannel.OnTeleport += DoTeleport;
    }

    private void OnDestroy()
    {
        m_TeleportChannel.OnTeleport -= DoTeleport;
    }

    public void DoTeleport(int teleportId)
    {
        if(teleportId == m_TeleportId)
        {
            Debug.Log("doTeleport. " + m_Target.name + " goes from " + m_Target.transform.position + " to " + transform.position);
            CharacterController controller = m_Target.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;
            m_Target.transform.position = transform.position;
            if (controller != null)
                controller.enabled = true;
        }
    }
}
