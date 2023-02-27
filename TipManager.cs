using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipManager : MonoBehaviour
{
    [SerializeField] private TMP_Text tip;

    private float time = 3.0f;
    public void HideTip() {
        StartCoroutine("Fade");
    }

    IEnumerator Fade() {
        float _curTime = 0f;
        while (_curTime < time) {
            float _alpha = Mathf.Lerp(1f, 0f, _curTime / time);
            tip.color = new Color(tip.color.r, tip.color.g, tip.color.b, _alpha);
            _curTime+=Time.deltaTime;
            yield return null;
        }
    }
}
