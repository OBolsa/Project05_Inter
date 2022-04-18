using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface ITextController
{
    void StartConversation(TalkConfig talkConfig);
    void NextText();
    void EndConversation();
    IEnumerator ShowLetters();
}
