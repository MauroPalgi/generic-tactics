using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;

    List<Vector3> pathWorldPosition;
    CharacterAnimator characterAnimator;

    [SerializeField] float moveSpeed = 1f;
    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    internal void Move(List<PathNode> path)
    {
        pathWorldPosition = gridObject.targetGrid.ConvertPathNodesToWorlPosition(path);
        gridObject.positionOnGrid.x = path[path.Count - 1].pos_x;
        gridObject.positionOnGrid.y = path[path.Count - 1].pos_y;
        RotateCharacter();
        characterAnimator.StarMoving();

    }

    private void RotateCharacter()
    {
        Vector3 direction = (pathWorldPosition[0] - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (pathWorldPosition == null)
        {
            return;
        }
        if (pathWorldPosition.Count == 0)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, pathWorldPosition[0], moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathWorldPosition[0]) < 0.05f)
        {
            pathWorldPosition.RemoveAt(0);
            if (pathWorldPosition.Count == 0)
            {
                characterAnimator.StopMoving();
            }
            else
            {
                RotateCharacter();
            }
        }
    }
}
