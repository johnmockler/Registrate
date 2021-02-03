using System;
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
    int sensitivityLevel;

    [SerializeField]
    public GameObject visualizationAxis;



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


        status_msg.text = "Welcome to Registrate. Press the 'A' button on the controller to proceed through the dialog";
        marker_count.text = previousMarkers.ToString();
        target_count.text = previousTargets.ToString();
        multiplier.text = sensitivityLevel.ToString();


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
                case AppState.Status.INTRO_1:
                    statusMessage = "Welcome to Registrate. Press the 'A' button on the controller to proceed through the dialogue";
                    mode_msg = "Introduction";
                    break;
                case AppState.Status.INTRO_2:
                    statusMessage = "To access the settings and map, press the menu button";
                    mode_msg = "Introduction";
                    break;
                case AppState.Status.INTRO_3:
                    statusMessage = "From there you can also place the map on a surface, or reset the calibration";
                    mode_msg = "Introduction";
                    break;
                case AppState.Status.EXPLORATION:
                    statusMessage = "Now, walk around the scene to initialize device tracking. When finished, press A.";
                    mode_msg = "Exploration";
                    break;
                case AppState.Status.FIND_MARKER:
                    statusMessage = "Find the indicated marker. Press A when satisfied with the alignment.";
                    mode_msg = "Find Marker";
                    break;
                case AppState.Status.FIND_TARGET:
                    statusMessage = "Place the target at the designated position by pressing A. ";
                    mode_msg = "Find Target";
                    break;
                case AppState.Status.ADJUST_ROTATIONY:
                    statusMessage = "Rotate the target horizontally using the left stick,  so that it is facing you.";
                    mode_msg = "Adjust Rotation";
                    break;
                case AppState.Status.ADJUST_ROTATIONX:
                    statusMessage = "Rotate the target vertically using the left stick.";
                    mode_msg = "Adjust Face";
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
                    statusMessage = "When you are ready, press A to confirm alignment";
                    mode_msg = "Alignment Ready";
                    break;
                case AppState.Status.ALIGNED:
                    statusMessage = "Cabin successfully aligned";
                    mode_msg = "Aligned";
                    break;
                case AppState.Status.FAILED:
                    statusMessage = "Could not find cabin in scene. Reset to try again.";
                    mode_msg = "Failed";
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
            multiplier.text = mapSensitivity(currentMultiplier).ToString();
        }


        previousStatus = currentStatus;
        previousMarkers = currentMarkers;
        previousTargets = currentTargets;
        previousMultiplier = currentMultiplier;

    }

    int mapSensitivity(float level)
    {
        return Convert.ToInt32(Math.Round(Math.Log(level, Constants.MULTIPLIER))) + 5;
    }
}
