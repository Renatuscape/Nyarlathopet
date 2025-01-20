using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapNav : MonoBehaviour
{
    public Button btnNorth;
    public Button btnEast;
    public Button btnSouth;
    public Button btnWest;

    RectTransform worldMap;

    int maxY;
    int maxX;
    int minY;
    int minX;

    int incrementMultiplier = 6;
    int increment;

    public void SetUpButtons(GameObject worldMap, int columns, int rows, int cellSize)
    {
        this.worldMap = worldMap.GetComponent<RectTransform>();
        minY = -(Mathf.FloorToInt(rows / 3 * cellSize) + 96);
        maxY = Mathf.FloorToInt(rows / 3 * cellSize) + cellSize;
        maxX = Mathf.FloorToInt(columns / 3 * cellSize) + cellSize;
        minX = -(Mathf.FloorToInt(columns / 3 * cellSize) + 224);
        increment = cellSize * incrementMultiplier;

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
            PanMap(new(newPos, worldMap.anchoredPosition.y));
        }
        else
        {
            PanMap(new(maxX, worldMap.anchoredPosition.y));
        }
    }

    void GoEast()
    {
        int current = (int)worldMap.anchoredPosition.x;
        int newPos = current - increment;

        if (newPos > minX)
        {
            PanMap(new(newPos, worldMap.anchoredPosition.y));
        }
        else
        {
            PanMap(new(minX, worldMap.anchoredPosition.y));
        }
    }

    void GoNorth()
    {
        int current = (int)worldMap.anchoredPosition.y;
        int newPos = current - increment;

        if (newPos > minY)
        {
            PanMap(new(worldMap.anchoredPosition.x, newPos));
        }
        else
        {
            PanMap(new(worldMap.anchoredPosition.x, minY));
        }
    }

    void GoSouth()
    {
        int current = (int)worldMap.anchoredPosition.y;
        int newPos = current + increment;

        if (newPos <= maxY)
        {
            PanMap(new(worldMap.anchoredPosition.x, newPos));
        }
        else
        {
            PanMap(new(worldMap.anchoredPosition.x, maxY));
        }
    }

    public void PanMap(Vector2 newPosition)
    {
        StopAllCoroutines();
        StartCoroutine(PanMapCoroutine(newPosition));
    }

    private IEnumerator PanMapCoroutine(Vector2 newPosition)
    {
        float elapsedTime = 0;
        float duration = 0.1f; // Adjust duration as needed
        Vector2 startPosition = worldMap.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // Ensure t is always between 0 and 1
            worldMap.anchoredPosition = Vector2.Lerp(startPosition, newPosition, t);
            yield return null;
        }

        worldMap.anchoredPosition = newPosition; // Ensure the final position is set
    }
}
