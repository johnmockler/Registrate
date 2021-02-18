using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController: MonoBehaviour, InputHandler.IPlayerActions
{


    GameObject cursor;
    GameObject hudDisplay;

    TextMesh statusMessage;

    MenuController menuController;

    AppState appStatus;

    InputHandler controls;

    private Sounds sounds;

    float translationStep = Constants.BASE_TRANSLATION;
    float rotationStep = Constants.BASE_ROTATION;


    bool b_PressedOnce = false;
    bool toggledControlsOnce = false;

    void Awake()
    {
        controls = new InputHandler();
        this.sounds = GetComponent<Sounds>();
    }
    void Start()
    {
        appStatus = AppState.instance;
        controls.Player.SetCallbacks(this);

        hudDisplay = GameObject.Find("/UserInterface/HUD");
        menuController = GameObject.Find("/_MenuController").GetComponent<MenuController>();
        statusMessage = GameObject.Find("/UserInterface/HUD/StatusMsg").GetComponent<TextMesh>();
        this.sounds = GetComponent<Sounds>();
        cursor.SetActive(false);
        this.EnableControl();
    }

    void OnEnable()
    {
        cursor = GameObject.Find("/Cursor");
    }

    public void EnableControl()
    {
        controls.Player.Enable();

        if (hudDisplay != null)
        {
            hudDisplay.SetActive(true);
        }

        if (appStatus.GetState() == AppState.Status.FIND_TARGET)
        {
            cursor.SetActive(true);
        }
    }

    public void DisableControl()
    {
        controls.Player.Disable();
        if (hudDisplay != null)
        {
            hudDisplay.SetActive(false);
        }
        cursor.SetActive(false); 
    }

 

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            //reset scaling for each adjustment.
            translationStep = Constants.BASE_TRANSLATION;
            rotationStep = Constants.BASE_ROTATION;
            switch (appStatus.GetState())
            {
                case AppState.Status.INTRO_1:
                    appStatus.SetState(AppState.Status.INTRO_2);
                    sounds.PlayClip("enter");
                    break;
                case AppState.Status.INTRO_2:
                    appStatus.SetState(AppState.Status.INTRO_3);
                    sounds.PlayClip("enter");
                    break;
                case AppState.Status.INTRO_3:
                    appStatus.SetState(AppState.Status.EXPLORATION);
                    sounds.PlayClip("enter");
                    break;
                case AppState.Status.EXPLORATION:
                    appStatus.SetState(AppState.Status.FIND_MARKER);
                    sounds.PlayClip("enter");
                    break;
                case AppState.Status.FIND_MARKER:
                    cursor.SetActive(true);
                    sounds.PlayClip("enter");
                    appStatus.SetState(AppState.Status.FIND_TARGET);
                    appStatus.SaveMarker();
                    break;
                case AppState.Status.FIND_TARGET:

                    if (appStatus.AddNewTarget())
                    {
                        sounds.PlayClip("enter");
                        cursor.SetActive(false);
                        appStatus.SetState(AppState.Status.ADJUST_ROTATIONY);
                        toggledControlsOnce = false;
                    }
                    else
                    {
                        sounds.PlayClip("enter failed");
                    }
                break;
                case AppState.Status.ADJUST_ROTATIONY:
                case AppState.Status.ADJUST_ROTATIONX:
                case AppState.Status.ADJUST_NORMAL:
                case AppState.Status.ADJUST_DEPTH:
                    if (!toggledControlsOnce)
                    {
                        statusMessage.text = "Position the target before confirming!";
                    }
                    else
                    {
                        if ((appStatus.GetTargetCount() + 1) >= Constants.NUM_TARGETS)
                        {
                            appStatus.ResetMarkers();

                            appStatus.SetState(AppState.Status.CONFIRM_MARKER);
                        }
                        else
                        {
                            sounds.PlayClip("enter");
                            if ((appStatus.GetTargetCount() + 1) % 2 == 0)
                            {
                                appStatus.SetState(AppState.Status.FIND_MARKER);
                            }
                            else
                            {
                                cursor.SetActive(true);
                                appStatus.SetState(AppState.Status.FIND_TARGET);
                            }
                        }
                        appStatus.ConfirmPlacement();
                    }
                    break;
                case AppState.Status.CONFIRM_MARKER:
                    sounds.PlayClip("enter");
                    if ((appStatus.GetMarkerCount()+1) == Constants.NUM_MARKERS)
                    {
                        appStatus.SetState(AppState.Status.ALIGNMENT_READY);

                    }

                    appStatus.SaveMarker();
                    if (appStatus.GetMarkerCount() != Constants.NUM_MARKERS)
                    {
                        //appStatus.RemoveMarkerAnchor();
                    }

                    break;
                case AppState.Status.ALIGNMENT_READY:
                    appStatus.ComputeAlignment();
                    sounds.PlayClip("enter");
                    break;
            }
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.PlayClip("back");

            switch (appStatus.GetState())
            {

                case AppState.Status.INTRO_2:
                    appStatus.SetState(AppState.Status.INTRO_1);
                    break;
                case AppState.Status.INTRO_3:
                    appStatus.SetState(AppState.Status.INTRO_2);
                    break;
                case AppState.Status.EXPLORATION:
                    appStatus.SetState(AppState.Status.INTRO_3);
                    break;
                case AppState.Status.FIND_MARKER:
                    appStatus.SetState(AppState.Status.EXPLORATION);
                    break;
                case AppState.Status.FIND_TARGET:
                    cursor.SetActive(false);
                    if(appStatus.GetTargetCount() == 0)
                    {
                        appStatus.SetState(AppState.Status.FIND_MARKER);
                        appStatus.DecrementMarker();
                    }
                    else
                    {
                        appStatus.RemoveTarget();
                    }
                break;
                case AppState.Status.ADJUST_ROTATIONY:
                case AppState.Status.ADJUST_ROTATIONX:
                case AppState.Status.ADJUST_NORMAL:
                case AppState.Status.ADJUST_DEPTH:
                    if (!b_PressedOnce)
                    {
                        appStatus.ResetTarget();
                        b_PressedOnce = true;
                    }
                    else
                    {
                        appStatus.RemoveTarget();
                        appStatus.SetState(AppState.Status.FIND_TARGET);
                    }
                    break;
                case AppState.Status.CONFIRM_MARKER:
                    if(appStatus.GetMarkerCount() == 0)
                    {
                        appStatus.RemoveTarget();
                        appStatus.SetState(AppState.Status.FIND_TARGET);
                    }
                    else
                    {
                        appStatus.DecrementMarker();
                        //appStatus.RemoveMarkerAnchor();
                    }
                break;
                case AppState.Status.ALIGNMENT_READY:
                    appStatus.DecrementMarker();
                    appStatus.SetState(AppState.Status.CONFIRM_MARKER);
                break;
            }
        }
    }

    public void OnDpadUp(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            if (translationStep < Constants.BASE_TRANSLATION * Math.Pow(Constants.MULTIPLIER, 4))
            {
                sounds.PlayClip("toggle settings");
                translationStep *= Constants.MULTIPLIER;
                rotationStep *= Constants.MULTIPLIER;
                appStatus.SetMultiplier(translationStep / Constants.BASE_TRANSLATION);
            }
            else
            {
                sounds.PlayClip("toggle settings failed");
            }
        }
    }

    public void OnDpadDown(InputAction.CallbackContext context)
    {
            if (context.started == true)
            {
                if (translationStep > Constants.BASE_TRANSLATION / Math.Pow(Constants.MULTIPLIER, 4))
                {
                    sounds.PlayClip("toggle settings");
                    translationStep /= Constants.MULTIPLIER;
                    rotationStep /= Constants.MULTIPLIER;
                    appStatus.SetMultiplier(translationStep / Constants.BASE_TRANSLATION);
                }
                else
                {
                    sounds.PlayClip("toggle settings failed");
                }
        }
    }

    public void OnDpadRight(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.PlayClip("toggle settings");

            //reset sensitivity:
            //translationStep = Constants.BASE_TRANSLATION;
            //rotationStep = Constants.BASE_ROTATION;
            //appStatus.SetMultiplier(translationStep / Constants.BASE_TRANSLATION);

            switch (appStatus.GetState())
                {
                    case AppState.Status.ADJUST_ROTATIONY:
                        appStatus.SetState(AppState.Status.ADJUST_ROTATIONX);
                        toggledControlsOnce = true;
                        break;
                    case AppState.Status.ADJUST_ROTATIONX:
                        appStatus.SetState(AppState.Status.ADJUST_NORMAL);
                        toggledControlsOnce = true;
                        break;
                    case AppState.Status.ADJUST_NORMAL:
                        appStatus.SetState(AppState.Status.ADJUST_DEPTH);
                        toggledControlsOnce = true;
                        break;
                    case AppState.Status.ADJUST_DEPTH:
                        appStatus.SetState(AppState.Status.ADJUST_ROTATIONY);
                        toggledControlsOnce = true;
                        break;
                }
        }
    }

    public void OnDpadLeft(InputAction.CallbackContext context)
        {
            if (context.started == true)
            {
                sounds.PlayClip("toggle settings");
                //translationStep = Constants.BASE_TRANSLATION;
                //rotationStep = Constants.BASE_ROTATION;
                //appStatus.SetMultiplier(translationStep / Constants.BASE_TRANSLATION);

                switch (appStatus.GetState())
                    {
                        case AppState.Status.ADJUST_ROTATIONY:
                            appStatus.SetState(AppState.Status.ADJUST_DEPTH);
                            toggledControlsOnce = true;
                            break;
                        case AppState.Status.ADJUST_ROTATIONX:
                            appStatus.SetState(AppState.Status.ADJUST_ROTATIONY);
                            toggledControlsOnce = true;
                            break;
                        case AppState.Status.ADJUST_NORMAL:
                            appStatus.SetState(AppState.Status.ADJUST_ROTATIONX);
                            toggledControlsOnce = true;
                            break;
                        case AppState.Status.ADJUST_DEPTH:
                            appStatus.SetState(AppState.Status.ADJUST_NORMAL);
                            toggledControlsOnce = true;
                            break;
                    }
            }
        }
    //Use to control positioning of the hologram
    public void OnLeftStick(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        float x_dir = dir[0];
        float y_dir = dir[1];

        //only take x values
        if (appStatus.GetState() == AppState.Status.ADJUST_ROTATIONY)
        {
            float rotateY = x_dir * rotationStep;
            appStatus.RotateTarget(0, -rotateY, 0);
        }
        //only take y values
        if (appStatus.GetState() == AppState.Status.ADJUST_ROTATIONX)
        {
            float rotateY = y_dir * rotationStep;
            appStatus.RotateTarget(rotateY, 0, 0);
        }
        //take both
        else if (appStatus.GetState() == AppState.Status.ADJUST_NORMAL)
        {
            float translateX = x_dir * translationStep;
            float translateY = y_dir * translationStep;
            appStatus.TranslateTarget(translateX, translateY, 0);

        }
        //take y values
        else if (appStatus.GetState() == AppState.Status.ADJUST_DEPTH)
        {
            float translateZ = y_dir * translationStep;
            appStatus.TranslateTarget(0, 0, translateZ);
        }
    }

    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.PlayClip("open menu");

            //switch control from menu to game
            this.DisableControl();
            menuController.enableControl();

        }
    }
}
