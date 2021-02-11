using System.Collections;
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
    private TextMesh status_msg;
    private GameController gameController;

    private bool placingMap;
    private Sounds sounds;

    AppState.Status previousState;

    //control tips
    GameObject controlTips;
    GameObject a_button;
    TextMesh a_text;
    GameObject b_button;
    TextMesh b_text;
    GameObject dpad;



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



        status_msg = GameObject.Find("StatusMsg").GetComponent<TextMesh>();
        placeMap = GameObject.Find("/UserInterface/MenuScreen/Options/PlaceMap/place_indicator");
        resetCalib = GameObject.Find("/UserInterface/MenuScreen/Options/ResetCalibration/reset_indicator");
        gameController = GameObject.Find("/_GameController").GetComponent<GameController>(); ;
        modelImage = GameObject.Find("/UserInterface/MenuScreen/Model/ModelImage");
        menuDisplay = GameObject.Find("/UserInterface/MenuScreen");
        controlTips = GameObject.Find("/UserInterface/ControlTips");
        dpad = GameObject.Find("/UserInterface/ControlTips/manipulation_controls");
        a_text = GameObject.Find("/UserInterface/ControlTips/main_controls/a_button/a_button_text").GetComponent<TextMesh>();
        b_text = GameObject.Find("/UserInterface/ControlTips/main_controls/b_button/b_button_text").GetComponent<TextMesh>();

        menuDisplay.SetActive(false);

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
        previousState = appStatus.GetState();
        appStatus.SetState(AppState.Status.MENU_OPEN);
        controlTips.SetActive(false);

    }

    public void disableControl()
    {
        print("Menu Controller is disabled");
        controls.UserInterface.Disable();
        menuDisplay.SetActive(false);
        appStatus.SetState(previousState);
        controlTips.SetActive(true);
    }


    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            if(!placingMap)
            {
                sounds.PlayClip("enter");

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
                //instantiate map on spatial mesh
                
                popOutModel = SpatialAwarenessInterface.PlaceObject(placeableMap);
           
                if(popOutModel != null)
                {
                    sounds.PlayClip("target placed");
                    menuDisplay.SetActive(true);
                    cursor.SetActive(false);
                    placingMap = false;
                    controlTips.SetActive(false);
                }
                else
                {
                    sounds.PlayClip("enter failed");

                }

            }

        }

    }
    public void OnDpadUp(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.PlayClip("toggle settings");

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
            sounds.PlayClip("toggle settings");

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
            sounds.PlayClip("close menu");

            //switch control from menu to game
            switchControl();

        }
    }
    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            sounds.PlayClip("close menu");

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
            controlTips.SetActive(true);
            dpad.SetActive(false);
            a_text.text = "Place";
            b_text.text = "Cancel";
        }
        else
        {
            //remove map
            Destroy(popOutModel);
        }
    }

    void resetCalibration()
    {
        appStatus.ResetAll();
        switchControl();

    }

    void switchControl()
    {

        this.disableControl();
        gameController.EnableControl();
    }
}
