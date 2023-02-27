using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour {

    [SerializeField] private TMP_Text unitBtnText;
    [SerializeField] private TMP_Dropdown colorDropdown;
    [SerializeField] private Slider volSlider;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private AudioMixer mixer;

    private void Start() {
        ReadyOptions();
    }

    public void ToggleUnit() {
        if (GameManager.instance.prefs.unit == Constants.Units.MPH) {
            GameManager.instance.prefs.unit = Constants.Units.KPH;
        } else {
            GameManager.instance.prefs.unit = Constants.Units.MPH;
        }

        unitBtnText.text = $"UNIT: {GameManager.instance.prefs.unit}";
        AudioManager.instance.Play("button");
    }

    public void ChangeCarColor() {
        GameManager.instance.prefs.CAR_COLOR_ID = colorDropdown.value;
        Debug.Log(colorDropdown.value);
        AudioManager.instance.Play("button");
    }

    public void ChangeVolume(float _vol) {
        mixer.SetFloat("volume", _vol);
        GameManager.instance.prefs.gameVolume = _vol;
    }

    public void ReadyOptions() {
        unitBtnText.text = $"UNIT: {GameManager.instance.prefs.unit}";
        volSlider.value = GameManager.instance.prefs.gameVolume;
        colorDropdown.value = GameManager.instance.prefs.CAR_COLOR_ID;
    }

    public void DefaultOptions() {
        mixer.SetFloat("volume", Defaults.VOLUME);
        GameManager.instance.prefs.gameVolume = Defaults.VOLUME;
        GameManager.instance.prefs.unit = Defaults.unit;
        GameManager.instance.prefs.CAR_COLOR_ID = Defaults.CAR_COLOR_ID;
        AudioManager.instance.Play("button");
        GameManager.instance.SavePrefs();
        menuManager.ShowOptions();
    }

    public void ResetAll() {
        DefaultOptions();
        ResetTime();
    }

    public void ResetTime() {
        GameManager.instance.SaveTime(float.MaxValue);
        AudioManager.instance.Play("button");
        menuManager.ShowOptions();
    }

    public void ApplyChanges() {
        AudioManager.instance.Play("button");
        GameManager.instance.SavePrefs();
        menuManager.ShowMenu();
    }
}