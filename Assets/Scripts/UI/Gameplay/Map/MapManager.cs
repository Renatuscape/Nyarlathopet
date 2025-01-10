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
    public Location currentLocation = new() { name = "Amsterdam", hasMagick = true};

    public Button btnSeek;
    public Button btnRecruit;
    public Button btnThwart;

    private void Start()
    {
        instance = this;
        btnSeek.onClick.AddListener(() => BtnSeek());
        btnThwart.onClick.AddListener(() => BtnThwart());
        btnRecruit.onClick.AddListener(() => BtnRecruit());
    }


    private void OnEnable()
    {
        RefreshInfo();
    }

    void RefreshInfo()
    {
        if (currentLocation != null && !string.IsNullOrEmpty(currentLocation.name))
        {
            infoTitle.text = (currentLocation.isRisky ? "!" : "") + currentLocation.name + (currentLocation.isRisky ? "!" : "");
            infoMesh.text = currentLocation.description;

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
