using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;
    [SerializeField] private int width = 25;
    [SerializeField] private int length = 25;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;



    private void Awake()
    {
        GenerateGrid();
    }


    private void GenerateGrid()
    {
        grid = new Node[length, width];
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                grid[x, y] = new Node();
            }
        }
        CheckPassableTerrain();
    }
    private void CheckPassableTerrain()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 worldPos = GetWorldPosition(x, y);
                bool passable = !Physics.CheckBox(worldPos, Vector3.one / 2, Quaternion.identity, obstacleLayer);
                grid[x, y] = new Node();
                grid[x, y].passable = passable;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (grid == null)
        {
            return;
        }

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 pos = GetWorldPosition(x, y);
                Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
                Gizmos.DrawCube(pos, Vector3.one / 5);
            }
        }

    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(transform.position.x + (x * cellSize), 0f, transform.position.z + (y * cellSize));
    }


    public bool CheckBoundry(Vector2Int positionOnGrid)
    {
        if (positionOnGrid.x < 0 || positionOnGrid.x >= length)
        {
            return false;
        }
        if (positionOnGrid.y < 0 || positionOnGrid.y >= width)
        {
            return false;
        }
        return true;

    }
    internal Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        worldPosition -= transform.position;
        Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.y / cellSize));
        return positionOnGrid;
    }

    internal void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(positionOnGrid))
        {
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
        }else{
            Debug.Log("OUT OF BOUNDRIES");
        }
    }

    internal GridObject GetPlacedObject(Vector2Int gridPosition)
    {
        return grid[gridPosition.x, gridPosition.y].gridObject;
    }
}
