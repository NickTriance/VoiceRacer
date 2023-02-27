using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine;

[RequireComponent(typeof(Driver))]
public class KeywordListener : MonoBehaviour
{
    [SerializeField] private PopupManager popupManager;
    [SerializeField] private ConfidenceLevel confidence;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>(); //todo: add keywords into dictionary
    private KeywordRecognizer recognizer;
    private Driver driver;
    private void Start() {

        driver = GetComponent<Driver>(); 
        //TODO: here has got to be a better way of assigning these.
        keywords.Add("forward", () => {
            Forward(); //"Forward" both shifts the car into forward and accelerates
        });
        keywords.Add("accelerate", () => {
            Accelerate();
        });
        keywords.Add("gas", () => {
            Accelerate();
        }); 
        keywords.Add("go", () => {
            Accelerate();
        });
        keywords.Add("vroom", () => {
            Accelerate();
        });
        keywords.Add("drive", () => {
            Accelerate();
        });
        keywords.Add("stop", () => {
            Stop();
        });
        keywords.Add("slow", () => {
            Brake();
        });
        keywords.Add("brake", () => {
            Brake();
        });
        keywords.Add("brakes", () => {
            Brake();
        });
        keywords.Add("left", () => {
            Steer(Constants.DIRECTIONS.LEFT);
        });
        keywords.Add("right", () => {
            Steer(Constants.DIRECTIONS.RIGHT);
        });
        keywords.Add("straight", () => {
            Steer(Constants.DIRECTIONS.STRAIGHT);
        });
        keywords.Add("straighten", () => {
            Steer(Constants.DIRECTIONS.STRAIGHT);
        });
        keywords.Add("reverse", () => {
            Reverse();
        });
        keywords.Add("handbrake", () => {
            Handbrake();
        });
        keywords.Add("lift", () => {
            Lift();
        });
        keywords.Add("off", () => {
            Lift();
        });
        keywords.Add("coast", () => {
            Coast();
        });
        keywords.Add("honk", () => {
            Horn();
        });
        keywords.Add("beep", () => {
            Horn();
        });
        keywords.Add("horn", () => {
            Horn();
        });
        /*
        keywords.Add("jump", () => {
            Jump();
        });
        */

        recognizer = new KeywordRecognizer(keywords.Keys.ToArray(), confidence);
        recognizer.OnPhraseRecognized += OnPhraseRecognized;
        recognizer.Start();
        Debug.Log("recognizer started");
     }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction)) {
            keywordAction.Invoke();
            Debug.Log(args.text);
            popupManager.CreatePopup(args.text);
        }
    }
    
    private void OnDisable() {
        recognizer.Stop();
        recognizer.Dispose();
        Debug.Log("Stopped Recognizer");
    }

    private void Accelerate() {
        driver.Accelerate();
    }
    private void Brake() {
        driver.Brake();
    }
    private void Stop() {
        driver.Stop();
    }
    private void Reverse() {
        driver.Reverse();
    }
    private void Lift() {
        driver.Lift();
    }
    private void Coast() {
        driver.Coast();
    }
    private void Steer(Constants.DIRECTIONS dir) {
        driver.Steer(dir);
    }
    private void Handbrake() {
        driver.Handbrake();
    }
    private void Forward() {
        driver.Forward();
    }
    private void Horn() {
        driver.Honk();
    }
    private void Jump() {
        driver.Jump();
    }
}  