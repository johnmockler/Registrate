using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

//Singleton Class to store app state and manage app-wide operations
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
    public InputHandler controls;
    private int targets_placed;
    private int markers_detected;
    private float multiplier;
    private bool registrationComputed;
    private Vector3 initialPlacement;
    public GameObject[] placedTargets;
    Matrix4x4 tfModelToWorld;


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

        print(instance == this);

        this.state = Status.EXPLORATION;
        this.controls = new InputHandler();
        this.targets_placed = 0;
        this.markers_detected = 0;
        this.multiplier = 1.0f;
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
        this.placedTargets[this.targets_placed] = SpatialAwarenessInterface.PlaceObject(_objectToPlace);
        if (this.placedTargets[this.targets_placed] != null)
        {
            initialPlacement = this.placedTargets[this.targets_placed].transform.position;
            return true;
        }
        else
        {
            return false;
        }
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

    public void resetState()
    {
        if (this.targets_placed > 0)
        {
            for (int i = 0; i < this.targets_placed; i++)
            {
                Destroy(this.placedTargets[i]);
            }

            this.targets_placed = 0;
            this.state = Status.FIND_TARGET;
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
            this.state = Status.EXPLORATION;
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


    private bool computeRegistration()
    {
        bool result;
        print("here");
        Alignment3D modelAlign = new Alignment3D(placedTargets);
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
