using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PawnInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pawnActionPoint;
    [SerializeField] private Pawn pawn;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image affectedElement;
    void Start()
    {
        UpdatePawnActionPoint();
        UpdateHealthBar();
        UpdateAffectedElement();
        PawnActionManager.Instance.OnSpenActionPoint += UpdatePawnActionPoint;
        TurnManager.Instance.ToNextTurn += UpdatePawnActionPoint;
        pawn.OnTakenDamage += UpdateHealthBar;
        PawnActionManager.Instance.OnSpenActionPoint += UpdateAffectedElement;
        TurnManager.Instance.ToNextTurn += UpdateAffectedElement;
    }

    void Update()
    {
    }

    private void UpdatePawnActionPoint()
    {
        pawnActionPoint.text = pawn.GetActionPoint().ToString();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = pawn.GetHealth() / pawn.GetMaxHealth();
    }

    private void UpdateAffectedElement()
    {
        switch (pawn.GetAffectedElement()) 
        {
            case ElementType.None:
                affectedElement.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                break;
            case ElementType.Light:
                affectedElement.color = new Color(0.9f, 0.5f, 0.1f, 1.0f);
                break;
            case ElementType.Dark:
                affectedElement.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                break;
        }

    }
}
