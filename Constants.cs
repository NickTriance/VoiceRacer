using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{    
    public const float convert_kph = 3.6f;
    public const float convert_mph = 2.237f;
    
    public enum DIRECTIONS {
    LEFT,
    RIGHT,
    STRAIGHT
   }
   
    public enum Units {
        MPH,
        KPH
    }

   public enum BuildIndexs {
        DIRECTOR = 0,
        MAIN_MENU = 1,
        TRACK = 2
   }
}
