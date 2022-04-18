using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchState_PointsCounting : MatchState
{
    public MatchState_PointsCounting(MatchSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        waitTime = new WaitForSeconds(2f);

        System.PlayerHand.canHighlightCard = false;
        System.DoFeedback("Fim do round " + (System.Round + 1).ToString(), 1f);

        yield return waitTime;

        System.DoFeedback("Contabilizando pontos", 1f);

        Debug.Log("_Counting Points");
        System.CountingPoints();

        yield return waitTime;

        Debug.Log("_Points Counted");
        System.DoFeedback("Jogador  " + System.PlayerPoints + "/" + System.EnemyPoints + " Adversário", 1f);

        yield return waitTime;

        string vencedor;

        if(System.PlayerPoints > System.EnemyPoints)
        {
            vencedor = "Jogador";
        }
        else if (System.PlayerPoints < System.EnemyPoints)
        {
            vencedor = "Adversário";
        }
        else
        {
            vencedor = "Empate";
        }

        System.DoFeedback(vencedor != "Empate" ? vencedor + " Venceu!" : vencedor, 1f);

        yield return waitTime;

        if(vencedor == "Jogador")
            System.MoveNeedle(true);
        else if (vencedor == "Adversário")
            System.MoveNeedle(false);

        System.ResetPoints();
        System.StartCoroutine(System.NextRound());
    }
}