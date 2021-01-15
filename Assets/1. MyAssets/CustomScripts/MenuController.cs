﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MenuController : MonoBehaviour, InputHandler.IUserInterfaceActions
{
    InputHandler controls;
    AppState appStatus;

    public GameObject placeableMap;

    private int numberOfOptions = 2;
    private int selectedOption;

    private GameObject placeMap;
    private GameObject resetCalib;
    private GameObject menuDisplay;
    private GameObject modelImage;
    private GameObject popOutModel;
    private GameObject cursor;

    private GameController gameController;

    private bool placingMap;
    private Sounds sounds;



    void Awake()
    {
        //create a singleton for this as well..
        controls = new InputHandler();
        this.sounds = GetComponent<Sounds>();

        //here so that it is found before it becomes invisible.


    }

    // Start is called before the first frame update
    void Start()
    {
        appStatus = AppState.instance;
        controls.UserInterface.SetCallbacks(this);




        placeMap = GameObject.Find("/UserInterface/MenuScreen/Options/PlaceMap/place_indicator");
        resetCalib = GameObject.Find("/UserInterface/MenuScreen/Options/ResetCalibration/reset_indicator");
        gameController = GameObject.Find("/_GameController").GetComponent<GameController>(); ;
        modelImage = GameObject.Find("/UserInterface/MenuScreen/Model/ModelImage");
        menuDisplay = GameObject.Find("/UserInterface/MenuScreen");

        menuDisplay.SetActive(false);

        print(resetCalib);
        placingMap = false;
    }
    void OnEnable()
    {
        cursor = GameObject.Find("/Cursor");

    }

    public void enableControl()
    {
        controls.UserInterface.Enable();
        print("Menu Controller is enabled");
        selectedOption = 1;
        menuDisplay.SetActive(true);
        placeMap.SetActive(true);
        resetCalib.SetActive(false);

    }

    public void disableControl()
    {
        print("Menu Controller is disabled");
        controls.UserInterface.Disable();
        menuDisplay.SetActive(false);
    }


    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            if(!placingMap)
            {
                sounds.playClip("enter");

                switch (selectedOption)
                {
                    case 1:
                        placeMapOnMesh();
                        break;
                    case 2:
                        resetCalibration();
                        break;
                }
            }
            else
            {
                print("i'm here");
                //instantiate map on spatial mesh
                popOutModel = SpatialAwarenessInterface.PlaceObject(placeableMap);
                if(popOutModel != null)
                {
                    sounds.playClip("target placed");
                    menuDisplay.SetActive(true);
                    cursor.SetActive(false);
                    placingMap = false;
                }
                else
                {
                    sounds.playClip("enter failed");

                }

            }

        }

    }
    public void OnDpadUp(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.playClip("toggle settings");

            selectedOption += 1;

            placeMap.SetActive(false);
            resetCalib.SetActive(false);

            if (selectedOption > numberOfOptions)
            {
                selectedOption = 1;
            }
            switch (selectedOption)
            {
                case 1:
                    placeMap.SetActive(true);
                    break;
                case 2:
                    resetCalib.SetActive(true);
                    break;
            }
        }


    }
    public void OnDpadDown(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.playClip("toggle settings");

            selectedOption -= 1;

            placeMap.SetActive(false);
            resetCalib.SetActive(false);

            if (selectedOption < 1)
            {
                selectedOption = numberOfOptions;
            }
            switch (selectedOption)
            {
                case 1:
                    placeMap.SetActive(true);
                    break;
                case 2:
                    resetCalib.SetActive(true);
                    break;
            }
        }
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.playClip("close menu");

            //switch control from menu to game
            switchControl();

        }
    }
    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.playClip("close menu");

            //switch control from menu to game
            switchControl();
        }
    }

    //Unused
    public void OnDpadRight(InputAction.CallbackContext context) { }
    public void OnDpadLeft(InputAction.CallbackContext context) { }
    public void OnLeftStick(InputAction.CallbackContext context) { }

    void placeMapOnMesh()
    {
        if(popOutModel == null)
        {
            //make menu dispear, activate cursor. On enter, place map on spatial mesh, and add a script to update the indicators(maybe it will be included);
            placingMap = true;
            cursor.SetActive(true);
            menuDisplay.SetActive(false);

        }
        else
        {
            //remove map
            Destroy(popOutModel);
        }
    }

    void resetCalibration()
    {
        print("resetting");
        appStatus.resetAll();
        switchControl();

    }

    void switchControl()
    {

        this.disableControl();
        gameController.enableControl();
    }
}
