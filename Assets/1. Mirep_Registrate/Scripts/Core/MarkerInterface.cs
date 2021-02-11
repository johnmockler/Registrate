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
        markerTracker = GameObject.Find("ARUWP Controller").GetComponent<ARUWPController>();
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
        appStatus.RemoveMarkerAnchor();

        if (!isTracking)
        {
            print("Turning tracking on");
            if (markerTracker.status == ARUWP.ARUWP_STATUS_RUNNING)
            {
                Debug.Log("Marker tracker already running");
            }
            else if (markerTracker.status != ARUWP.ARUWP_STATUS_CTRL_INITIALIZED)
            {
                Debug.Log("Error: marker tracking not initialized");
            }
            else
            {
                markerTracker.Resume();
                Debug.Log("Marker tracking resumed");
            }

            isTracking = true;
        }

    }

    void PauseMarkerTracking()
    {
        if(isTracking)
        {
            print("Turning off tracking");
            if (markerTracker.status == ARUWP.ARUWP_STATUS_CTRL_INITIALIZED)
            {
                Debug.Log("Marker Tracking already paused");
            }
            else if (markerTracker.status != ARUWP.ARUWP_STATUS_RUNNING)
            {
                Debug.Log("Error: not tracking marker");
            }
            else
            {
                markerTracker.Pause();
                Debug.Log("Marker tracking paused");
            }

            isTracking = false;
        }
    }
}
