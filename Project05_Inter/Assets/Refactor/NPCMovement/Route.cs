using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Route : MonoBehaviour
{
    [SerializeField]
    private string m_RouteName;
    [SerializeField]
    private PlaceSpot[] m_Spots;
    public PlaceSpot[] Spots => m_Spots;
}

public class RouteException : System.Exception
{
    public RouteException(string message)
        : base(message)
    {
    }
}

public class RouteSequencer
{
    public delegate void RouteCallback(Route route);
    public delegate void PlaceSpotCallback(PlaceSpot spot);

    public RouteCallback OnRouteStart;
    public RouteCallback OnRouteEnd;
    public PlaceSpotCallback OnWalkToSpotStart;
    public PlaceSpotCallback OnWalkToSpotEnd;

    private Route m_CurrentRoute;
    private PlaceSpot m_CurrentSpot;

    public Route CurrentRoute => m_CurrentRoute;
    public PlaceSpot CurrentSpot => m_CurrentSpot;

    public void StartRoute(Route route)
    {
        if(m_CurrentRoute == null)
        {
            m_CurrentRoute = route;
            OnRouteStart?.Invoke(route);
            StartMoveToSpot(route.Spots[0]);
        }
        else
        {
            throw new RouteException("Can't start a route when another is already running.");
        }
    }

    public void EndRoute(Route route)
    {
        if (m_CurrentRoute == route)
        {
            StopMoveToSpot(m_CurrentSpot);
            OnRouteEnd?.Invoke(m_CurrentRoute);
            m_CurrentRoute = null;
        }
        else
        {
            throw new RouteException("Trying to stpo a route that ins't running.");
        }
    }

    private bool CanMoveToThisSpot(PlaceSpot spot)
    {
        return m_CurrentSpot == null || spot == null || m_CurrentSpot.CanBeFollowedBySpot(spot);
    }

    public void GoToNextSpot()
    {
        if (m_CurrentSpot.NextSpot != null)
        {
            StartMoveToSpot(m_CurrentSpot.NextSpot);
        }
    }

    public void StartMoveToSpot(PlaceSpot spot)
    {
        if (CanMoveToThisSpot(spot))
        {
            if (m_CurrentSpot != null)
                m_CurrentSpot.EndMovement();

            StopMoveToSpot(m_CurrentSpot);

            m_CurrentSpot = spot;

            if(m_CurrentSpot != null)
            {
                OnWalkToSpotStart?.Invoke(spot);
            }
            else
            {
                EndRoute(m_CurrentRoute);
            }
        }
        else
        {
            throw new RouteException("Failed to start move to Spot.");
        }
    }

    private void StopMoveToSpot(PlaceSpot spot)
    {
        if(m_CurrentSpot == spot)
        {
            OnWalkToSpotEnd?.Invoke(spot);
            m_CurrentSpot = null;
        }
        else
        {
            throw new RouteException("Trying to stop a move to a spot that ins't running.");
        }
    }
}

[System.Serializable]
public class PlaceSpot
{
    public string SpotName;
    public float SecondsInSpot;

    [SerializeField]
    private Transform m_SpotPosition;
    public Vector3 SpotPosition => m_SpotPosition.position;

    [SerializeField]
    private Route m_Route;
    [SerializeField]
    private string m_NextSpot;
    [SerializeField]
    private UnityEvent OnMovementEnd;

    public PlaceSpot NextSpot
    {
        get 
        {
            if (m_Route == null)
                return null;

            for (int i = 0; i < m_Route.Spots.Length; i++)
            {
                if(m_Route.Spots[i].SpotName == m_NextSpot)
                    return m_Route.Spots[i];
            }

            return null;
        }
    }

    public bool CanBeFollowedBySpot(PlaceSpot spot)
    {
        return m_NextSpot == spot.SpotName;
    }

    public void EndMovement()
    {
        OnMovementEnd?.Invoke();
    }
}