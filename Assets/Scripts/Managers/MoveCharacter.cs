using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TacticGrid targetGrid;
    [SerializeField] LayerMask terrainLayerMask;
    [SerializeField] GridObject targetCharacter;

    PathFinder pathFinder;
    List<PathNode> path;
    private void Start()
    {
        pathFinder = targetGrid.GetComponent<PathFinder>();
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
                Debug.Log(gridPosition);
                path = pathFinder.FindPath(
                    targetCharacter.positionOnGrid.x,
                    targetCharacter.positionOnGrid.y,
                    gridPosition.x,
                    gridPosition.y
                );
                if (path == null)
                {
                    return;
                }
                if (path.Count == 0)
                {
                    return;
                }

                targetCharacter.GetComponent<Movement>().Move(path);
            }
        }
    }
}
