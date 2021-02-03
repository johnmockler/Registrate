using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//use spatial awareness to place cursor or nearest surface up to 2m or something
//update transform so that its facing me

public class setCursor : MonoBehaviour
{

    void Update()
    {

        Vector3 lookBack = Camera.main.transform.rotation.eulerAngles;
        lookBack.x = lookBack.z = 0f;
        lookBack.y += 180f;
        
        Vector3? positionToPlace = SpatialAwarenessInterface.GetPositionOnSpatialMap();

        if (positionToPlace != null)
        {
            this.transform.position = positionToPlace.Value;
        }
        else
        {
            Ray headRay = new Ray(transform.position, transform.forward);
            this.transform.position =  headRay.GetPoint(2);
        }

        this.transform.rotation = Quaternion.Euler(lookBack);
    }
}
