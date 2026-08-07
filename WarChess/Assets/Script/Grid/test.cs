using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Pawn unit;
    void Start()
    {
      // unit = GetComponent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {
       // GridDisplay.Instance.RenderDefaultGrid();
        if (Input.GetKeyDown(KeyCode.T))
        {
           // GridDisplay.Instance.RenderReachableRegions(unit.GetPawnMovement().GetReachableRegion(unit));
        }
    }
}
