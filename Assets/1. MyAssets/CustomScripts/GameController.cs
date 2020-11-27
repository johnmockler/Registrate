using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.WSA;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.Utilities;
using System.Collections.Specialized;
using System.Security.Cryptography;

public class GameController: MonoBehaviour, InputHandler.IPlayerActions
{


    GameObject cursor;
    GameObject hudDisplay;

    MenuController menuController;

    //public Constants.Status appStatus;
    AppState appStatus;

    InputHandler controls;


    //Matrix4x4 modelTransform;

    //find a good starting value for these. Reset for each 'a' press.
    float translationStep = Constants.BASE_TRANSLATION;
    float rotationStep = Constants.BASE_ROTATION;

    float MULTIPLIER = 1.5f;

    void Awake()
    {
        controls = new InputHandler();

    }
    void Start()
    {
        //_instructionText.text = "Walk around the scene to initialize device tracking";

        appStatus = AppState.instance;
        controls.Player.SetCallbacks(this);

        hudDisplay = GameObject.Find("/UserInterface/HUD");
        menuController = GameObject.Find("/_MenuController").GetComponent<MenuController>();

        cursor.SetActive(false);
        this.enableControl();
        /*
        targetList = new ModelRegistration.Target[] { new ModelRegistration.Target(new Vector3(0, 0, 0), new Vector3 (0.0867381f, -1.647f, 1.838f), GameObject.Find("/kabinDemonstrator/FirstTarget")),
                       new ModelRegistration.Target(new Vector3(0, 0, 0), new Vector3(0.407f, -1.781f, 1.13927f), GameObject.Find("/kabinDemonstrator/SecondTarget")),
                       new ModelRegistration.Target(new Vector3(0, 0, 0), new Vector3(1.7f, -1.7f, 0.4196f), GameObject.Find("/kabinDemonstrator/ThirdTarget")),
                       new ModelRegistration.Target(new Vector3(0, 0, 0), new Vector3(1.694f, -1.586f, 0.069646f), GameObject.Find("/kabinDemonstrator/FourthTarget"))};
        */

        //Move this to constants. This is the coordinates for a piece of paper
        

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
    }

    public void disableControl()
    {
        controls.Player.Disable();
        if (hudDisplay != null)
        {
            hudDisplay.SetActive(false);

        }
    }

 

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            //reset scaling for each adjustment.
            translationStep = Constants.BASE_TRANSLATION;
            rotationStep = Constants.BASE_ROTATION;
            print(appStatus.getTargetCount());
            switch (appStatus.getState())
            {
                case AppState.Status.EXPLORATION:
                    cursor.SetActive(true);
                    appStatus.setState(AppState.Status.FIND_TARGET);
                    break;
                case AppState.Status.FIND_TARGET:
                    if (appStatus.addNewTarget())
                    {
                        cursor.SetActive(false);
                        appStatus.setState(AppState.Status.ADJUST_ROTATION);
                    }
                    break;
                case AppState.Status.ADJUST_ROTATION:
                case AppState.Status.ADJUST_NORMAL:
                case AppState.Status.ADJUST_DEPTH:
                //add world anchor to object once it is finished being placed
                //Check whether we have found all of the targets.


                if (appStatus.getTargetCount() >= Constants.NUM_TARGETS-1)
                    {
                        appStatus.setState(AppState.Status.ALIGNMENT_READY);

                    }
                    else
                    {
                        appStatus.incrementTarget();

                        cursor.SetActive(true);
                        appStatus.setState(AppState.Status.FIND_TARGET);
                    }

                break;
                //put in appstate maybe?
                case AppState.Status.ALIGNMENT_READY:
                    appStatus.computeAlignment();
                    break;



            }
        }
    }
    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            // 'Use' code here.
            appStatus.resetTarget();
        }

    }
    public void OnDpadUp(InputAction.CallbackContext context)
    {
        // 'Use' code here.
        if (context.started == true)
        {
            if (translationStep < Constants.BASE_TRANSLATION * 10)
            {
                translationStep *= MULTIPLIER;
                rotationStep *= MULTIPLIER;
                appStatus.setMultiplier(translationStep / Constants.BASE_TRANSLATION);
            }

        }
    }
    public void OnDpadDown(InputAction.CallbackContext context)
        {
            // 'Use' code here.
            if (context.started == true)
            {
                if (translationStep > Constants.BASE_TRANSLATION / 10)
                {
                    translationStep /= MULTIPLIER;
                    rotationStep /= MULTIPLIER;
                    appStatus.setMultiplier(translationStep / Constants.BASE_TRANSLATION);
            }



            }
        }
    public void OnDpadRight(InputAction.CallbackContext context)
        {
            if (context.started == true)
            {

                switch (appStatus.getState())
                {
                    case AppState.Status.ADJUST_ROTATION:
                    appStatus.setState(AppState.Status.ADJUST_NORMAL);
                    break;
                    case AppState.Status.ADJUST_NORMAL:
                    appStatus.setState(AppState.Status.ADJUST_DEPTH);
                    break;
                    case AppState.Status.ADJUST_DEPTH:
                    appStatus.setState(AppState.Status.ADJUST_ROTATION);
                    break;
                }
            }
           
        }
    public void OnDpadLeft(InputAction.CallbackContext context)
        {
            if (context.started == true)
            {
                switch (appStatus.getState())
                {
                    case AppState.Status.ADJUST_ROTATION:
                    appStatus.setState(AppState.Status.ADJUST_DEPTH);
                    break;
                    case AppState.Status.ADJUST_NORMAL:
                    appStatus.setState(AppState.Status.ADJUST_ROTATION);
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
            if (appStatus.getState() == AppState.Status.ADJUST_ROTATION)
            {
                float rotateY = x_dir * rotationStep;
                appStatus.rotateTarget(0, rotateY, 0);
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
            //switch control from menu to game
            this.disableControl();
            menuController.enableControl();

        }
    }



    }
