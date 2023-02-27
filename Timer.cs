using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float sessionBestTime = float.MaxValue;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private TMP_Text prevText;
    [SerializeField] private float previousDisplayTime;
    [SerializeField] private Color sbColor;
    [SerializeField] private Color pbColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private float fullAlphaTime = 2.0f;
    [SerializeField] private float fadeoutTime = 4.0f;
    public float currentElapsedTime {get; private set;} = 0f;
    private bool isRunning = false;
    public List<float> previousTimes {get; private set;} = new List<float>();

    private float personalBestTime;

    private void Start() {
        personalBestTime = GameManager.instance.pbTime.time;
        prevText.color = new Color(0, 0, 0, 0);
    }


    public void StartTimer() {
        isRunning = true;
        currentElapsedTime = 0f;
    }

    public void Lap() {
        previousTimes.Add(currentElapsedTime);

        if (currentElapsedTime == 0f)
            return; 

        prevText.color = normalColor;
        if (currentElapsedTime <= sessionBestTime) {
            prevText.color = sbColor;
            sessionBestTime = currentElapsedTime;
        }
        if (currentElapsedTime < personalBestTime) {
            prevText.color = pbColor;
            personalBestTime = currentElapsedTime;
            GameManager.instance.SaveTime(currentElapsedTime);
        }
        prevText.text = $"<b><i>{FormatTime(currentElapsedTime)}</b></i>";
        StartCoroutine("FadeOut");

        Debug.Log($"Lap completed in {FormatTime(currentElapsedTime)}");
        currentElapsedTime = 0f;
    }

    public void StopTimer() {
        isRunning = false;
    }

    private void DisplayTime() {
        string displayText = $"<b><i>{FormatTime(currentElapsedTime)}</b></i>";
        mainText.text = displayText;
    }

    public static string FormatTime(float time) {

        int secs = (int)time;
        int mins = secs / 60;
        int hours = mins / 60;
        int ms = (int)((time - secs) * 1000);
        
        secs = secs % 60;
        mins = mins % 60;

        string _hours = hours.ToString("D2");
        string _mins = mins.ToString("D2");
        string _secs = secs.ToString("D2");;
        string _ms = ms.ToString("D3");;

        return $"{_hours}:{_mins}:{_secs}.{_ms}";
    }

    private void Update() {
        if (!isRunning)
            return;
        
        currentElapsedTime += Time.deltaTime;
        DisplayTime();
    }

    private IEnumerator FadeOut() {
        float _curTime = 0f;
        while (_curTime < fullAlphaTime) {
            _curTime+=Time.deltaTime;
            yield return null;
        }
        while (_curTime < fadeoutTime) {
            float _alpha = Mathf.Lerp(1f, 0f, _curTime / fadeoutTime);
            prevText.color = new Color(prevText.color.r, prevText.color.g, prevText.color.b, _alpha);
            _curTime+=Time.deltaTime;
            yield return null;
        }
    }
    
    /// <summary>
    /// Returns the current lap time in seconds.
    /// </summary>
    public float GetTime() {
        return currentElapsedTime;
    }

    public bool GetRunning() {
        return isRunning;
    }
}
