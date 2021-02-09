﻿using System;
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
    private ARUWPController marker_tracker;

    private GameObject[] placedTargets;
    private GameObject[] markers;

    private Vector3 initialPlacement;
    private Vector3[] targetAxis;
    Matrix4x4 tfModelToWorld;

    private int targets_placed;
    private int markers_detected;
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

        print(instance == this);

        this.state = Status.INTRO_1;
        this.controls = new InputHandler();
        this.targets_placed = 0;
        this.markers_detected = 0;
        this.multiplier = 1.0f;
    }

    void Start()
    {
        placedTargets = new GameObject[Constants.NUM_TARGETS];

        markers = new GameObject[] { GameObject.Find("/Marker1"), GameObject.Find("/Marker2"),
            GameObject.Find("/Marker3"), GameObject.Find("/Marker4") };

        marker_tracker = GameObject.Find("ARUWP Controller").GetComponent< ARUWPController >();
        registrationComputed = false;
        targetAxis = new Vector3[3];
    }

    public Status getState()
    {
        return this.state;
    }

    public void getControl(ref InputHandler controller)
    {
        controller = this.controls;
        return;
    }

    public int getTargetCount()
    {
        return targets_placed;
    }

    public int getMarkerCount()
    {
        return markers_detected;
    }

    public float getMultiplier()
    {
        return multiplier;
    }

    public void setState(Status newState)
    {
        this.state = newState;
        if(newState == Status.ADJUST_DEPTH && !translateZshown)
        {
            this.placedTargets[this.targets_placed].GetComponent<AnimationManager>().TranslateZ();
            translateZshown = true;
        }
        else if (newState == Status.ADJUST_NORMAL && !translateXYshown)
        {
            this.placedTargets[this.targets_placed].GetComponent<AnimationManager>().TranslateXY();
            translateXYshown = true;
        }
    }

    public void setMultiplier(float level)
    {
        this.multiplier = level;
    }

    public void incrementTarget()
    {
        this.targets_placed++;
    }
    
    public void decrementTarget()
    {
        this.targets_placed--;
    }

    public void incrementMarker()
    {
        this.markers_detected++;
    }

    public void decrementMarker()
    {
        if (this.markers_detected != 0)
        {
            this.markers_detected--;
        }
    }

    public void confirmPlacement()
    {
        if (lockToMarker)
        {
            //update 2 with however many targets we want per marker...
            this.placedTargets[this.targets_placed].transform.parent = markers[targets_placed / 2].transform;
            incrementTarget();
        }
        else
        {
            addAnchor();
            incrementTarget();
        }
    }

    //Set x, y and z axis vectors. 
    public void setTargetAxis()
    {
        targetAxis[0] = placedTargets[targets_placed].transform.right;
        targetAxis[1] = placedTargets[targets_placed].transform.up;
        targetAxis[2] = placedTargets[targets_placed].transform.forward;

    }

    public bool addNewTarget()
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
        this.placedTargets[this.targets_placed] = SpatialAwarenessInterface.PlaceObject(_objectToPlace);
        if (this.placedTargets[this.targets_placed] != null)
        {
            /*
            if (!rotateYshown)
            {
                this.placedTargets[this.targets_placed].GetComponent<AnimationManager>().RotateY();
                rotateYshown = true;
            }*/

            initialPlacement = this.placedTargets[this.targets_placed].transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Removes anchor from marker so it can be moved, resumes ARToolkit tracking
    public void trackMarker()
    {
        WorldAnchor anchor = markers[this.markers_detected].AddComponent<WorldAnchor>();
        if (anchor != null)
        {
            Destroy(anchor);
        }

        if (marker_tracker.status == ARUWP.ARUWP_STATUS_RUNNING)
        {
            Debug.Log("Marker tracker already running");
        }
        else if (marker_tracker.status != ARUWP.ARUWP_STATUS_CTRL_INITIALIZED)
        {
            Debug.Log("Error: marker tracking not initialized");
        }
        else {
            marker_tracker.Resume();
            Debug.Log("Marker tracking resumed");
        }
    }

    //adds a world anchor to the marker, pauses tracking, and incremements the marker count
    public void saveMarker()
    {
        markers[this.markers_detected].AddComponent<WorldAnchor>();


        if (marker_tracker.status == ARUWP.ARUWP_STATUS_CTRL_INITIALIZED)
        {
            Debug.Log("Marker Tracking already paused");
        }
        else if (marker_tracker.status != ARUWP.ARUWP_STATUS_RUNNING)
        {
            Debug.Log("Error: not tracking marker");
        }
        else
        {
            marker_tracker.Pause();
            Debug.Log("Marker tracking paused");
        }

        this.incrementMarker();

    }

    public void resetMarkers()
    {
        this.markers_detected = 0;
    }

    //resets the target hologram to the original position (maybe just make you place it again?)
    public void resetTarget()
    {
        if (this.placedTargets[this.targets_placed] != null)
        {
            this.placedTargets[this.targets_placed].transform.position = initialPlacement;

        }

        return;
    }

    public void removeTarget()
    {
        switch (this.getState())
        {
                case AppState.Status.ADJUST_ROTATIONY:
                case AppState.Status.ADJUST_ROTATIONX:
                case AppState.Status.ADJUST_NORMAL:
                case AppState.Status.ADJUST_DEPTH:
                    Destroy(this.placedTargets[this.targets_placed]);
                    break;
                default:
                    Destroy(this.placedTargets[this.targets_placed - 1]);
                    decrementTarget();
                break;

        }

    }


    public void resetAll()
    {
        int n = this.placedTargets.Length;
        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                Destroy(this.placedTargets[i]);
            }

            this.placedTargets = new GameObject[Constants.NUM_TARGETS];
            this.targets_placed = 0;
            this.markers_detected = 0;
            this.state = Status.FIND_MARKER;
        }

        return;
    }

    public void translateTarget(float x, float y, float z)
    {
        placedTargets[targets_placed].transform.Translate(x, y, z);
    }

    public void rotateTarget(float x, float y, float z)
    {
        placedTargets[targets_placed].transform.Rotate(x, y, z);
    }

    public void computeAlignment()
    {
        if(this.computeRegistration())
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

    public bool isRegistered()
    {
        return registrationComputed;
    }

    private void addAnchor()
    {
        placedTargets[targets_placed].GetComponent<WorldAnchor>();
    }


    private bool computeRegistration()
    {
        bool result;
        print("here");
        Vector3[] targetCoords = new Vector3[placedTargets.Length];
        for (int i = 0; i < placedTargets.Length; i++)
        {
            targetCoords[i] = placedTargets[i].transform.position;
        }

        Alignment3D modelAlign = new Alignment3D(targetCoords);
        print("here1");
        (tfModelToWorld, result) = modelAlign.computeRegistration();
        print(result);
        
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
