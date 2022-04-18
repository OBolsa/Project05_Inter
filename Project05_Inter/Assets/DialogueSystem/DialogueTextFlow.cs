using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTextFlow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Text;
    [SerializeField]
    private float m_TextAnimationSpeed;

    private void OnEnable()
    {
        if (m_Text == null)
            m_Text = GetComponent<TMP_Text>();

        if (m_Text == null)
            return;

        StartCoroutine(ShowText());
    }

    public IEnumerator ShowText()
    {
        m_Text.maxVisibleCharacters = 0;

        yield return new WaitForSeconds(m_TextAnimationSpeed);

        WaitForSeconds seconds = new WaitForSeconds(m_TextAnimationSpeed);
        
        while(m_Text.maxVisibleCharacters < m_Text.textInfo.characterCount)
        {
            m_Text.maxVisibleCharacters++;
            yield return seconds;
        }
    }
}
