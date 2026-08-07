using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionActivating : MonoBehaviour
{
    void Start()
    {
        PawnActionManager.Instance.OnIsTakingActionChange += UpdateActionStateUI;
        TurnManager.Instance.OnPlayerTurnEnd += UpdateActionStateUI;
        TurnManager.Instance.ToNextTurn += UpdateActionStateUI;

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateActionStateUI()
    {
        bool isTakingAction = PawnActionManager.Instance.GetIsTakingAction();
        if (TurnManager.Instance.IsPlayerTurn())
        {
            if (isTakingAction)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
        else
        {
            Show();
        }
    }
}
