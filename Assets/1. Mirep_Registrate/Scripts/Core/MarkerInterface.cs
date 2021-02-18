using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
// only allows marker tracking to be turned on when in FIND_MARKER or CONFIRM_MARKER state
/// </summary>
public class MarkerInterface : MonoBehaviour
{
    AppState appStatus;
    ARUWPController markerTracker;
    bool isTracking;
    // Start is called before the first frame update
    void Start()
    {
        appStatus = AppState.instance;
        markerTracker = GameObject.Find("ARToolKit").GetComponent<ARUWPController>();
        isTracking = false;
        EventManager.OnStateChange += OnStateChange;
    }

    void OnStateChange()
    {
        switch (appStatus.GetState())
        {
            case AppState.Status.FIND_MARKER:
            case AppState.Status.CONFIRM_MARKER:
                ResumeMarkerTracking();
            break;
            default:
                PauseMarkerTracking();
            break;
        }
    }

    void ResumeMarkerTracking()
    {
        if (!isTracking)
        {
            print("Turning tracking on");
            markerTracker.Resume();
            isTracking = true;
        }

    }

    void PauseMarkerTracking()
    {
        if(isTracking)
        {
            print("Turning tracking off");
            markerTracker.Pause();
            isTracking = false;
        }
    }
}
