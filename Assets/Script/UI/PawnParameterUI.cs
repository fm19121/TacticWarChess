using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PawnParameterUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI pawnName; 
    [SerializeField]private TextMeshProUGUI pawnParams; 

    void Start()
    {
        PawnActionManager.Instance.OnSelectedPawnChanged += UpdateTextInfo;
        TurnManager.Instance.ToNextTurn += UpdateTextInfo;
        PawnActionManager.Instance.OnSpenActionPoint += UpdateTextInfo;
        UpdateTextInfo();
    }

    private void UpdateTextInfo()
    {
        gameObject.SetActive(true);
        Pawn pawn = PawnActionManager.Instance.GetSelectedPawn();
        if(pawn != null)
        {
            pawnName.text = pawn.GetType().Name;
            pawnParams.text = 
           "Element: " + pawn.GetElementType().ToString() + "\n" +
           "Health:  " + pawn.GetHealth().ToString() + " / " + pawn.GetMaxHealth().ToString() + "\n" +
           "Attck:   " + pawn.GetCurrentAttack().ToString() + "\n" +
           "Defense: " + pawn.GetCurrentDefense().ToString() + "\n" +
           "CounterAttack: " + pawn.GetCounterAttack().ToString() + "\n" +
           "Attck Range: " + pawn.GetAttackRange().ToString() + "\n" +
           "Move Range:  " + pawn.GetMoveRange().ToString() + "\n" +
           "Impose Element: " + pawn.GetImposeElement().ToString() + "\n" +
           "Affected Element: " + pawn.GetAffectedElement().ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
