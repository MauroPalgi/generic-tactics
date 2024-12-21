using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    // Start is called before the first frame update
    TacticGrid grid;

    [SerializeField]
    GameObject movePoint;

    [SerializeField]
    Vector2Int testTargetPosition;

    [SerializeField]
    List<Vector2Int> testListTargetPositions;

    [SerializeField]
    GameObject movePointContainer;

    List<GameObject> movePointGOs;

    void Start()
    {
        grid = GetComponent<TacticGrid>();
        movePointGOs = new List<GameObject>();
        Highlight(testListTargetPositions);
    }

    private GameObject CreateMovePointHighlightObject()
    {
        GameObject go = Instantiate(movePoint);
        movePointGOs.Add(go);
        go.transform.SetParent(movePointContainer.transform);
        return go;
    }

    public void Highlight(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetMovePointGO(i));
        }
    }

    private GameObject GetMovePointGO(int i)
    {
        if (movePointGOs.Count < i)
        {
            return movePointGOs[i];
        }
        return CreateMovePointHighlightObject();
    }

    private void Highlight(int x, int y, GameObject highlightObject)
    {
        Vector3 position = grid.GetWorldPosition(x, y, true);
        position += Vector3.up * 0.2f;
        highlightObject.transform.position = position;
    }

    // Update is called once per frame
    void Update() { }
}
