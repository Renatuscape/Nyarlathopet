using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Loads all JSON data and proceeds to either NewGame or MainMenu depending on existing data
public class GameLoader : MonoBehaviour
{
    public Slider loadBar;
    public RandomNameLoader randomNameLoader;
    public SaveDataLoader saveDataLoader;
    public HorrorLoader horrorLoader;
    public LocationLoader locationLoader;
    public TextMeshProUGUI loaderDescription;

    void Start()
    {
        Report.Write(name, "Starting load routine.");
        StartCoroutine(LoadRoutine());
    }

    IEnumerator LoadRoutine()
    {
        // Set up loadbar
        loadBar.value = 0;
        loadBar.maxValue = 4; // Should equal amount of loaders. Find way to automate this

        // Load components
        loaderDescription.text = "Finding locations ...";
        yield return StartCoroutine(locationLoader.LoadData());
        Report.Write(name, "Loaded locations from JSON.");
        loadBar.value++;

        loaderDescription.text = "Perceiving horrors ...";
        yield return StartCoroutine(horrorLoader.LoadData());
        Report.Write(name, "Loaded horrors from JSON.");
        loadBar.value++;

        loaderDescription.text = "Transcribing human names ...";
        yield return StartCoroutine(randomNameLoader.LoadData());
        Report.Write(name, "Loaded human names from JSON.");
        loadBar.value++;

        loaderDescription.text = "Fetching cult records ...";
        yield return StartCoroutine(saveDataLoader.LoadData());
        Report.Write(name, "Loaded save data from JSON.");
        loadBar.value++;

        yield return new WaitForSeconds(1); // emulate loading. Replace with real loader
        loadBar.value++;


        Report.Write(name, "Completed load routine. Player data " + (Player.Data == null ? "does not exist. Loading NewGame." : "exists. Loading MainMenu."));

        if (Player.Data != null)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("NewGame");
        }
    }
}
