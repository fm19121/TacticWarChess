using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;

    public void SetActionButton(BaseAction action)
    {
        if(action == null)
        {
            Debug.Log("Null Action");
            return;
        }
        buttonText.text = action.GetActionName() + "\n" + "Cost: " + action.GetActionCost();
        if (!action.CanTakeAction())
        {
            DisableButton();
            return;
        }
        if(action == PawnActionManager.Instance.GetCurrentAction())
        {
            BeSelected();
        }
        button.onClick.AddListener(() => { 
            PawnActionManager.Instance.SetCurrentAction(action);
        });
    }

    private void BeSelected()
    {
        button.image.color = new Color(0.3f, 0.8f, 0.2f, 1.0f);
    }

    private void DisableButton()
    {
        button.image.color = new Color(0.2f, 0.2f, 0.2f, 1.0f);
        buttonText.color = new Color(0.45f, 0.4f, 0.4f, 1.0f);
        button.interactable = false;
    }
}
