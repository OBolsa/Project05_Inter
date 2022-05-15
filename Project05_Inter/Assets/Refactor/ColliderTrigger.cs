using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private string m_InteractionTag;
    [SerializeField]
    private FlowChannel m_FlowChannel;
    [SerializeField]
    private UnityEvent OnEnter;
    [SerializeField]
    private UnityEvent OnStay;
    [SerializeField]
    private UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.CompareTag(m_InteractionTag))
        {
            OnEnter?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(m_InteractionTag))
            OnStay?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_InteractionTag))
            OnExit?.Invoke();
    }

    public void RaiseNewState(FlowState flowState)
    {
        m_FlowChannel.RaiseFlowStateRequest(flowState);
    }
}
