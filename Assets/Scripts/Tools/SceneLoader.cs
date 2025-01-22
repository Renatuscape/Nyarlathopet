using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    static LoadHelper helper;

    static LoadHelper GetHelper()
    {
        if (helper == null)
        {
            // Create a GameObject that handles enumeration
            GameObject go = new GameObject("SceneLoadHelper");
            helper = go.AddComponent<LoadHelper>();
            // Make it persist between scenes
            GameObject.DontDestroyOnLoad(go);
        }
        return helper;
    }

    public static void LoadSceneAndExecute(string sceneName, Action doAfterLoad)
    {
        GetHelper().StartCoroutine(GetHelper().LoadSceneAsync(sceneName, doAfterLoad));
    }

    class LoadHelper : MonoBehaviour
    {
        public IEnumerator LoadSceneAsync(string sceneName, Action doAfterLoad)
        {
            Report.Write(name, "Loading " + sceneName);
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            Report.Write(name, "Completed loading " + sceneName);

            if (doAfterLoad != null)
            {
                doAfterLoad.Invoke();
            }
            else
            {
                Report.Write(name, "Scene was loaded, but doAfterLoad was null.");
            }
        }
    }
}