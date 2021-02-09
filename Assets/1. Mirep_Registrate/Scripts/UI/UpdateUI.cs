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

    //control tips
    GameObject a_visual;
    TextMesh a_text;
    GameObject b_visual;
    TextMesh b_text;
    GameObject move_controls;
    GameObject control_tips;
    GameObject up_arrow;
    GameObject down_arrow;
    GameObject left_arrow;
    GameObject right_arrow;
    float shiftControls = 6.0f;
    float scaleFactor = 0.007f;
    bool controlTipsActivated = false;


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

        //Controls tips
        control_tips = GameObject.Find("/UserInterface/ControlTips");
        a_visual = GameObject.Find("/UserInterface/ControlTips/main_controls/a_button");
        a_text = GameObject.Find("/UserInterface/ControlTips/main_controls/a_button/a_button_text").GetComponent<TextMesh>();
        b_visual = GameObject.Find("/UserInterface/ControlTips/main_controls/b_button");
        b_text = GameObject.Find("/UserInterface/ControlTips/main_controls/b_button/b_button_text").GetComponent<TextMesh>();
        move_controls = GameObject.Find("/UserInterface/ControlTips/manipulation_controls");
        up_arrow = GameObject.Find("/UserInterface/ControlTips/manipulation_controls/left_stick/up_arrow");
        down_arrow = GameObject.Find("/UserInterface/ControlTips/manipulation_controls/left_stick/down_arrow");
        left_arrow = GameObject.Find("/UserInterface/ControlTips/manipulation_controls/left_stick/left_arrow");
        right_arrow = GameObject.Find("/UserInterface/ControlTips/manipulation_controls/left_stick/right_arrow");




        //shift controls to initial position (not showing dpad)
        //control_tips.transform.Translate(0.0f, -shiftControls*scaleFactor, 0.0f);
        move_controls.SetActive(false);

        GameObject.Find("StatusDisplay/MarkerLimit").GetComponent<TextMesh>().text = Constants.NUM_MARKERS.ToString();
        GameObject.Find("StatusDisplay/TargetLimit").GetComponent<TextMesh>().text = Constants.NUM_TARGETS.ToString();


        status_msg.text = "Welcome to Registrate. \n Press the 'A' button on the controller to proceed through the dialog";
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
                    statusMessage = "Welcome to Registrate. \n The controls are displayed in the bottom left";
                    mode_msg = "Introduction";
                    move_controls.SetActive(false);
                    a_text.text = "Next";
                    b_text.text = "Back";
                    break;
                case AppState.Status.INTRO_2:
                    statusMessage = "To access the settings and map, \n press the menu button";
                    mode_msg = "Introduction";
                    move_controls.SetActive(false);
                    a_text.text = "Next";
                    b_text.text = "Back";
                    break;
                case AppState.Status.INTRO_3:
                    statusMessage = "From there you can also place the map \n on a surface, or reset the calibration";
                    mode_msg = "Introduction";
                    move_controls.SetActive(false);
                    a_text.text = "Next";
                    b_text.text = "Back";
                    break;
                case AppState.Status.EXPLORATION:
                    statusMessage = "Now, walk around the scene to \n initialize device tracking.";
                    mode_msg = "Exploration";
                    move_controls.SetActive(false);
                    a_text.text = "Next";
                    b_text.text = "Back";
                    break;
                case AppState.Status.FIND_MARKER:
                    statusMessage = "Find the marker indicated \n by the blinking object on the map \n  For best alignment, view at slight angle.";
                    mode_msg = "Find Marker";
                    move_controls.SetActive(false);
                    a_text.text = "Next";
                    b_text.text = "Back";
                    break;
                case AppState.Status.FIND_TARGET:
                    statusMessage = "Place the target at the position indicated \n by the blinking circle on the map ";
                    mode_msg = "Find Target";
                    move_controls.SetActive(false);
                    a_text.text = "Place";
                    b_text.text = "Back";
                    break;
                case AppState.Status.ADJUST_ROTATIONY:
                    //statusMessage = "Rotate the target horizontally, so that it is facing you.";
                    statusMessage = "Adjust each axis of the target \n until it aligns with the bracket. \n change the axis on the directional pad";
                    mode_msg = "Rotate Y Axis";
                    a_text.text = "Confirm";
                    b_text.text = "Reset";
                    move_controls.SetActive(true);
                    up_arrow.SetActive(false);
                    down_arrow.SetActive(false);
                    right_arrow.SetActive(true);
                    left_arrow.SetActive(true);
                    break;
                case AppState.Status.ADJUST_ROTATIONX:
                //statusMessage = "Rotate the target vertically";
                    statusMessage = "Adjust each axis of the target \n until it aligns with the bracket. \n change the axis on the directional pad";
                    mode_msg = "Rotate X Axis";
                    move_controls.SetActive(true);
                    a_text.text = "Place";
                    b_text.text = "Reset";
                    move_controls.SetActive(true);
                    up_arrow.SetActive(true);
                    down_arrow.SetActive(true);
                    right_arrow.SetActive(false);
                    left_arrow.SetActive(false);
                    break;
                case AppState.Status.ADJUST_NORMAL:
                //statusMessage = "Adjust the position of the target.";
                    statusMessage = "Adjust each axis of the target \n until it aligns with the bracket. \n change the axis on the directional pad";
                    mode_msg = "Translate Surface Axes";
                    move_controls.SetActive(true);
                    a_text.text = "Place";
                    b_text.text = "Reset";
                    move_controls.SetActive(true);
                    up_arrow.SetActive(true);
                    down_arrow.SetActive(true);
                    right_arrow.SetActive(true);
                    left_arrow.SetActive(true);
                    break;
                case AppState.Status.ADJUST_DEPTH:
                 //statusMessage = "Adjust the depth of the target.";
                    statusMessage = "Tip: View target from the side to better judge depth.";
                    mode_msg = "Translate Depth";
                    move_controls.SetActive(true);
                    a_text.text = "Place";
                    b_text.text = "Reset";
                    move_controls.SetActive(true);
                    up_arrow.SetActive(true);
                    down_arrow.SetActive(true);
                    right_arrow.SetActive(false);
                    left_arrow.SetActive(false);
                    break;
                case AppState.Status.CONFIRM_MARKER:
                    statusMessage = "Rescan the indicated marker to save \n Marker/Target pairs";
                    mode_msg = "Confirm Markers";
                    move_controls.SetActive(false);
                    a_text.text = "Confirm";
                    b_text.text = "Back";
                    break;
                case AppState.Status.ALIGNMENT_READY:
                    statusMessage = "When you are ready, press A to confirm alignment";
                    mode_msg = "Alignment Ready";
                    move_controls.SetActive(false);
                    a_text.text = "Confirm";
                    b_text.text = "Back";
                    break;
                case AppState.Status.ALIGNED:
                    statusMessage = "Cabin successfully aligned";
                    mode_msg = "Aligned";
                    a_visual.SetActive(false);
                    b_visual.SetActive(false);
                    break;
                case AppState.Status.FAILED:
                    statusMessage = "Could not find cabin in scene. Reset to try again.";
                    mode_msg = "Failed";
                    a_visual.SetActive(false);
                    b_visual.SetActive(false);
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
