using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MenuController : MonoBehaviour, InputHandler.IPlayerActions
{
    InputHandler controls;
    AppState appStatus;

    private int numberOfOptions = 2;
    private int selectedOption;

    private GameObject placeMap;
    private GameObject resetCalib;
    private GameObject hudDisplay;

    private GameController gameController;

    void Awake()
    {

        appStatus = AppState.instance;
        print("i am here");

    }

    // Start is called before the first frame update
    void Start()
    {
        placeMap = GameObject.Find("/UserInterface/MenuScreen/Options/PlaceMap/place_indicator");
        resetCalib = GameObject.Find("/UserInterface/MenuScreen/Options/PlaceMap/reset_indicator");
        gameController = GameObject.Find("/_GameController").GetComponent(typeof(GameController)) as GameController ;
    }


    //should this be in Awake or OnEnable??
    void OnEnable()
    {
        //controls = new InputHandler();
        appStatus.getControl(ref controls);
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

        this.gameObject.SetActive(true);

        selectedOption = 1;
        placeMap.SetActive(true);
        resetCalib.SetActive(false);

    }

    void OnDisable()
    {

        this.gameObject.SetActive(false);
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
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

    }
    public void OnDpadUp(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
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

    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            //switch control from menu to game
            gameController.enabled = true;
            this.enabled = false;
        }
    }
    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            //switch control from menu to game
            gameController.enabled = true;
            this.enabled = false;
        }
    }

    //Unused
    public void OnDpadRight(InputAction.CallbackContext context) { }
    public void OnDpadLeft(InputAction.CallbackContext context) { }
    public void OnLeftStick(InputAction.CallbackContext context) { }

    void placeMapOnMesh()
    {

    }

    void resetCalibration()
    {

    }
}
