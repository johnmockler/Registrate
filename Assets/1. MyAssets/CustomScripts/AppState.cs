using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

//Singleton Class to store app state
public class AppState: MonoBehaviour
{
    public enum Status
    {
        EXPLORATION,
        FIND_TARGET,
        ADJUST_TARGET,
        ADJUST_ROTATION,
        ADJUST_NORMAL,
        ADJUST_DEPTH,
        ADJUST_X,
        ADJUST_Y,
        ADJUST_Z,
        REGISTRATION,
        ALIGNMENT_READY,
        ALIGNED

    }

    public static AppState instance = null;

    private Status state;
    InputHandler controls;
    private int targets_placed;
    private int markers_detected;
    private float multiplier;
    private bool registrationComputed;
    private Vector3 initialPlacement;
    public GameObject[] placedTargets;
    Transform modelTransform;


    [SerializeField]
    public GameObject _objectToPlace;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject) ;
        }

        state = Status.EXPLORATION;
        controls = new InputHandler();
        targets_placed = 0;
        markers_detected = 0;
        multiplier = 1.0f;
    }

    void Start()
    {
        placedTargets = new GameObject[4];
        registrationComputed = false;
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
        this.addAnchor();
    }

    public void decrementMarker()
    {
        this.markers_detected--;
    }

    public bool addNewTarget()
    {
        placedTargets[targets_placed] = SpatialAwarenessInterface.PlaceObject(_objectToPlace);
        if (placedTargets[targets_placed] != null)
        {
            initialPlacement = placedTargets[targets_placed].transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool resetTarget()
    {
        if (placedTargets[targets_placed] != null)
        {
            placedTargets[targets_placed].transform.position = initialPlacement;
            return true;
        }
        else
        {
            return false;
        }
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
        }
        else
        {
            //change status to failed.
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

    //Helper Functions Move these somewhere else??

    private bool computeRegistration()
    {
        bool result;
        Matrix4x4 tfMatrix;

        Alignment3D modelAlign = new Alignment3D.Alignment3D(placedTargets);
        (modelTransform, result) = modelAlign.computeRegistration();
        
        return result;

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
