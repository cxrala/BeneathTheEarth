using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public Dictionary<string, Scene> loadedScenes = new Dictionary<string, Scene>();
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            Scene currentScene = SceneManager.GetActiveScene();
            loadedScenes.Add(currentScene.name, currentScene);
        }
    }

    private void Update() {
        
    }

    public static void LoadSceneInBackground(string sceneName) {
        instance.StartCoroutine(AsyncLoadSceneInBackground(sceneName));
    }

    static IEnumerator AsyncLoadSceneInBackground(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
        }
        instance.loadedScenes.Add(sceneName, SceneManager.GetSceneByName(sceneName));
    }

    public static void LoadSceneInBackgroundThenSwitch(string sceneName) {
        instance.StartCoroutine(AsyncLoadSceneInBackgroundThenSwitch(sceneName));
    }

    static IEnumerator AsyncLoadSceneInBackgroundThenSwitch(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
        }
        instance.loadedScenes.Add(sceneName, SceneManager.GetSceneByName(sceneName));
        SceneManager.SetActiveScene(instance.loadedScenes[sceneName]);
    }

    public static void LoadSingleSceneInBackground(string sceneName) {
        instance.StartCoroutine(AsyncLoadSingleSceneInBackground(sceneName));
    }

    static IEnumerator AsyncLoadSingleSceneInBackground(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone) {
            yield return null;
        }
        instance.loadedScenes = new Dictionary<string, Scene>();
        instance.loadedScenes.Add(sceneName, SceneManager.GetSceneByName(sceneName));
    }

    public static void LoadSceneInBackgroundOrSwitch(string sceneName) {
        if (!instance.loadedScenes.ContainsKey(sceneName)) {
            instance.StartCoroutine(AsyncLoadSceneInBackgroundThenSwitch(sceneName));
        } else {
            SceneManager.SetActiveScene(instance.loadedScenes[sceneName]);
        }
    }

    public static void LoadSingleSceneInBackgroundOrSwitch(string sceneName) {
        if (!instance.loadedScenes.ContainsKey(sceneName)) {
            instance.StartCoroutine(AsyncLoadSingleSceneInBackground(sceneName));
        } else {
            SceneManager.SetActiveScene(instance.loadedScenes[sceneName]);
            UnloadAllButActive();
        }
    }

    public static void UnloadSceneInBackground(string sceneName) {
        instance.loadedScenes.Remove(sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public static void UnloadAllButActive() {
        Scene activeScene = SceneManager.GetActiveScene();
        foreach (KeyValuePair<string, Scene> kvp in instance.loadedScenes) {
            if(kvp.Key != activeScene.name) {
                SceneManager.UnloadSceneAsync(kvp.Key);
            }
        }
        instance.loadedScenes.Clear();
        instance.loadedScenes.Add(activeScene.name, activeScene);
    }

    public static void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        instance.loadedScenes = new Dictionary<string, Scene>() { { sceneName, SceneManager.GetSceneByName(sceneName) } };
    }

    public static void SetActiveScene(string sceneName) {
        SceneManager.SetActiveScene(instance.loadedScenes[sceneName]);
    }
}
