using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavScript : MonoBehaviour
{
    [SerializeField]
    private bool m_StartRouteInAwake;
    [SerializeField]
    private Route m_Route;
    private NavMeshAgent m_NavMeshAgent;
    private RouteSequencer m_Sequencer = new RouteSequencer();

    private bool m_ReachSpot = false;

    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartMovement();
    }

    private void Update()
    {
        if(m_Sequencer.CurrentSpot.SpotPosition != null && m_NavMeshAgent.enabled)
            m_NavMeshAgent.destination = m_Sequencer.CurrentSpot.SpotPosition;

        CheckMovement();
    }

    private void StartMovement()
    {
        if (m_Route == null)
            return;

        if (m_StartRouteInAwake)
        {
            m_Sequencer.StartRoute(m_Route);
        }
    }

    public void CheckMovement()
    {
        if(Vector3.Distance(transform.position, m_NavMeshAgent.destination) < 0.1f && !m_ReachSpot && m_NavMeshAgent.enabled)
        {
            m_ReachSpot = true;
            StartCoroutine(WaitForNextSpot(m_Sequencer.CurrentSpot.SecondsInSpot));
        }
    }

    private IEnumerator WaitForNextSpot(float seconds)
    {
        WaitForSeconds waitTime = new WaitForSeconds(seconds);

        yield return waitTime;

        m_Sequencer.GoToNextSpot();
        m_ReachSpot = false;
    }
}