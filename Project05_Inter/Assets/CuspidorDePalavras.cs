using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CuspidorDePalavras : MonoBehaviour
{
    public int howManyLetters = 5;
    [SerializeField] public TextAsset words;
    [TextArea(0, 20)] public string export;
    private List<string> wordsWithLetters;

    public void SeparateWords()
    {
        var wordsInFile = words.text;
        var lines = wordsInFile.Split('\n');
        wordsWithLetters = new List<string>();

        foreach (string line in lines)
        {
            if (line.Length != howManyLetters + 1)
                continue;
            else
                wordsWithLetters.Add(line);
        }

        export = "";

        foreach (string word in wordsWithLetters)
        {
            export += word + ",";
        }
    }

    [ContextMenu("Export File")]
    public void Export()
    {
        SeparateWords();

        if (wordsWithLetters.Count <= 0)
            return;

        string path = "Assets/TextFiles/WordsWith"+ howManyLetters + "Letters.txt";

        StreamWriter writer = new StreamWriter(path, false);

        writer.Write(export);
        writer.Close();
    }
}
