using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public TextMeshProUGUI infoTitle;
    public TextMeshProUGUI infoMesh;
    public Location currentLocation;

    public Button btnSeek;
    public Button btnRecruit;
    public Button btnThwart;

    private void Awake()
    {
        instance = this;
        btnSeek.onClick.AddListener(() => BtnSeek());
        btnThwart.onClick.AddListener(() => BtnThwart());
        btnRecruit.onClick.AddListener(() => BtnRecruit());
        currentLocation = null;
    }

    private void OnEnable()
    {
        RefreshInfo();
    }

    void RefreshInfo()
    {
        if (currentLocation != null && !string.IsNullOrEmpty(currentLocation.name))
        {
            infoTitle.text = currentLocation.name;
            infoMesh.text = $"Lv.{currentLocation.level} {currentLocation.type.ToString()}\n\n{currentLocation.description}\n\n{(currentLocation.hasLore ? $"{Tags.Get("LOR")} " : "")}{(currentLocation.hasOccultism ? $"{Tags.Get("OCL")} " : "")}{(currentLocation.hasStrength ? $"{Tags.Get("STR")} " : "")}{(currentLocation.isRisky ? $"\n\n{Tags.Get("RiskyE")}" : "")}";

            btnSeek.gameObject.SetActive(true);
            btnRecruit.gameObject.SetActive(true);
            btnThwart.gameObject.SetActive(true);
        }
        else
        {
            infoTitle.text = Text.Get("MAP-INFT");
            infoMesh.text = Text.Get("MAP-INFD");

            btnSeek.gameObject.SetActive(false);
            btnRecruit.gameObject.SetActive(false);
            btnThwart.gameObject.SetActive(false);
        }
    }

    void BtnSeek()
    {
        if (currentLocation == null)
        {
            AlertSystem.Print(Text.Get("MAP-NSEL"));
        }
        else
        {
            ExplorationArtefacts.PromptArtefactOptions(currentLocation);
        }
    }

    void BtnThwart()
    {
        AlertSystem.Prompt($"Expend your cultists and connections to thwart your enemies?", () =>
        {
            ExplorationManager.ThwartEnemies();
        });
    }

    void BtnRecruit()
    {
        if (currentLocation == null)
        {
            AlertSystem.Print(Text.Get("MAP-NSEL"));
        }
        else
        {
            AlertSystem.Prompt($"{Text.Get("MAP-RECR")} {currentLocation.name}?", () =>
            {
                ExplorationManager.RecruitMembers(currentLocation);
            });
        }
    }

    public static void SetDestination(Location location)
    {
        instance.currentLocation = location;
        instance.RefreshInfo();
    }
}
