using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleScript : MonoBehaviour
{
    [SerializeField]
    private float m_AnimationSpeed;
    [SerializeField]
    private int m_NeedleSpotInfo;

    public int NeedleSpotInfo => m_NeedleSpotInfo;

    private Vector3 m_AtualPosition;
    private Vector3 m_DesiredPosition;
    private Transform m_TransformParent;
    private float m_EnlapsedTime;
    private float m_PercentageComplete;
    private bool m_IsInMovement;

    public delegate void MatchEndCallBack();
    public MatchEndCallBack OnPlayerWin;
    public MatchEndCallBack OnEnemyWin;

    void Update()
    {
        DoMovement();
    }

    private void DoMovement()
    {
        bool reachDesiredPosition = transform.position == m_DesiredPosition;

        if (m_IsInMovement)
        {
            m_EnlapsedTime += Time.deltaTime;
            m_PercentageComplete = m_EnlapsedTime / m_AnimationSpeed;

            transform.position = Vector3.Lerp(m_AtualPosition, m_DesiredPosition, Mathf.SmoothStep(0, 1, m_PercentageComplete));

            if (reachDesiredPosition)
            {
                m_IsInMovement = false;
                m_DesiredPosition = Vector3.zero;

                if (m_TransformParent != null)
                {
                    transform.SetParent(m_TransformParent, true);
                    m_TransformParent = null;
                }

                CheckWinner();
            }
        }
    }

    public void SetNeedleSpotInfo(int position)
    {
        m_NeedleSpotInfo = position;
    }

    public void StartMovement(Vector3 position, Transform toTransform)
    {
        m_AtualPosition = transform.position;
        m_DesiredPosition = position;

        m_EnlapsedTime = 0;

        m_TransformParent = toTransform;
        m_IsInMovement = true;
    }

    public void CheckWinner()
    {
        if (m_NeedleSpotInfo == 0)
        {
            OnPlayerWin?.Invoke();
        }
        else if (m_NeedleSpotInfo == 8)
        {
            OnEnemyWin?.Invoke();
        }
        else
        {
            return;
        }
    }
}
