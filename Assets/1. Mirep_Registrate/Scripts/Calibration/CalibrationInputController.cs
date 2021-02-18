using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CalibrationInputController : MonoBehaviour, InputHandler.IPlayerActions
{
    // Start is called before the first frame update
    GameObject hudDisplay;
    TextMesh statusMessage;
    CalibrationAppState appStatus;

    private Sounds sounds;

    float translationStep = Constants.BASE_TRANSLATION;
    float rotationStep = Constants.BASE_ROTATION;

    InputHandler controls;

    bool b_PressedOnce = false;
    bool toggledControlsOnce = false;

    void Awake()
    {
        controls = new InputHandler();

        this.sounds = GetComponent<Sounds>();
    }
    void Start()
    {
        hudDisplay = GameObject.Find("/UserInterface/HUD");
        statusMessage = GameObject.Find("/UserInterface/HUD/StatusMsg").GetComponent<TextMesh>();
        this.sounds = GetComponent<Sounds>();
        appStatus = GameObject.Find("AppStatus").GetComponent<CalibrationAppState>();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            print("HERE");
            //reset scaling for each adjustment.
            translationStep = Constants.BASE_TRANSLATION;
            rotationStep = Constants.BASE_ROTATION;
            sounds.PlayClip("enter");

            switch(appStatus.GetState())
            {
                case AppState.Status.INTRO_1:
                    appStatus.SetState(AppState.Status.ADJUST_ROTATIONY);
                break;
                case AppState.Status.ADJUST_ROTATIONY:
                case AppState.Status.ADJUST_Y:
                case AppState.Status.ADJUST_X:
                case AppState.Status.ADJUST_Z:
                    appStatus.InitCalibration();
                break;
            }
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.PlayClip("back");
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
            translationStep = Constants.BASE_TRANSLATION;
            rotationStep = Constants.BASE_ROTATION;
            appStatus.SetMultiplier(translationStep / Constants.BASE_TRANSLATION);

            switch (appStatus.GetState())
            {
                case AppState.Status.ADJUST_ROTATIONY:
                appStatus.SetState(AppState.Status.ADJUST_Y);
                toggledControlsOnce = true;
                break;
                case AppState.Status.ADJUST_Y:
                appStatus.SetState(AppState.Status.ADJUST_X);
                toggledControlsOnce = true;
                break;
                case AppState.Status.ADJUST_X:
                appStatus.SetState(AppState.Status.ADJUST_Z);
                toggledControlsOnce = true;
                break;
                case AppState.Status.ADJUST_Z:
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
            translationStep = Constants.BASE_TRANSLATION;
            rotationStep = Constants.BASE_ROTATION;
            appStatus.SetMultiplier(translationStep / Constants.BASE_TRANSLATION);

            switch (appStatus.GetState())
            {
                case AppState.Status.ADJUST_ROTATIONY:
                appStatus.SetState(AppState.Status.ADJUST_Z);
                toggledControlsOnce = true;
                break;
                case AppState.Status.ADJUST_Y:
                appStatus.SetState(AppState.Status.ADJUST_ROTATIONY);
                toggledControlsOnce = true;
                break;
                case AppState.Status.ADJUST_X:
                appStatus.SetState(AppState.Status.ADJUST_Y);
                toggledControlsOnce = true;
                break;
                case AppState.Status.ADJUST_Z:
                appStatus.SetState(AppState.Status.ADJUST_X);
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
        if (appStatus.GetState() == AppState.Status.ADJUST_Y)
        {
            float translateY = y_dir * translationStep;
            appStatus.TranslateTarget(0, translateY, 0);
        }
        //take both
        else if (appStatus.GetState() == AppState.Status.ADJUST_X)
        {
            float translateX = x_dir * translationStep;
            appStatus.TranslateTarget(translateX, 0, 0);

        }
        //take y values
        else if (appStatus.GetState() == AppState.Status.ADJUST_Z)
        {
            float translateZ = x_dir * translationStep;
            appStatus.TranslateTarget(0, 0, translateZ);
        }
    }

    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
        }
    }
}
