using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody target;
    [SerializeField] private TMP_Text text;

    private float conversionFactor;
    private string speedUnitName;

    private void Start() {

        if (GameManager.instance.prefs.unit == Constants.Units.MPH) {
            speedUnitName = "MPH";
            conversionFactor = Constants.convert_mph; 
        } else {
            speedUnitName = "KPH";
            conversionFactor = Constants.convert_kph;
        }
    }

    private void FixedUpdate() {
        
        float targetSpeed = target.velocity.magnitude * conversionFactor;
        targetSpeed = Mathf.RoundToInt(targetSpeed);
        text.text = $"<b><i>{targetSpeed} {speedUnitName}</b></i>";
    }
}
