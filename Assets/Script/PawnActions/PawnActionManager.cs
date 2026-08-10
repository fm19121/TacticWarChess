using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PawnActionManager : MonoBehaviour
{

    public event Action OnSelectedPawnChanged;
    public event Action OnSelectedActionChanged;
    public event Action OnSpenActionPoint;
    public event Action OnIsTakingActionChange;
    public static PawnActionManager Instance { get; private set; }
    private Pawn selectedPawn;
    private BaseAction currentAction;

    private bool isTakingAction;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        OnSpenActionPoint += UpgradeAttackAction;
        BattleManager.OnBuffRemoved += BackToBaseAttack;
        isTakingAction = false;
    }

    void Update()
    {
        if (isTakingAction)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!TurnManager.Instance.IsPlayerTurn())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            SelectPawn();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedPawn != null && !selectedPawn.IsEnemy())
            {
                GridPosition mousePos = GridLevel.Instance.GetGridPosition(Cursor.GetPosition());
                if (currentAction.IsInActionRegion(mousePos) && currentAction.CanTakeAction())
                {
                    IsTakingAction();
                    currentAction.TakeAction(mousePos);
                    selectedPawn.SpendActionPoint(currentAction.GetActionCost());
                    TurnManager.Instance.SpendTurnActionPoint(currentAction.GetActionCost());
                    SpendActionPoint();
                }
            }
        }
    }

    private void SelectPawn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Pawn")))
        {
            Pawn pawn = hit.transform.GetComponent<Pawn>();
            SetSelectedPawn(pawn);
        }
    }

    private void SetSelectedPawn(Pawn newPawn)
    {
        selectedPawn = newPawn;
        SetCurrentAction(selectedPawn.GetPawnMovement());
        OnSelectedPawnChanged?.Invoke();
    }
    public Pawn GetSelectedPawn()
    {
        return selectedPawn;
    }

    public BaseAction GetCurrentAction()
    {
        return currentAction;
    }

    public void SpendActionPoint()
    {
        OnSpenActionPoint?.Invoke();
    }
    public void SetCurrentAction(BaseAction action)
    {
        currentAction = action;

        OnSelectedActionChanged?.Invoke();
    }

    public void IsTakingAction()
    {
        isTakingAction = true;
        OnIsTakingActionChange?.Invoke();
    }

    public void FinishAction()
    {
        isTakingAction = false;
        OnIsTakingActionChange?.Invoke();
    }

    public bool GetIsTakingAction()
    {
        return isTakingAction;
    }
    private void UpgradeAttackAction()
    {
        if(selectedPawn == null)
        {
            return;
        }
        List<BuffObject> buffs = selectedPawn.GetPawnBuffs();
        if (buffs == null)
        {
            return;
        }
        List<BaseAction> pawnActions = selectedPawn.GetPawnActions();
        BuffObject lightArrow = new BuffObject(BuffObject.BuffName.LightArrow);
        BuffObject FatalAttack = new BuffObject(BuffObject.BuffName.FatalDarkAttack);
        if (buffs.Contains(lightArrow))
        {
            int i = pawnActions.FindIndex(action => action is PawnAttack);
            if (i >= 0)
            {
                pawnActions[i] = selectedPawn.GetComponent<LightArrowAttack>();
                pawnActions[i].enabled = true;
            }
        }
        else if(buffs.Contains(FatalAttack))
        {
            int i = pawnActions.FindIndex(action => action is PawnAttack);
            if (i >= 0)
            {
                pawnActions[i] = selectedPawn.GetComponent<DarkFatalAttack>();
                pawnActions[i].enabled = true;
            }
        }
    }

    private void BackToBaseAttack()
    {
        if (selectedPawn == null)
        {
            return;
        }
        List<BuffObject> buffs = selectedPawn.GetPawnBuffs();
        if (buffs == null)
        {
            return;
        }
        List<BaseAction> pawnActions = selectedPawn.GetPawnActions();
        int i = pawnActions.FindIndex(action => action is LightArrowAttack);
        if(i >= 0)
        {
            pawnActions[i] = selectedPawn.GetComponent<PawnAttack>();
        }
        else
        {
            i = pawnActions.FindIndex(action => action is DarkFatalAttack);
            if( i >= 0)
            {
                pawnActions[i] = selectedPawn.GetComponent<PawnAttack>();
            }
        }
    }
}
