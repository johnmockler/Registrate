using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationAppState : AppState
{
    GameObject calibCube;
    CalibrationController calibrationController;
    // Start is called before the first frame update
    void Start()
    {
        calibCube = GameObject.Find("CalibCube");
        calibrationController = GameObject.Find("ARToolKit").GetComponent<CalibrationController>();
    }
    public void InitCalibration()
    {
        calibrationController.InitCalibration();
    }    
    public new void TranslateTarget(float x, float y, float z)
    {
        calibCube.transform.Translate(x, y, z);
    }

    public new void RotateTarget(float x, float y, float z)
    {
        calibCube.transform.Rotate(x, y, z);
    }
}
