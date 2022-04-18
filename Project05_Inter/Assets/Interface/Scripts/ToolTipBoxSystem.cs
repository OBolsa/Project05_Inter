using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipBoxSystem : MonoBehaviour
{
    [Header("Atributtes")]
    public TMP_Text Title;
    public TMP_Text SubTitle;
    public TMP_Text BodyText;
    public bool IsBeingUsed { get; private set; }

    [Header("Text Configs")]
    [Range(1f, 5f)] public float textAnimationSpeed;
    [HideInInspector] public TweenAnimationController tween;

    private void Awake()
    {
        tween = GetComponent<TweenAnimationController>();
    }

    public void ShowToolTip(string title, string subtitle, string bodyText, bool show)
    {
        CanvasGroup canvas = GetComponent<CanvasGroup>();

        //if (show)
        //{
            Title.text = title;
            SubTitle.text = subtitle;
            BodyText.text = bodyText;
        //    canvas.alpha = 1;
        //}
        //else
        //{
        //    Title.text = "";
        //    SubTitle.text = "";
        //    BodyText.text = "";
        //    canvas.alpha = 0;
        //}
        StartCoroutine(ShowToolTipDisplay(show));
    }

    public IEnumerator ShowToolTipDisplay(bool startShow)
    {
        if (startShow)
        {
            yield return tween.FadeCoroutine(tween.AnimationTime, true);
            IsBeingUsed = true;
        }
        else
        {
            yield return tween.FadeCoroutine(tween.AnimationTime, false);
            IsBeingUsed = false;
            Title.text = "";
            SubTitle.text = "";
            BodyText.text = "";
        }
    }

    public IEnumerator ShowLetters(TMP_Text text, float time)
    {
        int textCounter = 1;

        WaitForSeconds s = new WaitForSeconds(textAnimationSpeed / 100);

        while (textCounter < BodyText.textInfo.characterCount + 1)
        {
            BodyText.maxVisibleCharacters = textCounter;
            textCounter++;
            yield return s;
        }
    }
}
