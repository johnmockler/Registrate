using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{
    AppState appStatus;
    AppState.Status currentStatus;
    AppState.Status previousStatus;

    int currentTargets;
    int currentMarkers;
    float currentMultiplier;

    int previousTargets;
    int previousMarkers;
    float previousMultiplier;



    TextMesh status_msg;
    TextMesh mode;
    TextMesh marker_count;
    TextMesh target_count;
    TextMesh multiplier;
    // Start is called before the first frame update
    void Start()
    {
        appStatus = AppState.instance;
        previousStatus = appStatus.getState();
        previousTargets = appStatus.getTargetCount();
        previousMarkers = appStatus.getMarkerCount();


        status_msg = GameObject.Find("StatusMsg").GetComponent<TextMesh>();
        mode = GameObject.Find("StatusDisplay/CurrentMode").GetComponent<TextMesh>();
        marker_count = GameObject.Find("StatusDisplay/MarkerCount").GetComponent<TextMesh>();
        target_count = GameObject.Find("StatusDisplay/TargetCount").GetComponent<TextMesh>();
        multiplier = GameObject.Find("StatusDisplay/MultiplierCount").GetComponent<TextMesh>();

        GameObject.Find("StatusDisplay/MarkerLimit").GetComponent<TextMesh>().text = Constants.NUM_MARKERS.ToString();
        GameObject.Find("StatusDisplay/TargetLimit").GetComponent<TextMesh>().text = Constants.NUM_TARGETS.ToString();


        status_msg.text = "Walk around the scene to initialize device tracking";
        marker_count.text = previousMarkers.ToString();
        target_count.text = previousTargets.ToString();
        multiplier.text = "1x";


    }

    // Checks for a change in the state, and updates UI Accordingly
    void Update()
    {
        currentStatus = appStatus.getState();
        currentMarkers = appStatus.getMarkerCount();
        currentTargets = appStatus.getTargetCount();
        currentMultiplier = appStatus.getMultiplier();


        if (currentStatus != previousStatus)
        {
            string statusMessage;
            string mode_msg;

            switch (currentStatus)
            {
                case AppState.Status.EXPLORATION:
                    statusMessage = "Walk around the scene to initialize device tracking";
                    mode_msg = "Exploration";
                    break;
                case AppState.Status.FIND_TARGET:
                    statusMessage = "Place the target at the designated position.";
                    mode_msg = "Searching";
                    break;
                case AppState.Status.ADJUST_ROTATION:
                    statusMessage = "Rotate the target so that it is facing you.";
                    mode_msg = "Adjust Rotation";
                    break;
                case AppState.Status.ADJUST_NORMAL:
                    statusMessage = "Adjust the position of the target.";
                    mode_msg = "Adjust Face";
                    break;
                case AppState.Status.ADJUST_DEPTH:
                    statusMessage = "Adjust the depth of the target.";
                    mode_msg = "Adjust Depth";
                    break;
                case AppState.Status.ALIGNMENT_READY:
                    statusMessage = "Press A to confirm alignment";
                    mode_msg = "Alignment Ready";
                    break;
                default:
                    statusMessage = "";
                    mode_msg = "";
                    break;
            }

            status_msg.text = statusMessage;
            mode.text = mode_msg;
        }

        if (currentMarkers != previousMarkers)
        {
            marker_count.text = currentMarkers.ToString();
        }

        if (currentTargets != previousTargets)
        {
            target_count.text = currentTargets.ToString();
        }

        if (currentMultiplier != previousMultiplier)
        {
            multiplier.text = currentMultiplier.ToString() + "x";
        }

        previousStatus = currentStatus;
        previousMarkers = currentMarkers;
        previousTargets = currentTargets;
        previousMultiplier = currentMultiplier;

    }

    public void setMultiplier(float level)
    {

    }
}
