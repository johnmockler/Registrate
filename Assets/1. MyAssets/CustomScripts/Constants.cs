using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{

    //change this based on type of registration
    public const int NUM_TARGETS = 4;
    public const int NUM_MARKERS = 4;

    public const float BASE_TRANSLATION = 0.005f;
    public const float BASE_ROTATION = 0.5f;
    public const float MULTIPLIER = 1.5f;

    //array wont let me be constant :(
    public static string[] BRACKET_TARGETS = {"bracket3", "bracket4", "bracket8", "bracket9", "bracket14", "bracket15" };

    //public const List<Vector4> MODEL_POINTS;


}