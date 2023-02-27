using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
   private float fps;
   [SerializeField] private Text text;

   private void Start() {
        //if (UserPrefs.showFps) {
           // InvokeRepeating("GetFPS", 1, 1);
        //}
   }

   public void GetFPS() {
        fps = (int)(1f / Time.unscaledDeltaTime);
        text.text = $"{fps} FPS";
   }
}
