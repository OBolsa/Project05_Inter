using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    [SerializeField]
    private string m_SceneName;
    [SerializeField]
    private GameObject m_MainCamera;
    [SerializeField]
    private GameObject m_EventSystem;

    public string SceneName => m_SceneName;
    public GameObject MainCamera => m_MainCamera;
    public GameObject EventSystem => m_EventSystem;
}
