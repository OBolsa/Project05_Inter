using UnityEngine;
using UnityEngine.Events;

public class UIObject : MonoBehaviour
{
    [Header("Ui Region")]
    [SerializeField]
    private string m_UiStateRelate;
    [SerializeField]
    private MenuChannel m_MenuChannel;
    public bool isActive;

    [Header("Tween Region")]
    [SerializeField]
    private UnityEvent m_OnEnter;
    [SerializeField]
    private UnityEvent m_OnExit;

    private void Awake()
    {
        m_MenuChannel.OnStateChange += EnterUiElement;
        m_MenuChannel.OnStateChange += ExitUiElement;
    }

    private void OnDestroy()
    {
        m_MenuChannel.OnStateChange -= EnterUiElement;
        m_MenuChannel.OnStateChange -= ExitUiElement;
    }

    public void EnterUiElement(string state)
    {
        if(state == m_UiStateRelate)
        {
            isActive = true;
            m_OnEnter.Invoke();
        }
    }

    public void ExitUiElement(string state)
    {
        if(state != m_UiStateRelate && isActive)
        {
            isActive = false;
            m_OnExit.Invoke();
        }
    }
}