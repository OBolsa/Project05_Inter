using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Interface/Menu Channel")]
public class MenuChannel : ScriptableObject
{
    public delegate void MenuStateCallBack(string state);
    public MenuStateCallBack OnStateChange;

    public void RaiseMenuState(string state)
    {
        OnStateChange?.Invoke(state);
    }
}