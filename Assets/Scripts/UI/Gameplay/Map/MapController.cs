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

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var location = Repository.GetLocationByCoordinates((x, y));

                if (location != null)
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

                    SetUpCell(cell, x, y, location);
                }
            }
        }
    }

    public void SetUpCell(GameObject cell, int x, int y, Location location)
    {
        var script = cell.GetComponent<MapCellPrefab>();
        script.Initialise(location, (x, y));
    }
}
