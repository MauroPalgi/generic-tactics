using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] Grid targetGrid;
    [SerializeField] LayerMask terrainLayerMask;

    PathFinder pathFinder;
    Vector2Int currentPosition = new Vector2Int();

    List<PathNode> path;
    private void Start()
    {
        pathFinder = targetGrid.GetComponent<PathFinder>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                path = pathFinder.FindPath(currentPosition.x, currentPosition.y, gridPosition.x, gridPosition.y);

                currentPosition = gridPosition;
                /*
                GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
                if (gridObject == null)
                {
                    Debug.Log(gridPosition.x + " - " + gridPosition.y);
                }else
                {
                    Debug.Log(gridObject.name);
                }
                */
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (path == null)
        {
            return;
        }

        for (int i = 0; i < path.Count - 1; i++)
        {
            Gizmos.DrawLine(targetGrid.GetWorldPosition(path[i].pos_x, path[i].pos_y, true), targetGrid.GetWorldPosition(path[i + 1].pos_x, path[i + 1].pos_y, true));
        }
    }

}
