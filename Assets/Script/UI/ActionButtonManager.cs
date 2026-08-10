using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonManager : MonoBehaviour
{
    [SerializeField] private Transform actionButtonUI;
    [SerializeField] private Transform actionButtonContainer;

    void Start()
    {
        CleanActionButtons();
        PawnActionManager.Instance.OnSelectedPawnChanged += UpdateActionButtons;
        PawnActionManager.Instance.OnSelectedActionChanged += UpdateActionButtons;
        PawnActionManager.Instance.OnSpenActionPoint += UpdateActionButtons;
        PawnActionManager.Instance.OnIsTakingActionChange += UpdateActionButtons;
        TurnManager.Instance.ToNextTurn += UpdateActionButtons;
    }

    private void CreatePawnActionButtons()
    {
        Pawn selectedPawn = PawnActionManager.Instance.GetSelectedPawn();
        if (selectedPawn != null)
        {
            foreach (BaseAction pawnAction in selectedPawn.GetPawnActions())
            {
                Transform buttonTrans = Instantiate(actionButtonUI, actionButtonContainer);
                ActionButtonUI buttonUI = buttonTrans.GetComponent<ActionButtonUI>(); 
                buttonUI.SetActionButton(pawnAction);
            }
        }
    }

    private void CleanActionButtons()
    {
        foreach (Transform buttonTrans in actionButtonContainer)
        {
            Destroy(buttonTrans.gameObject);
        }
    }

    private void UpdateActionButtons()
    {
        CleanActionButtons();
        CreatePawnActionButtons();
    }
}
