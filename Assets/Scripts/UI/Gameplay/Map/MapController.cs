using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject worldMap;
    public GameObject cellPrefab;
    public MapNav mapNav;

    int cellSize = 32; // Size of each cell in pixels
    int columns = 60;
    int rows = 45;

    GameObject[,] cells;

    void Awake()
    {
        mapNav.SetUpButtons(worldMap, columns, rows, cellSize);

        Vector2 topLeft = new Vector2((-columns / 2f * cellSize) + (cellSize * 0.5f), (rows / 2f * cellSize) - (cellSize * 0.5f));

        cells = new GameObject[columns, 45];

        for (int x = 0; x < 60; x++)
        {
            for (int y = 0; y < 45; y++)
            {
                GameObject cell = Instantiate(cellPrefab, worldMap.transform);
                RectTransform rectTransform = cell.GetComponent<RectTransform>();

                // Position relative to top-left corner
                rectTransform.anchoredPosition = new Vector2((x * cellSize) + (int)topLeft.x, (-y * cellSize) + (int)topLeft.y);

                // Set size
                rectTransform.sizeDelta = new Vector2(cellSize, cellSize);

                // Optional: name the cell for debugging
                cell.name = $"Cell_{x}_{y}";

                cells[x, y] = cell;

                SetUpCell(cell, x, y);
            }
        }
    }

    public void SetUpCell(GameObject cell, int x, int y)
    {
        var script = cell.GetComponent<MapCellPrefab>();

        var location = Repository.GetLocationByCoordinates((x, y));

        script.Initialise(location, (x, y));
    }

    // Helper method to get cell at specific coordinates
    public GameObject GetCell(int x, int y)
    {
        if (x >= 0 && x < 60 && y >= 0 && y < 45)
            return cells[x, y];
        return null;
    }
}
