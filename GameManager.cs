using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPaused {get; private set;} = false;
    public string settingsFilePath = $"{Directory.GetCurrentDirectory()}\\settings.json";
    public string timeFilePath = $"{Directory.GetCurrentDirectory()}\\pb.time";
    public UserPrefs prefs;
    public LapTime pbTime;

    [SerializeField] private GameObject loadingScreen;
    
    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Awake() {
        
        if (instance == null){
            instance = this;
        } else {
            Destroy(this);
        }

        SceneManager.LoadSceneAsync((int)Constants.BuildIndexs.MAIN_MENU, LoadSceneMode.Additive);
        
        if (File.Exists(settingsFilePath)) {
            string json = File.ReadAllText(settingsFilePath);
            Debug.Log($"Found settings file, loaded settings: {json}");
            prefs = JsonUtility.FromJson<UserPrefs>(json);
        } else {
            prefs = new UserPrefs();
            prefs.CAR_COLOR_ID = Defaults.CAR_COLOR_ID;
            prefs.unit = Defaults.unit;
            prefs.gameVolume = Defaults.VOLUME;
            prefs.speedUnitConversion = Defaults.speedUnitConversion;
            Debug.Log($"Didn't find settings file, created a new one with defaults...");
            SavePrefs();
        }

        if (File.Exists(timeFilePath)) {
            string json = File.ReadAllText(timeFilePath);
            pbTime = JsonUtility.FromJson<LapTime>(json);
            Debug.Log("Loaded previous personal best time");
        } else {
            pbTime = new LapTime();
            pbTime.time = float.MaxValue;
            Debug.Log("Didn't find a previous best time");
        }
    }

    public void LoadGame() {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)Constants.BuildIndexs.MAIN_MENU));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)Constants.BuildIndexs.TRACK, LoadSceneMode.Additive));
        StartCoroutine("sceneLoader");
        //loadingScreen.SetActive(false);
    }

    public void ResetGame() {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)Constants.BuildIndexs.TRACK));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)Constants.BuildIndexs.TRACK, LoadSceneMode.Additive));
        StartCoroutine("sceneLoader");
        UnpauseGame();
    }

    public void LoadMainMenu() {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)Constants.BuildIndexs.TRACK));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)Constants.BuildIndexs.MAIN_MENU, LoadSceneMode.Additive));

        StartCoroutine("sceneLoader");
        // loadingScreen.SetActive(false);
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnpauseGame() {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public async void SavePrefs() {
        string json = JsonUtility.ToJson(prefs);
        Debug.Log($"Saving {json}...");
        await File.WriteAllTextAsync("settings.json", json);
    }

    public async void SaveTime(float time) {
        pbTime.time = time;
        string json = JsonUtility.ToJson(pbTime);
        await File.WriteAllTextAsync("pb.time", json);
    }

    public IEnumerator sceneLoader()  {
        for (int i = 0; i < scenesLoading.Count; i++) {
            while (!scenesLoading[i].isDone) {
                yield return null;
            }
            scenesLoading.Remove(scenesLoading[i]);
        }
        loadingScreen.SetActive(false);
    }
}
