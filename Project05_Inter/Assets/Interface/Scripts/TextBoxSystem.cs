using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxSystem : MonoBehaviour
{
    [Header("Atributtes")]
    public TMP_Text Speaker;
    public TMP_Text Text;
    public GameObject OptionsBox;

    [Header("Text Configs")]
    [Range(1f, 5f)] public float textAnimationSpeed;
    public TalkConfig talkConfig;
    public ITextController controller;

    [HideInInspector] public bool isInConversation;
    [HideInInspector] public TweenAnimationController tween;

    private void Awake()
    {
        tween = GetComponent<TweenAnimationController>();
        controller = GetComponent<ITextController>();
    }

    private void Start()
    {
        Text.text = "";
        Text.ForceMeshUpdate();
    }

    private void Update()
    {
        Text.ForceMeshUpdate();

        if (Input.GetButtonDown("Jump"))
        {
            if (!isInConversation)
                controller.StartConversation(talkConfig);
            else
                controller.NextText();
        }
    }

    public IEnumerator StartTalk()
    {
        yield return tween.FadeCoroutine(tween.AnimationTime, true);
        Text.ForceMeshUpdate();
        yield return ShowLetters(Text, textAnimationSpeed);
    }

    public IEnumerator ShowLetters(TMP_Text text, float time)
    {
        int textCounter = 1;

        WaitForSeconds s = new WaitForSeconds(textAnimationSpeed / 100);

        while (textCounter < Text.textInfo.characterCount + 1)
        {
            Text.maxVisibleCharacters = textCounter;
            textCounter++;
            yield return s;
        }
    }

    public void OpenOptionsBox()
    {
        TweenAnimationController t = OptionsBox.GetComponent<TweenAnimationController>();

        t.StartFade(t.AnimationTime, true);
    }
}
