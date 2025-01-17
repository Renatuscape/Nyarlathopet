using UnityEngine;
using UnityEngine.UI;

public class MapNav : MonoBehaviour
{
    int increment = 3;
    public Button btnNorth;
    public Button btnEast;
    public Button btnSouth;
    public Button btnWest;

    RectTransform worldMap;
    int maxY;
    int maxX;

    public void SetUpButtons(GameObject worldMap, int columns, int rows, int cellSize)
    {
        this.worldMap = worldMap.GetComponent<RectTransform>();
        maxY = Mathf.FloorToInt(rows / 3 * cellSize) + cellSize;
        maxX = Mathf.FloorToInt(columns / 3 * cellSize) + cellSize;
        increment = cellSize * 3;

        btnEast.onClick.AddListener(GoEast);
        btnWest.onClick.AddListener(GoWest);
        btnNorth.onClick.AddListener(GoNorth);
        btnSouth.onClick.AddListener(GoSouth);

        Report.Write(name, "Movement increment set to " + increment);
    }

    void GoWest()
    {
        int current = (int)worldMap.anchoredPosition.x;
        int newPos = current + increment;

        if (newPos <= maxX)
        {
            worldMap.anchoredPosition = new(newPos, worldMap.anchoredPosition.y);
        }
        else
        {
            worldMap.anchoredPosition = new(maxX, worldMap.anchoredPosition.y);
        }
    }

    void GoEast()
    {
        int current = (int)worldMap.anchoredPosition.x;
        int newPos = current - increment;

        if (newPos > -maxX)
        {
            worldMap.anchoredPosition = new(newPos, worldMap.anchoredPosition.y);
        }
        else
        {
            worldMap.anchoredPosition = new(-maxX, worldMap.anchoredPosition.y);
        }
    }

    void GoNorth()
    {
        int current = (int)worldMap.anchoredPosition.y;
        int newPos = current - increment;

        if (newPos > -maxY)
        {
            worldMap.anchoredPosition = new(worldMap.anchoredPosition.x, newPos);
        }
        else
        {
            worldMap.anchoredPosition = new(worldMap.anchoredPosition.x, -maxY);
        }
    }

    void GoSouth()
    {
        int current = (int)worldMap.anchoredPosition.y;
        int newPos = current + increment;

        if (newPos <= maxY)
        {
            worldMap.anchoredPosition = new(worldMap.anchoredPosition.x, newPos);
        }
        else
        {
            worldMap.anchoredPosition = new(worldMap.anchoredPosition.x, maxY);
        }
    }
}
