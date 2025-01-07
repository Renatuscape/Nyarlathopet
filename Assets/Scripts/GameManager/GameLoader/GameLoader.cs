
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public Slider loadBar;
    public HumanNameLoader humanNameLoader;
    public SaveDataLoader saveDataLoader;
    public TextMeshProUGUI loaderDescription;

    void Start()
    {
        StartCoroutine(LoadRoutine());
    }

    IEnumerator LoadRoutine()
    {
        // Set up loadbar
        loadBar.value = 0;
        loadBar.maxValue = 3; // Should equal amount of loaders. Find way to automate this

        // Load components
        loaderDescription.text = "Transcribing human names...";
        yield return StartCoroutine(humanNameLoader.LoadData());
        Report.Write(name, "Loaded human names from JSON.");
        loadBar.value++;

        yield return new WaitForSeconds(1); // emulate loading. Replace with real loader
        loadBar.value++;

        loaderDescription.text = "Fetching cult records...";
        yield return StartCoroutine(saveDataLoader.LoadData());
        Report.Write(name, "Loaded save data from JSON.");
        loadBar.value++;

        yield return new WaitForSeconds(1); // emulate loading. Replace with real loader
        loadBar.value++;
        DebugManager.WriteDebugSessionLog(true);


        Report.Write(name, "Completed load routine.");
        SceneManager.LoadScene("MainMenu");
    }
}
