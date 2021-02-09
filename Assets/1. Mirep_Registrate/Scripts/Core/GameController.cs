﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController: MonoBehaviour, InputHandler.IPlayerActions
{


    GameObject cursor;
    GameObject hudDisplay;

    MenuController menuController;

    AppState appStatus;

    InputHandler controls;

    private Sounds sounds;

    float translationStep = Constants.BASE_TRANSLATION;
    float rotationStep = Constants.BASE_ROTATION;

    bool rotateYshown = false;
    bool translateXYshown = false;
    bool translateZshown = false;

    bool b_PressedOnce = false;

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

        this.sounds = GetComponent<Sounds>();
        cursor.SetActive(false);
        this.enableControl();
    }

    void OnEnable()
    {
        cursor = GameObject.Find("/Cursor");
    }

    public void enableControl()
    {
        controls.Player.Enable();

        if (hudDisplay != null)
        {
            hudDisplay.SetActive(true);
        }

        if (appStatus.getState() == AppState.Status.FIND_TARGET)
        {
            cursor.SetActive(true);
        }
    }

    public void disableControl()
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
            switch (appStatus.getState())
            {
                case AppState.Status.INTRO_1:
                    appStatus.setState(AppState.Status.INTRO_2);
                    sounds.playClip("enter");
                    break;
                case AppState.Status.INTRO_2:
                    appStatus.setState(AppState.Status.INTRO_3);
                    sounds.playClip("enter");
                    break;
                case AppState.Status.INTRO_3:
                    appStatus.setState(AppState.Status.EXPLORATION);
                    sounds.playClip("enter");
                    break;
                case AppState.Status.EXPLORATION:
                    appStatus.setState(AppState.Status.FIND_MARKER);
                    appStatus.trackMarker();
                    sounds.playClip("enter");
                    break;
                case AppState.Status.FIND_MARKER:
                    cursor.SetActive(true);
                    sounds.playClip("enter");
                    appStatus.saveMarker();
                    appStatus.setState(AppState.Status.FIND_TARGET);
                    break;
                case AppState.Status.FIND_TARGET:

                    if (appStatus.addNewTarget())
                    {
                        sounds.playClip("enter");
                        cursor.SetActive(false);
                        appStatus.setState(AppState.Status.ADJUST_ROTATIONY);
                    }
                    else
                    {
                        sounds.playClip("enter failed");
                    }
                break;
                case AppState.Status.ADJUST_ROTATIONY:
                case AppState.Status.ADJUST_ROTATIONX:
                case AppState.Status.ADJUST_NORMAL:
                case AppState.Status.ADJUST_DEPTH:
                    appStatus.confirmPlacement();
                    if (appStatus.getTargetCount() >= Constants.NUM_TARGETS)
                        {
                            appStatus.setState(AppState.Status.CONFIRM_MARKER);
                            appStatus.resetMarkers();
                            appStatus.trackMarker();
                        }
                        else
                        {
                            sounds.playClip("enter");
                            print(appStatus.getTargetCount()%2);
                            if (appStatus.getTargetCount()%2 == 0)
                            {
                                appStatus.setState(AppState.Status.FIND_MARKER);
                                appStatus.trackMarker();
                            }
                            else
                            {
                                cursor.SetActive(true);
                                appStatus.setState(AppState.Status.FIND_TARGET);
                            }
                        }

                    break;
                case AppState.Status.CONFIRM_MARKER:
                    sounds.playClip("enter");
                    appStatus.saveMarker();
                    if (appStatus.getMarkerCount() == Constants.NUM_MARKERS)
                    {
                        appStatus.setState(AppState.Status.ALIGNMENT_READY);
                    }
                    else
                    {
                        appStatus.trackMarker();
                    }
                    break;
                case AppState.Status.ALIGNMENT_READY:
                    appStatus.computeAlignment();
                    sounds.playClip("enter");
                    break;
            }
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.playClip("back");

            switch (appStatus.getState())
            {

                case AppState.Status.INTRO_2:
                    appStatus.setState(AppState.Status.INTRO_1);
                    break;
                case AppState.Status.INTRO_3:
                    appStatus.setState(AppState.Status.INTRO_2);
                    break;
                case AppState.Status.EXPLORATION:
                    appStatus.setState(AppState.Status.INTRO_3);
                    break;
                case AppState.Status.FIND_MARKER:
                    appStatus.setState(AppState.Status.EXPLORATION);
                    break;
                case AppState.Status.FIND_TARGET:
                    cursor.SetActive(false);
                    if(appStatus.getTargetCount() == 0)
                    {
                        appStatus.setState(AppState.Status.FIND_MARKER);
                        appStatus.decrementMarker();
                        appStatus.trackMarker();
                    }
                    else
                    {
                        appStatus.removeTarget();
                    }
                break;
                case AppState.Status.ADJUST_ROTATIONY:
                case AppState.Status.ADJUST_ROTATIONX:
                case AppState.Status.ADJUST_NORMAL:
                case AppState.Status.ADJUST_DEPTH:
                    if (!b_PressedOnce)
                    {
                        appStatus.resetTarget();
                        b_PressedOnce = true;
                    }
                    else
                    {
                        appStatus.removeTarget();
                        appStatus.setState(AppState.Status.FIND_TARGET);
                    }
                    break;
                case AppState.Status.CONFIRM_MARKER:
                    if(appStatus.getMarkerCount() == 0)
                    {
                        appStatus.removeTarget();
                        appStatus.setState(AppState.Status.FIND_TARGET);
                    }
                    else
                    {
                        appStatus.decrementMarker();
                        appStatus.trackMarker();
                    }
                break;
                case AppState.Status.ALIGNMENT_READY:
                    appStatus.decrementMarker();
                    appStatus.trackMarker();
                    appStatus.setState(AppState.Status.CONFIRM_MARKER);
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
                sounds.playClip("toggle settings");
                translationStep *= Constants.MULTIPLIER;
                rotationStep *= Constants.MULTIPLIER;
                appStatus.setMultiplier(translationStep / Constants.BASE_TRANSLATION);
            }
            else
            {
                sounds.playClip("toggle settings failed");
            }
        }
    }

    public void OnDpadDown(InputAction.CallbackContext context)
    {
            if (context.started == true)
            {
                if (translationStep > Constants.BASE_TRANSLATION / Math.Pow(Constants.MULTIPLIER, 4))
                {
                    sounds.playClip("toggle settings");
                    translationStep /= Constants.MULTIPLIER;
                    rotationStep /= Constants.MULTIPLIER;
                    appStatus.setMultiplier(translationStep / Constants.BASE_TRANSLATION);
                }
                else
                {
                    sounds.playClip("toggle settings failed");
                }
        }
    }

    public void OnDpadRight(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.playClip("toggle settings");

            //reset sensitivity:
            translationStep = Constants.BASE_TRANSLATION;
            rotationStep = Constants.BASE_ROTATION;
            appStatus.setMultiplier(translationStep / Constants.BASE_TRANSLATION);

            switch (appStatus.getState())
                {
                    case AppState.Status.ADJUST_ROTATIONY:
                        appStatus.setState(AppState.Status.ADJUST_ROTATIONX);
                        break;
                    case AppState.Status.ADJUST_ROTATIONX:
                        appStatus.setState(AppState.Status.ADJUST_NORMAL);
                        break;
                    case AppState.Status.ADJUST_NORMAL:
                        appStatus.setState(AppState.Status.ADJUST_DEPTH);
                        break;
                    case AppState.Status.ADJUST_DEPTH:
                        appStatus.setState(AppState.Status.ADJUST_ROTATIONY);
                        break;
                }
        }
    }

    public void OnDpadLeft(InputAction.CallbackContext context)
        {
            if (context.started == true)
            {
                sounds.playClip("toggle settings");
                translationStep = Constants.BASE_TRANSLATION;
                rotationStep = Constants.BASE_ROTATION;
                appStatus.setMultiplier(translationStep / Constants.BASE_TRANSLATION);

                switch (appStatus.getState())
                    {
                        case AppState.Status.ADJUST_ROTATIONY:
                            appStatus.setState(AppState.Status.ADJUST_DEPTH);
                            break;
                        case AppState.Status.ADJUST_ROTATIONX:
                            appStatus.setState(AppState.Status.ADJUST_ROTATIONY);
                            break;
                        case AppState.Status.ADJUST_NORMAL:
                            appStatus.setState(AppState.Status.ADJUST_ROTATIONX);
                            break;
                        case AppState.Status.ADJUST_DEPTH:
                            appStatus.setState(AppState.Status.ADJUST_NORMAL);
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
        if (appStatus.getState() == AppState.Status.ADJUST_ROTATIONY)
        {
            float rotateY = x_dir * rotationStep;
            appStatus.rotateTarget(0, -rotateY, 0);
        }
        //only take y values
        if (appStatus.getState() == AppState.Status.ADJUST_ROTATIONX)
        {
            float rotateY = y_dir * rotationStep;
            appStatus.rotateTarget(rotateY, 0, 0);
        }
        //take both
        else if (appStatus.getState() == AppState.Status.ADJUST_NORMAL)
        {
            float translateX = x_dir * translationStep;
            float translateY = y_dir * translationStep;
            appStatus.translateTarget(translateX, translateY, 0);

        }
        //take y values
        else if (appStatus.getState() == AppState.Status.ADJUST_DEPTH)
        {
            float translateZ = y_dir * translationStep;
            appStatus.translateTarget(0, 0, translateZ);
        }
    }

    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.playClip("open menu");

            //switch control from menu to game
            this.disableControl();
            menuController.enableControl();

        }
    }
}
