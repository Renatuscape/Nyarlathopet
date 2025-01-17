using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapCellPrefab : MonoBehaviour
{
    public Location location;
    public Button button;
    public TextMeshProUGUI textMesh;
    public Image image;
    public (int x, int y) coords;

    public void Initialise(Location location, (int x, int y) coords)
    {
        this.location = location;
        this.coords = coords;

        button.onClick.AddListener(() =>
        {
            Report.Write("MapCell", $"{GetComponent<RectTransform>().position.x},{GetComponent<RectTransform>().position.x} {(location == null ? "Empty Cell" : location.name)}");
        });

        if (location != null && !string.IsNullOrEmpty(location.name))
        {
            textMesh.text = location.name[0].ToString();
            button.onClick.AddListener(() => MapManager.SetDestination(location));
        }
        else
        {
            textMesh.text = "";
        }
    }
}
