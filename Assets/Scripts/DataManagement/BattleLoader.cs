using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoader : MonoBehaviour
{
    public static BattleLoader instance;
    private static string dungeonName;
    public float fadeTime;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public static void LoadBattle(GameObject data) {
        GameState.SetPaused();
        dungeonName = SceneManager.GetActiveScene().name;
        SceneLoader.LoadSceneInBackgroundOrSwitch("Battle");
        MusicEngine.instance.FadeToChannel2(instance.fadeTime);
        Data.AttachObject("enemyData", data);
    }

    public static void LoadDungeonFromWin() {
        SceneLoader.LoadSingleSceneInBackgroundOrSwitch(dungeonName);
        GameObject data =  Data.PopObject("enemyData", instance.gameObject);
        Destroy(data);
        MusicEngine.instance.FadeToChannel1(instance.fadeTime);
        GameState.SetPlaying();
    }

    public static void LoadDungeonFromLose() {
        MusicEngine.instance.FadeBothAndStop(instance.fadeTime);
        GameObject data = Data.PopObject("enemyData", instance.gameObject);
        Destroy(data);
        data = Data.PopObject("playerData", instance.gameObject);
        Destroy(data);
        instance.StartCoroutine(WaitForDungeonUnload(SceneManager.sceneCount));
        SceneLoader.UnloadSceneInBackground(dungeonName);
    }

    static IEnumerator WaitForDungeonUnload(int count) {
        while (SceneManager.sceneCount == count) {
            yield return null;
        }
        SceneLoader.LoadSingleSceneInBackground(dungeonName);
        GameState.SetPlaying();
    }
}
