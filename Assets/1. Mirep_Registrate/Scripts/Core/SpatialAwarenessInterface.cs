using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit;

public class SpatialAwarenessInterface: MonoBehaviour
{
    private static int _meshPhysicsLayer = 0;

    public static GameObject PlaceObject(GameObject object2Place)
    {
        Vector3? positionToPlace = GetPositionOnSpatialMap();
        //find vector with  y vector pointing up, and z vector perpendicular to surface normal. (this requires user to be standing directly in front??
        Transform camTransform = Camera.main.transform;
        Vector3 lookBack = new Vector3(0, 0, 0);

        if (positionToPlace != null)
        {
            return InstantiateObject(object2Place,positionToPlace.Value, lookBack);
        }
        else
        {
            return null;
        }
    }

    private static GameObject InstantiateObject(GameObject object2place, Vector3 position, Vector3 rotation)
    {
        return Instantiate(object2place, position, Quaternion.Euler(rotation));
    }

    public static Vector3? GetPositionOnSpatialMap(float maxDistance = 2)
    {
        RaycastHit hitInfo;
        var transform = Camera.main.transform;
        var headRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(headRay, out hitInfo, maxDistance, GetSpatialMeshMask()))
        {
            return hitInfo.point;
        }
        else
        {
            return headRay.GetPoint(1.5f);
        }
    }

    private static int GetSpatialMeshMask()
    {
        if (_meshPhysicsLayer == 0)
        {
            var spatialMappingConfig =
              CoreServices.SpatialAwarenessSystem.ConfigurationProfile as
                MixedRealitySpatialAwarenessSystemProfile;
            if (spatialMappingConfig != null)
            {
                foreach (var config in spatialMappingConfig.ObserverConfigurations)
                {
                    var observerProfile = config.ObserverProfile
                        as MixedRealitySpatialAwarenessMeshObserverProfile;
                    if (observerProfile != null)
                    {
                        _meshPhysicsLayer |= (1 << observerProfile.MeshPhysicsLayer);
                    }
                }
            }
        }

        return _meshPhysicsLayer;
    }

}
