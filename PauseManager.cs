using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour {
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject times;


    private void Start() {
        pauseMenu.SetActive(GameManager.instance.isPaused);
        hud.SetActive(!GameManager.instance.isPaused);
        if (!(GameManager.instance.isPaused)){
            AudioManager.instance.Unmute();
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Pause")) {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu() {
        if (GameManager.instance.isPaused) {
            GameManager.instance.UnpauseGame();
            AudioManager.instance.Unmute();
        } else {
            GameManager.instance.PauseGame();
            AudioManager.instance.Mute();
        }
        hud.SetActive(!GameManager.instance.isPaused);
        pauseMenu.SetActive(GameManager.instance.isPaused);
        times.SetActive(!(hud.activeInHierarchy || pauseMenu.activeInHierarchy));
        AudioManager.instance.Play("button");
    }

    [SerializeField] private TMP_Text pb_time;
    [SerializeField] private TMP_Text sb_time;
    [SerializeField] private TMP_Text cur_time;
    [SerializeField] private TMP_Text session_times;
    [SerializeField] private Timer timer;

    public void ShowTimes() {

        float _pb = GameManager.instance.pbTime.time;
        float _sb = Timer.sessionBestTime;
        if (_pb == float.MaxValue) {
            _pb = 0f;
        }
        if (_sb == float.MaxValue) {
            _sb = 0f;
        }

        pb_time.text = Timer.FormatTime(_pb);
        sb_time.text = Timer.FormatTime(_sb);
        cur_time.text = Timer.FormatTime(timer.currentElapsedTime);

        string sessionTimes = $"{cur_time.text}{System.Environment.NewLine}";
        for(int i = 0; i < timer.previousTimes.Count; i++) {
            sessionTimes+=$"{Timer.FormatTime(timer.previousTimes[i])}{System.Environment.NewLine}";
        }
        session_times.text = sessionTimes;

        times.SetActive(true);
        pauseMenu.SetActive(false);
        hud.SetActive(false);
        AudioManager.instance.Play("button");
    }

    public void HideTimes() {
        times.SetActive(false);
        pauseMenu.SetActive(true);
        hud.SetActive(false);
        AudioManager.instance.Play("button");
    }

    public void MainMenu() {
        GameManager.instance.UnpauseGame();
        GameManager.instance.LoadMainMenu();
        AudioManager.instance.Play("button");
    }

    public void Restart() {
        GameManager.instance.ResetGame();
    }
}