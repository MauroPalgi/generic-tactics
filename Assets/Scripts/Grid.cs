using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;
    public int width = 100;
    public int length = 100;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask terrainLayer;

    private void Awake()
    {
        Debug.Log("Awaker");
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

        CalculateElevation();
        CheckPassableTerrain();
    }

    private void CalculateElevation()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                // Origen del rayo
                Vector3 rayOrigin = GetWorldPosition(x, y) + Vector3.up * 10f;
                // Dirección del rayo
                Vector3 rayDirection = Vector3.down;
                Ray ray = new Ray(rayOrigin, rayDirection);

                // Dibujar el raycast (línea desde el origen hasta el límite del rayo)
                // Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * 20f, Color.red, 10f);

                // Realizar el raycast
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 20f, terrainLayer))
                {
                    grid[x, y].elevation = hit.point.y;

                    // Opcional: Dibujar un punto donde el raycast golpea
                    // Debug.DrawLine(hit.point, hit.point + Vector3.up * 0.5f, Color.green, 2f);
                }
                if (Physics.Raycast(ray, out hit, 20f, obstacleLayer))
                {
                    grid[x, y].elevation = hit.point.y;

                    // Opcional: Dibujar un punto donde el raycast golpea
                    // Debug.DrawLine(hit.point, hit.point + Vector3.up * 0.5f, Color.green, 2f);
                }
            }
        }
    }

    private void CheckPassableTerrain()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Vector3 worldPos = GetWorldPosition(x, y);
                bool passable = !Physics.CheckBox(worldPos, Vector3.one / 2, Quaternion.identity, obstacleLayer);
                grid[x, y].passable = passable;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.Log("OnDrawGizmos");

        if (grid == null)
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, y);
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(pos, Vector3.one * cellSize * 0.1f);
                }
            }
        }
        else
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 pos = GetWorldPosition(x, y, true);
                    Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
                    Gizmos.DrawCube(pos, Vector3.one * cellSize * 0.1f);
                }
            }
        }

    }

    public Vector3 GetWorldPosition(int x, int y, bool elevation = false)
    {
        Vector3 worldPosition = new Vector3(x * cellSize, elevation == true ? grid[x, y].elevation : 0f, y * cellSize) + transform.position;
        return worldPosition;
    }

    public bool CheckBoundry(Vector2Int positionOnGrid)
    {
        return positionOnGrid.x >= 0 && positionOnGrid.x < length && positionOnGrid.y >= 0 && positionOnGrid.y < width;
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        worldPosition -= transform.position;
        return new Vector2Int(
            Mathf.FloorToInt(worldPosition.x / cellSize),
            Mathf.FloorToInt(worldPosition.z / cellSize)
        );
    }

    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(positionOnGrid))
        {
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
        }
        else
        {
            Debug.LogError("OUT OF BOUNDARIES");
        }
    }

    public GridObject GetPlacedObject(Vector2Int gridPosition)
    {
        return CheckBoundry(gridPosition) ? grid[gridPosition.x, gridPosition.y].gridObject : null;
    }

    public void LogAllNodes()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Debug.Log(grid[x, y].ToString());
            }
        }
    }

    public bool CheckBoundry(int x, int y)
    {
        return CheckBoundry(new Vector2Int(x, y));
    }

    public bool CheckWalkable(int pos_x, int pos_y)
    {
        return grid[pos_x, pos_y].passable;
    }
}
