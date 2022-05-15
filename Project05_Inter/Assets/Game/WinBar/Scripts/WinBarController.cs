using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBarController : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_NeedleSpots;
    [SerializeField]
    private int m_CenterSpot = 4;
    [SerializeField]
    private NeedleScript m_Needle;

    public NeedleScript Needle => m_Needle;

    private void Start()
    {
        SetNeedleInSpot(m_CenterSpot);
    }

    public void SetNeedleInSpot(int newPosition)
    {
        Vector3 needleNewPosition = m_NeedleSpots[newPosition].transform.position;

        if(newPosition < 0)
        {
            m_Needle.StartMovement(needleNewPosition, m_NeedleSpots[0]);
            m_Needle.SetNeedleSpotInfo(0);
        }
        else if (newPosition > m_NeedleSpots.Length - 1)
        {
            m_Needle.StartMovement(needleNewPosition, m_NeedleSpots[m_NeedleSpots.Length - 1]);
            m_Needle.SetNeedleSpotInfo(m_NeedleSpots.Length - 1);
        }
        else
        {
            m_Needle.StartMovement(needleNewPosition, m_NeedleSpots[newPosition]);
            m_Needle.SetNeedleSpotInfo(newPosition);
        }
    }
}
