using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    [SerializeField] private GameObject regionUI;

    public static GridDisplay Instance { get; private set; }

    private RegionRender[,] regionRenders;

    private void Awake()
    {
        if(Instance  != null)
        {
            Debug.LogError("more than one GridDisplay");
            Destroy(Instance.gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        regionRenders = new RegionRender[GridLevel.Instance.GetGridWidth(), GridLevel.Instance.GetGridHeight()];
        for(int x = 0; x < GridLevel.Instance.GetGridWidth(); x++)
        {
            for(int z = 0; z < GridLevel.Instance.GetGridHeight(); z++)
            {
                GridPosition gridPos = new GridPosition(x, z);
                GameObject region = Instantiate(regionUI, GridLevel.Instance.GetWorldPosition(gridPos), Quaternion.identity);
                regionRenders[x, z] = region.GetComponent<RegionRender>();
            }
        }

        //PawnActionManager.Instance.OnSelectedPawnChanged += GridUpdate;
        //PawnActionManager.Instance.OnSelectedActionChanged += GridUpdate;
    }

    private void GridUpdate()
    {
        HideGrid();
        RenderDefaultGrid();
        UpdateGridColor();
    }

    void Update() 
    {
        GridUpdate();
    }

    private void RenderDefaultGrid()
    {
        for (int x = 0; x < GridLevel.Instance.GetGridWidth(); x++)
        {
            for (int z = 0; z < GridLevel.Instance.GetGridHeight(); z++)
            {
                regionRenders[x, z].Default();
            }
        }
    }

    private void HideGrid()
    {
        for (int x = 0; x < GridLevel.Instance.GetGridWidth(); x++)
        {
            for (int z = 0; z < GridLevel.Instance.GetGridHeight(); z++)
            {
                regionRenders[x, z].Hide();
            }
        }
    }
    private void RenderReachableRegions(List<GridPosition> reachableRegions, BaseAction action)
    {
        foreach(GridPosition reachableRegion in reachableRegions)
        {
            regionRenders[reachableRegion.x, reachableRegion.z].HighlightReachableRegion(action);
        }
    }

    private void RenderActionRangeRegions(List<GridPosition> reachableRegions, BaseAction action)
    {
        foreach (GridPosition reachableRegion in reachableRegions)
        {
            regionRenders[reachableRegion.x, reachableRegion.z].HighlightActionRangeRegion(action);
        }
    }
    private void UpdateGridColor()
    {
        BaseAction action = PawnActionManager.Instance.GetCurrentAction();
        if (action)
        {
            if (!action.CanTakeAction() || action.GetPawn().IsEnemy())
            {
                RenderUnavailableActionRegion(action.GetActionRangeRegion());
                return;
            }
            RenderActionRangeRegions(action.GetActionRangeRegion(), action);
            RenderReachableRegions(action.GetActionRegion(), action);
        }
    }

    private void RenderUnavailableActionRegion(List<GridPosition> reachableRegions)
    {
        foreach (GridPosition reachableRegion in reachableRegions)
        {
            regionRenders[reachableRegion.x, reachableRegion.z].DisableActionRegion();
        }
    }
}
