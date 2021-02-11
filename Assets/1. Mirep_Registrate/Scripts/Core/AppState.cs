using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

//Singleton Class to store app state and manage app-wide operations
public class AppState: MonoBehaviour
{
    public enum Status
    {
        INTRO_1,
        INTRO_2,
        INTRO_3,
        EXPLORATION,
        FIND_MARKER,
        FIND_TARGET,
        ADJUST_TARGET,
        ADJUST_ROTATION,
        ADJUST_ROTATIONY,
        ADJUST_ROTATIONX,
        ADJUST_NORMAL,
        ADJUST_DEPTH,
        ADJUST_X,
        ADJUST_Y,
        ADJUST_Z,
        CONFIRM_MARKER,
        REGISTRATION,
        ALIGNMENT_READY,
        ALIGNED,
        FAILED,
        MENU_OPEN

    }

    public static AppState instance = null;
    private Status state;
    public InputHandler controls;
    private ARUWPController markerTracker;

    private GameObject[] placedTargets;
    private GameObject[] markers;

    private Vector3 initialPlacement;
    private Vector3[] targetAxis;
    Matrix4x4 tfModelToWorld;

    private int targetsPlaced;
    private int markersDetected;
    private float multiplier;
    private bool registrationComputed;
    private bool rotateYshown = false;
    private bool translateZshown = false;
    private bool translateXYshown = false;

    [SerializeField]
    public GameObject _objectToPlace;

    [SerializeField]
    public GameObject _objectToPlaceAnimated;

    [SerializeField]
    bool lockToMarker;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject) ;
        }

        this.state = Status.INTRO_1;
        this.controls = new InputHandler();
        this.targetsPlaced = 0;
        this.markersDetected = 0;
        this.multiplier = 1.0f;
    }

    void Start()
    {
        placedTargets = new GameObject[Constants.NUM_TARGETS];

        markers = new GameObject[] { GameObject.Find("/Marker1"), GameObject.Find("/Marker2"),
            GameObject.Find("/Marker3"), GameObject.Find("/Marker4") };

        markerTracker = GameObject.Find("ARUWP Controller").GetComponent< ARUWPController >();
        registrationComputed = false;
        targetAxis = new Vector3[3];
    }

    public Status GetState()
    {
        return this.state;
    }

    public void getControl(ref InputHandler controller)
    {
        controller = this.controls;
        return;
    }

    public int GetTargetCount()
    {
        return targetsPlaced;
    }

    public int GetMarkerCount()
    {
        return markersDetected;
    }

    public float GetMultiplier()
    {
        return multiplier;
    }

    public void SetState(Status newState)
    {
        this.state = newState;
        EventManager.TriggerStateChange();
    }

    public void SetMultiplier(float level)
    {
        this.multiplier = level;
        EventManager.TriggerMultiplierChanged();
    }

    public void IncrementTarget()
    {
        this.targetsPlaced++;
        EventManager.TriggerTargetPlaced();
    }

    public void DecrementTarget()
    {
        this.targetsPlaced--;
        EventManager.TriggerTargetPlaced();
    }

    public void IncrementMarker()
    {
        this.markersDetected++;
        EventManager.TriggerMarkerFound();
    }

    public void DecrementMarker()
    {
        if (this.markersDetected != 0)
        {
            this.markersDetected--;
            EventManager.TriggerMarkerFound();
        }
    }

    public void ConfirmPlacement()
    {
        if (lockToMarker)
        {
            //update 2 with however many targets we want per marker...
            this.placedTargets[this.targetsPlaced].transform.parent = markers[targetsPlaced / 2].transform;
            IncrementTarget();
        }
        else
        {
            AddAnchor();
            IncrementTarget();
        }
    }

    //Set x, y and z axis vectors. 
    public void SetTargetAxis()
    {
        targetAxis[0] = placedTargets[targetsPlaced].transform.right;
        targetAxis[1] = placedTargets[targetsPlaced].transform.up;
        targetAxis[2] = placedTargets[targetsPlaced].transform.forward;

    }

    public bool AddNewTarget()
    {
        /*
        if(this.targets_placed == 0)
        {
            this.placedTargets[this.targets_placed] = SpatialAwarenessInterface.PlaceObject(_objectToPlaceAnimated);
            //perform initial animation

        }
        else
        {
            this.placedTargets[this.targets_placed] = SpatialAwarenessInterface.PlaceObject(_objectToPlace);
        }
        */
        this.placedTargets[this.targetsPlaced] = SpatialAwarenessInterface.PlaceObject(_objectToPlace);
        if (this.placedTargets[this.targetsPlaced] != null)
        {

            initialPlacement = this.placedTargets[this.targetsPlaced].transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Removes anchor from marker so it can be moved, resumes ARToolkit tracking
    public void TrackMarker()
    {
        WorldAnchor anchor = markers[this.markersDetected].AddComponent<WorldAnchor>();
        if (anchor != null)
        {
            Destroy(anchor);
        }

        if (markerTracker.status == ARUWP.ARUWP_STATUS_RUNNING)
        {
            Debug.Log("Marker tracker already running");
        }
        else if (markerTracker.status != ARUWP.ARUWP_STATUS_CTRL_INITIALIZED)
        {
            Debug.Log("Error: marker tracking not initialized");
        }
        else {
            markerTracker.Resume();
            Debug.Log("Marker tracking resumed");
        }
    }

    public void RemoveMarkerAnchor()
    {
        WorldAnchor anchor = markers[this.markersDetected].GetComponent<WorldAnchor>();
        print(anchor);
        if (anchor)
        {
            DestroyImmediate(anchor);
        }
    }  
   

    //adds a world anchor to the marker, pauses tracking, and incremements the marker count
    public void SaveMarker()
    {
        markers[this.markersDetected].AddComponent<WorldAnchor>();
        this.IncrementMarker();

    }

    public void ResetMarkers()
    {
        this.markersDetected = 0;
    }

    //resets the target hologram to the original position (maybe just make you place it again?)
    public void ResetTarget()
    {
        if (this.placedTargets[this.targetsPlaced] != null)
        {
            this.placedTargets[this.targetsPlaced].transform.position = initialPlacement;

        }

        return;
    }

    public void RemoveTarget()
    {
        switch (this.GetState())
        {
                case AppState.Status.ADJUST_ROTATIONY:
                case AppState.Status.ADJUST_ROTATIONX:
                case AppState.Status.ADJUST_NORMAL:
                case AppState.Status.ADJUST_DEPTH:
                    Destroy(this.placedTargets[this.targetsPlaced]);
                    break;
                default:
                    Destroy(this.placedTargets[this.targetsPlaced - 1]);
                    DecrementTarget();
                break;

        }

    }


    public void ResetAll()
    {
        int n = this.placedTargets.Length;
        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                Destroy(this.placedTargets[i]);
            }

            this.placedTargets = new GameObject[Constants.NUM_TARGETS];
            this.targetsPlaced = 0;
            this.markersDetected = 0;
            this.state = Status.FIND_MARKER;
        }

        return;
    }

    public void TranslateTarget(float x, float y, float z)
    {
        placedTargets[targetsPlaced].transform.Translate(x, y, z);
    }

    public void RotateTarget(float x, float y, float z)
    {
        placedTargets[targetsPlaced].transform.Rotate(x, y, z);
    }

    public void ComputeAlignment()
    {
        if(this.ComputeRegistration())
        {
            //this.correctPoints();
            this.state = Status.ALIGNED;
        }
        else
        {
            //change status to failed.
            this.state = Status.FAILED;
        }
    }

    public bool IsRegistered()
    {
        return registrationComputed;
    }

    private void AddAnchor()
    {
        placedTargets[targetsPlaced].GetComponent<WorldAnchor>();
    }


    private bool ComputeRegistration()
    {
        bool result;
        Vector3[] targetCoords = new Vector3[placedTargets.Length];
        for (int i = 0; i < placedTargets.Length; i++)
        {
            targetCoords[i] = placedTargets[i].transform.position;
        }

        Alignment3D modelAlign = new Alignment3D(targetCoords);
        (tfModelToWorld, result) = modelAlign.computeRegistration();
        
        return result;

    }

    //possibly its own class?
    //save transform to txt file or something
    private bool saveTransform()
    {
        return true;
    }

    //load transform and load to variable.
    private bool recoverTransform()
    {
        return true;
    }

    /*
    void correctPoints()
    {
        /*
        Vector3 translation = new Vector3(0, 0, 0);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        translation.x = modelTransform[0, 3];
        translation.y = modelTransform[1, 3];
        translation.z = modelTransform[2, 3];

        rotation = modelTransform.rotation;

        /

        Vector3[] newPoints = new Vector3[targetList.Length];

        for (int i = 0; i < targetList.Length; i++)
        {
            newPoints[i] = modelTransform.TransformPoint(targetList[i].actualCoord);
        }


        for (int i = 0; i < placedTargets.Length; i++)
        {
            placedTargets[i].transform.position = newPoints[i];

        }

    }
     */

}
