using System.Collections;
using System.Collections.Generic;
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

    private void Start()
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
            infoMesh.text = $"Lv.{currentLocation.level} {currentLocation.type.ToString()}\n\n{currentLocation.description}\n\n{(currentLocation.hasLore ? "LOR+ " : "")}{(currentLocation.hasMagick ? "MGC+ " : "")}{(currentLocation.hasStrength ? "STR+ " : "")}{(currentLocation.isRisky ? "\n\nRisky endeavour!" : "")}";

            btnSeek.gameObject.SetActive(true);
            btnRecruit.gameObject.SetActive(true);
            btnThwart.gameObject.SetActive(true);
        }
        else
        {
            infoTitle.text = "Endeavour";
            infoMesh.text = "Select a destination to proceed with your endeavour. Risky locations yield higher rewards.";

            btnSeek.gameObject.SetActive(false);
            btnRecruit.gameObject.SetActive(false);
            btnThwart.gameObject.SetActive(false);
        }
    }

    void BtnSeek()
    {
        if (currentLocation == null)
        {
            AlertSystem.Print("Select your destination on the map.");
        }
        else
        {
            AlertSystem.Prompt($"Start an endeavour to seek hidden artefacts in {currentLocation.name}?", () =>
            {
                ExplorationManager.SeekArtefacts(currentLocation);
            });
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
            AlertSystem.Print("Select your destination on the map.");
        }
        else
        {
            AlertSystem.Prompt($"Start an endeavour to recruit members in {currentLocation.name}?", () =>
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
