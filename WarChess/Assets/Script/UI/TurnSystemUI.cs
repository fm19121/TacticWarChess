using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI turnNumText;
    [SerializeField] private TextMeshProUGUI turnActionPoint;
    [SerializeField] private Image environmentUI;
    [SerializeField] private Sprite Light;
    [SerializeField] private Sprite Dark;

    private void Start()
    {
        SetNextTurnButton();
        SetTurnText();
        SetTurnActionPoint();
        SetEnironmnetUI();

        TurnManager.Instance.ToNextTurn += SetTurnText;
        TurnManager.Instance.ToNextTurn += SetTurnActionPoint;
        TurnManager.Instance.ToNextTurn += SetEnironmnetUI;
        PawnActionManager.Instance.OnSpenActionPoint += SetTurnActionPoint;
    }
    private void SetNextTurnButton()
    {
        button.onClick.AddListener(TurnManager.Instance.EndPlayerTurn);
    }

    private void SetTurnText()
    {
        turnNumText.text = "Turn: " + TurnManager.Instance.GetTurnNumber();
    }

    private void SetTurnActionPoint()
    {
        turnActionPoint.text = "Action Point: " + TurnManager.Instance.GetTurnActionPoint();
    }

    private void SetEnironmnetUI()
    {
       ElementType environment =  EnvironmentSystem.Instance.GetCurrentEnvironmentElement();
        if(environment == ElementType.Light)
        {
            environmentUI.sprite = Light;
        }
        else
        {
            environmentUI.sprite = Dark;
        }
    }
}
