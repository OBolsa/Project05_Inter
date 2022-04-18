using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController_Match : MonoBehaviour, ITextController
{
    TextBoxSystem textSystem;
    TalkConfig config;
    int subjectIndex = 0;
    int narrativeLineIndex = 0;
    int textIndex = 0;
    bool isInTalk;

    public void StartConversation(TalkConfig newConfig)
    {
        textSystem = GetComponent<TextBoxSystem>();
        config = newConfig;
        textSystem.isInConversation = true;
        isInTalk = true;

        StartCoroutine(StartDialogue());
    }

    public void NextText()
    {
        if (config != null)
        {
            if (textIndex < config.Subjects[subjectIndex].Narratives[narrativeLineIndex].Text.Count - 1)
            {
                textIndex += 1;
                StartCoroutine(UpdateText());
            }
            else
            {
                textIndex = 0;

                if (narrativeLineIndex < config.Subjects[subjectIndex].Narratives.Count - 1)
                {
                    narrativeLineIndex += 1;
                    StartCoroutine(UpdateText());
                }
                else
                {
                    narrativeLineIndex = 0;

                    if (subjectIndex < config.Subjects.Count - 1)
                    {
                        subjectIndex += 1;
                        StartCoroutine(UpdateText());
                    }
                    else
                    {
                        subjectIndex = 0;
                        isInTalk = false;
                        EndConversation();
                    }


                    if (isInTalk)
                        StartCoroutine(UpdateText());
                }

                if (isInTalk)
                    StartCoroutine(UpdateText());
                //else if (config.)
            }
        }
    }

    public void EndConversation()
    {
        StartCoroutine(FinishDialogue());
    }

    public IEnumerator UpdateText()
    {
        textSystem.Text.ForceMeshUpdate();
        yield return ShowLetters();
    }

    public IEnumerator StartDialogue()
    {
        textSystem.Text.text = "";
        yield return textSystem.tween.FadeCoroutine(textSystem.tween.AnimationTime, true);
        yield return ShowLetters();
    }

    public IEnumerator FinishDialogue()
    {
        yield return textSystem.tween.FadeCoroutine(textSystem.tween.AnimationTime, false);
        textSystem.Text.ForceMeshUpdate();
        textSystem.Text.text = "";
        textSystem.isInConversation = false;
    }

    public IEnumerator ShowLetters()
    {
        WaitForSeconds s = new WaitForSeconds(textSystem.textAnimationSpeed / 100);
        int textCounter = 1;

        textSystem.Text.text = config.Subjects[subjectIndex].Narratives[narrativeLineIndex].Text[textIndex];
        textSystem.Text.ForceMeshUpdate();
        textSystem.Text.maxVisibleCharacters = 0;

        while (textCounter < textSystem.Text.textInfo.characterCount + 1)
        {
            textSystem.Text.maxVisibleCharacters = textCounter;
            textCounter++;
            yield return s;
        }
    }
}
