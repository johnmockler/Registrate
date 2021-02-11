using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void Event();
    public static event Event OnStateChange;
    public static event Event OnMarkerFound;
    public static event Event OnTargetPlaced;
    public static event Event OnMultiplierChanged;
    // Start is called before the first frame update
    
    public static void TriggerStateChange()
    {
        OnStateChange();
    }

    public static void TriggerMarkerFound()
    {
        OnMarkerFound();
    }

    public static void TriggerTargetPlaced()
    {
        OnTargetPlaced();
    }

    public static void TriggerMultiplierChanged()
    {
        OnMultiplierChanged();
    }


}
