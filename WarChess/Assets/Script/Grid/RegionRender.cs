using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RegionRender : MonoBehaviour
{

    [SerializeField] private MeshRenderer gridUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render()
    {
        gridUI.enabled = true;
    }

    public void Hide()
    {
        gridUI.enabled = false;
    }

    public void Default()
    {
        gridUI.material.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
        gridUI.enabled = true;
    }

    public void HighlightReachableRegion(BaseAction action)
    {
        switch (action) 
        {
            case PawnMovement:
                gridUI.material.color = new Color(0.1f, 0.15f, 0.7f, 0.7f);
                break;
            case PawnAttack:
                gridUI.material.color = new Color(0.8f, 0.2f, 0.3f, 0.7f);
                break;
            default:
                gridUI.material.color = new Color(0.8f, 0.6f, 0.2f, 0.7f);
                break;
        }
    }

    public void HighlightActionRangeRegion(BaseAction action)
    {
        switch (action)
        {
            case PawnMovement:
                gridUI.material.color = new Color(0.25f, 0.1f, 0.6f, 0.4f);
                break;
            case PawnAttack:
                gridUI.material.color = new Color(0.6f, 0.2f, 0.2f, 0.4f);
                break;
            default:
                gridUI.material.color = new Color(0.5f, 0.4f, 0.2f, 0.4f);
                break;
        }
    }

    public void DisableActionRegion()
    {
        gridUI.material.color = new Color(0.2f, 0.2f, 0.2f, 0.7f); ;
    }
}
