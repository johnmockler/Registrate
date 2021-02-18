using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibUpdateUI : MonoBehaviour
{
    CalibrationAppState appStatus;
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
        appStatus = GameObject.Find("AppStatus").GetComponent<CalibrationAppState>();
        previousStatus = appStatus.GetState();
        sensitivityLevel = 5;

        statusMsg = GameObject.Find("StatusMsg").GetComponent<TextMesh>();
        mode = GameObject.Find("StatusDisplay/CurrentMode").GetComponent<TextMesh>();
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
        EventManager.OnMultiplierChanged += OnMultiplierChanged;



        //shift controls to initial position (not showing dpad)
        //control_tips.transform.Translate(0.0f, -shiftControls*scaleFactor, 0.0f);
        moveControls.SetActive(false);

      

        statusMsg.text = "Align the hologram with the cube. Press A to begin.";
        multiplier.text = sensitivityLevel.ToString();


    }

    void OnStateChange()
    {
        currentStatus = appStatus.GetState();

        string statusMessage;
        string mode_msg;

        switch (currentStatus)
        {
            
            case AppState.Status.ADJUST_ROTATIONY:
            statusMessage = "For best results, view the cube from every angle.";
            statusMessage = "A";
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
            case AppState.Status.ADJUST_Y:
            //statusMessage = "Rotate the target vertically";
            statusMessage = "For best results, view the cube from every angle.";
            mode_msg = "Translate Height";
            moveControls.SetActive(true);
            aText.text = "Place";
            bText.text = "Reset";
            moveControls.SetActive(true);
            upArrow.SetActive(true);
            downArrow.SetActive(true);
            rightArrow.SetActive(false);
            leftArrow.SetActive(false);
            break;
            case AppState.Status.ADJUST_X:
            //statusMessage = "Adjust the position of the target.";
            statusMessage = "For best results, view the cube from every angle.";
            mode_msg = "Translate X Axis";
            moveControls.SetActive(true);
            aText.text = "Place";
            bText.text = "Reset";
            moveControls.SetActive(true);
            upArrow.SetActive(true);
            downArrow.SetActive(true);
            rightArrow.SetActive(true);
            leftArrow.SetActive(true);
            break;
            case AppState.Status.ADJUST_Z:
            //statusMessage = "Adjust the depth of the target.";
            statusMessage = "For best results, view the cube from every angle.";
            mode_msg = "Translate Z Axis ";
            moveControls.SetActive(true);
            aText.text = "Place";
            bText.text = "Reset";
            moveControls.SetActive(true);
            upArrow.SetActive(true);
            downArrow.SetActive(true);
            rightArrow.SetActive(false);
            leftArrow.SetActive(false);
            break;
            default:
            statusMessage = "";
            mode_msg = "";
            break;

        }

        statusMsg.text = statusMessage;
        mode.text = mode_msg;

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
