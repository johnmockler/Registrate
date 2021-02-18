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



    TextMesh statusMsg;
    TextMesh mode;
    TextMesh markerCount;
    TextMesh targetCount;
    TextMesh multiplier;

    //control tips
    GameObject aVisual;
    TextMesh aText;
    GameObject bVisual;
    TextMesh bText;
    GameObject moveControls;
    GameObject controlTips;
    GameObject upArrow;
    GameObject downArrow;
    GameObject leftArrow;
    GameObject rightArrow;
    GameObject menuButton;

    bool tipsSeen = false;


    // Start is called before the first frame update
    void Start()
    {
        appStatus = AppState.instance;
        previousStatus = appStatus.GetState();
        previousTargets = appStatus.GetTargetCount();
        previousMarkers = appStatus.GetMarkerCount();
        sensitivityLevel = 5;

        statusMsg = GameObject.Find("StatusMsg").GetComponent<TextMesh>();
        mode = GameObject.Find("StatusDisplay/CurrentMode").GetComponent<TextMesh>();
        markerCount = GameObject.Find("StatusDisplay/MarkerCount").GetComponent<TextMesh>();
        targetCount = GameObject.Find("StatusDisplay/TargetCount").GetComponent<TextMesh>();
        multiplier = GameObject.Find("StatusDisplay/MultiplierCount").GetComponent<TextMesh>();

        //Controls tips
        controlTips = GameObject.Find("/UserInterface/ControlTips");
        aVisual = GameObject.Find("/UserInterface/ControlTips/main_controls/a_button");
        aText = GameObject.Find("/UserInterface/ControlTips/main_controls/a_button/a_button_text").GetComponent<TextMesh>();
        bVisual = GameObject.Find("/UserInterface/ControlTips/main_controls/b_button");
        bText = GameObject.Find("/UserInterface/ControlTips/main_controls/b_button/b_button_text").GetComponent<TextMesh>();
        moveControls = GameObject.Find("/UserInterface/ControlTips/manipulation_controls");
        upArrow = GameObject.Find("/UserInterface/ControlTips/manipulation_controls/left_stick/up_arrow");
        downArrow = GameObject.Find("/UserInterface/ControlTips/manipulation_controls/left_stick/down_arrow");
        leftArrow = GameObject.Find("/UserInterface/ControlTips/manipulation_controls/left_stick/left_arrow");
        rightArrow = GameObject.Find("/UserInterface/ControlTips/manipulation_controls/left_stick/right_arrow");
        menuButton = GameObject.Find("/UserInterface/ControlTips/main_controls/menu_button");
        EventManager.OnStateChange += OnStateChange;
        EventManager.OnTargetPlaced += OnTargetPlaced;
        EventManager.OnMarkerFound += OnMarkerFound;
        EventManager.OnMultiplierChanged += OnMultiplierChanged;



        //shift controls to initial position (not showing dpad)
        //control_tips.transform.Translate(0.0f, -shiftControls*scaleFactor, 0.0f);
        moveControls.SetActive(false);

        GameObject.Find("StatusDisplay/MarkerLimit").GetComponent<TextMesh>().text = Constants.NUM_MARKERS.ToString();
        GameObject.Find("StatusDisplay/TargetLimit").GetComponent<TextMesh>().text = Constants.NUM_TARGETS.ToString();


        statusMsg.text = "Welcome to Registrate. \n Press the 'A' button on the controller to proceed through the dialog";
        markerCount.text = previousMarkers.ToString();
        targetCount.text = previousTargets.ToString();
        multiplier.text = sensitivityLevel.ToString();


    }

    void OnStateChange()
    {
        currentStatus = appStatus.GetState();

        string statusMessage;
        string mode_msg;

        switch (currentStatus)
        {
            case AppState.Status.INTRO_1:
                statusMessage = "Welcome to Registrate. Press the 'A' button on the controller";
                mode_msg = "Introduction";
                moveControls.SetActive(false);
                aText.text = "Next";
                bText.text = "Back";
                break;
            case AppState.Status.INTRO_2:
                statusMessage = "The controls are displayed in the bottom left corner.";
                mode_msg = "Introduction";
                moveControls.SetActive(false);
                aText.text = "Next";
                bText.text = "Back";
                Blink.BlinkTwiceAsync(controlTips);
                break;
            case AppState.Status.INTRO_3:
                statusMessage = "To access the settings and map, \n press the menu button";
                mode_msg = "Introduction";
                moveControls.SetActive(false);
                aText.text = "Next";
                bText.text = "Back";
                break;
            case AppState.Status.EXPLORATION:
                statusMessage = "Now, walk around the scene to \n initialize device tracking. When you are done press 'Next'";
                mode_msg = "Exploration";
                moveControls.SetActive(false);
                aText.text = "Next";
                bText.text = "Back";
                break;
            case AppState.Status.FIND_MARKER:
                statusMessage = "Find the marker indicated \n by the blinking object on the map \n  For best alignment, view at slight angle.";
                mode_msg = "Find Marker";
                moveControls.SetActive(false);
                aText.text = "Next";
                bText.text = "Back";
                break;
            case AppState.Status.FIND_TARGET:
                statusMessage = "Place the target at the position indicated \n by the blinking circle on the map ";
                mode_msg = "Find Target";
                moveControls.SetActive(false);
                aText.text = "Place";
                bText.text = "Back";
                break;
            case AppState.Status.ADJUST_ROTATIONY:
            //statusMessage = "Rotate the target horizontally, so that it is facing you.";
                statusMessage = "Adjust each axis of the target \n until it aligns with the bracket. \n change the axis on the directional pad";
                mode_msg = "Rotate Y Axis";
                aText.text = "Confirm";
                bText.text = "Reset";
                moveControls.SetActive(true);
                upArrow.SetActive(false);
                downArrow.SetActive(false);
                rightArrow.SetActive(true);
                leftArrow.SetActive(true);
                if (!tipsSeen)
                {
                    Blink.BlinkTwiceAsync(moveControls);
                    tipsSeen = true;
                }
                break;
            case AppState.Status.ADJUST_ROTATIONX:
            //statusMessage = "Rotate the target vertically";
                statusMessage = "Adjust each axis of the target \n until it aligns with the bracket. \n change the axis on the directional pad";
                mode_msg = "Rotate X Axis";
                moveControls.SetActive(true);
                aText.text = "Confirm";
                bText.text = "Reset";
                moveControls.SetActive(true);
                upArrow.SetActive(true);
                downArrow.SetActive(true);
                rightArrow.SetActive(false);
                leftArrow.SetActive(false);
                break;
            case AppState.Status.ADJUST_NORMAL:
            //statusMessage = "Adjust the position of the target.";
                statusMessage = "Adjust each axis of the target \n until it aligns with the bracket. \n change the axis on the directional pad";
                mode_msg = "Translate Surface Axes";
                moveControls.SetActive(true);
                aText.text = "Confirm";
                bText.text = "Reset";
                moveControls.SetActive(true);
                upArrow.SetActive(true);
                downArrow.SetActive(true);
                rightArrow.SetActive(true);
                leftArrow.SetActive(true);
                break;
            case AppState.Status.ADJUST_DEPTH:
            //statusMessage = "Adjust the depth of the target.";
                statusMessage = "Tip: View target from the side to better judge depth.";
                mode_msg = "Translate Depth";
                moveControls.SetActive(true);
                aText.text = "Confirm";
                bText.text = "Reset";
                moveControls.SetActive(true);
                upArrow.SetActive(true);
                downArrow.SetActive(true);
                rightArrow.SetActive(false);
                leftArrow.SetActive(false);
                break;
            case AppState.Status.CONFIRM_MARKER:
                statusMessage = "Rescan the indicated marker to save \n Marker/Target pairs";
                mode_msg = "Confirm Markers";
                moveControls.SetActive(false);
                aText.text = "Confirm";
                bText.text = "Back";
                break;
            case AppState.Status.ALIGNMENT_READY:
                statusMessage = "When you are ready, press A to confirm alignment";
                mode_msg = "Alignment Ready";
                moveControls.SetActive(false);
                aText.text = "Confirm";
                bText.text = "Back";
                break;
            case AppState.Status.ALIGNED:
                statusMessage = "Cabin successfully aligned";
                mode_msg = "Aligned";
                aVisual.SetActive(false);
                bVisual.SetActive(false);
                break;
            case AppState.Status.FAILED:
                statusMessage = "Could not find cabin in scene. Reset to try again.";
                mode_msg = "Failed";
                aVisual.SetActive(false);
                bVisual.SetActive(false);
                break;
            default:
                statusMessage = "";
                mode_msg = "";
                break;
        }

        statusMsg.text = statusMessage;
        mode.text = mode_msg;
    
    }

    void OnMarkerFound()
    {
        currentMarkers = appStatus.GetMarkerCount();
        markerCount.text = currentMarkers.ToString();
    }

    void OnTargetPlaced()
    {
        currentTargets = appStatus.GetTargetCount();
        targetCount.text = currentTargets.ToString();
    }

    void OnMultiplierChanged()
    {
        currentMultiplier = appStatus.GetMultiplier();
        multiplier.text = MapSensitivity(currentMultiplier).ToString();
    }

    int MapSensitivity(float level)
    {
        return Convert.ToInt32(Math.Round(Math.Log(level, Constants.MULTIPLIER))) + 5;
    }

}
