using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombinations
{
    public int CheckCardsValueCombinations (List<GameObject> cards);
    public int CheckCardsSuitsCombinations (List<GameObject> cards);
    public int CheckCardsSequenceCombinations (List<GameObject> cards);
    public int CheckCombinationFour (List<GameObject> cards);
    public int CheckCombinationFive (List<GameObject> cards);
}
