using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float target;
    [SerializeField] private float time;
    [SerializeField] private TMP_Text text;
    
    private void Start()
    {
        text = GetComponent<TMP_Text>();
        this.transform.localPosition = new Vector3(0, this.transform.localPosition.y, this.transform.localPosition.z);
        LeanTween.moveY(this.gameObject, target, time).setOnComplete(DestroyMe);
        StartCoroutine("Fade");
    }

    public void SetText(string _text) {
        text.text = $"\"{_text.ToUpper()}\"";
    }

    IEnumerator Fade() {
        float _curTime = 0f;
        while (_curTime < time) {
            float _alpha = Mathf.Lerp(1f, 0f, _curTime / time);
            text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
            _curTime+=Time.deltaTime;
            yield return null;
        }
    }

    private void DestroyMe() {
        Destroy(this.gameObject);
    }
}
