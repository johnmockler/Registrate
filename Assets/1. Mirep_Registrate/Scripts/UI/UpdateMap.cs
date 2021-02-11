using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateMap : MonoBehaviour
{
    GameObject[] targetObjects;
    GameObject[] markerObjects;
    AppState appStatus;
    AppState.Status previousState;
    int previousTargetCount;
    int currentTargetCount;
    int currentMarkerCount;
    int previousMarkerCount;
    Blink activeBlink;
    GameObject activeBlinker;


    void Awake()
    {
        targetObjects = new GameObject[Constants.NUM_TARGETS];
        for (int i = 0; i < Constants.NUM_TARGETS; i++)
        {
            targetObjects[i] = this.gameObject.transform.Find(Constants.BRACKET_TARGETS[i]).gameObject;
        }

        markerObjects = new GameObject[Constants.NUM_MARKERS];
        for (int i = 0; i < Constants.NUM_MARKERS; i++)
        {
            markerObjects[i] = this.gameObject.transform.Find("marker" + i).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        appStatus = AppState.instance;
        previousTargetCount = appStatus.GetTargetCount();
        previousMarkerCount = appStatus.GetMarkerCount();
        UpdateIndicators(previousMarkerCount,previousTargetCount,1);
        UpdateIndicators(previousMarkerCount, previousTargetCount, 0);
        previousState = appStatus.GetState();

        EventManager.OnMarkerFound += OnMapUpdate;
        EventManager.OnTargetPlaced += OnMapUpdate;

    }
    void OnMapUpdate()
    {
        currentTargetCount = appStatus.GetTargetCount();
        currentMarkerCount = appStatus.GetMarkerCount();
        switch (appStatus.GetState())
        {
            case AppState.Status.FIND_MARKER:
            case AppState.Status.CONFIRM_MARKER:
                UpdateIndicators(currentMarkerCount, currentTargetCount, 0);
                break;
            case AppState.Status.FIND_TARGET:
                UpdateIndicators(currentMarkerCount, currentTargetCount, 1);
                break;
            case AppState.Status.ALIGNMENT_READY:
                UpdateIndicators(currentMarkerCount, currentTargetCount, 2);
                break;

        }
    }

    //type = 0: Marker is active, 1: Target is active, 2: nothing is active
    void UpdateIndicators(int markerCount, int targetCount, int type )
    {
        //stop blinking
        if (activeBlinker != null && activeBlinker.GetComponent<Blink>() != null)
        {
            Destroy(activeBlinker.GetComponent<Blink>());
            activeBlinker.SetActive(true);
        }
        //not many markers so we can loop through each time there's an update i think
        for (int i = 0; i < Constants.NUM_MARKERS; i++)
        {

            //completed markers are blue
            if (i < markerCount)
            {
                markerObjects[i].GetComponent<Image>().color = Color.blue;

            }
            //active markers are green
            else if (i == markerCount)
            {                
                if (type == 0)
                {
                    markerObjects[i].GetComponent<Image>().color = Color.green;
                    markerObjects[i].AddComponent(typeof(Blink));// as Blink;
                    activeBlinker = markerObjects[i];//.GetComponent<Blink>();

                }
            }
            //not yet active markers are red
            else
            {
                markerObjects[i].GetComponent<Image>().color = Color.red;
            }

        }

        for (int i = 0; i < Constants.NUM_TARGETS; i++)
        {

            //completed targets are blue
            if (i < targetCount)
            {
                targetObjects[i].GetComponent<Image>().color = Color.blue;

            }
            //active targets are green
            else if (i == targetCount)
            {                
                if(type == 1)
                {
                    targetObjects[i].GetComponent<Image>().color = Color.green;
                    targetObjects[i].AddComponent(typeof(Blink));// as Blink;
                    activeBlinker = targetObjects[i]; //target_objs[i].GetComponent<Blink>();
                }
            }
            //not yet active targets are red
            else
            {
                targetObjects[i].GetComponent<Image>().color = Color.red;
            }
        }
    }

    }

