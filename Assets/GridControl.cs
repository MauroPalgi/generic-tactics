using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayerMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
                if (gridObject == null)
                {
                    Debug.Log(gridPosition.x + " - " + gridPosition.y);
                }else
                {
                    Debug.Log(gridObject.name);
                }
            }
        }
    }

}
