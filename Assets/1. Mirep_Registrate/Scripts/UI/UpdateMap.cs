using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateMap : MonoBehaviour
{
    GameObject[] target_objs;
    GameObject[] marker_objs;
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
        target_objs = new GameObject[Constants.NUM_TARGETS];
        for (int i = 0; i < Constants.NUM_TARGETS; i++)
        {
            target_objs[i] = this.gameObject.transform.Find(Constants.BRACKET_TARGETS[i]).gameObject;
        }

        marker_objs = new GameObject[Constants.NUM_MARKERS];
        for (int i = 0; i < Constants.NUM_MARKERS; i++)
        {
            marker_objs[i] = this.gameObject.transform.Find("marker" + i).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        appStatus = AppState.instance;
        previousTargetCount = appStatus.getTargetCount();
        previousMarkerCount = appStatus.getMarkerCount();
        updateIndicators(previousMarkerCount,previousTargetCount,1);
        updateIndicators(previousMarkerCount, previousTargetCount, 0);
        previousState = appStatus.getState();


    }
    // Update is called once per frame
    void Update()
    {
        currentTargetCount = appStatus.getTargetCount();
        currentMarkerCount = appStatus.getMarkerCount();
        
        if (appStatus.getState() == AppState.Status.FIND_TARGET)// || appStatus.getState() == AppState.Status.ALIGNMENT_READY || appStatus.getState() == AppState.Status.FIND_MARKER)
        {
            if (currentTargetCount != previousTargetCount || appStatus.getState() != previousState)// || previousState == AppState.Status.FIND_MARKER)
            {
                updateIndicators(currentMarkerCount, currentTargetCount, 1);
            }
        }
        else if (appStatus.getState() == AppState.Status.FIND_MARKER)
        {
            if (currentMarkerCount != previousMarkerCount || appStatus.getState() != previousState)// || previousState == AppState.Status.FIND_MARKER)
            {
                updateIndicators(currentMarkerCount, currentTargetCount, 0);
            }

        }
        else if (appStatus.getState() == AppState.Status.ALIGNMENT_READY)
        {
            updateIndicators(currentMarkerCount, currentTargetCount, 2);
        }

        previousMarkerCount = currentMarkerCount;
        previousTargetCount = currentTargetCount;
        previousState = appStatus.getState();


    }

    void updateIndicators(int markerCount, int targetCount, int type )
    {
        //type = 0: Marker is active, 1: Target is active, 2: nothing is active
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
                marker_objs[i].GetComponent<Image>().color = Color.blue;

            }
            //active markers are green
            else if (i == markerCount)
            {                
                if (type == 0)
                {
                    marker_objs[i].GetComponent<Image>().color = Color.green;
                    marker_objs[i].AddComponent(typeof(Blink));// as Blink;
                    activeBlinker = marker_objs[i];//.GetComponent<Blink>();

                }
            }
            //not yet active markers are red
            else
            {
                marker_objs[i].GetComponent<Image>().color = Color.red;
            }

        }

        for (int i = 0; i < Constants.NUM_TARGETS; i++)
        {

            //completed targets are blue
            if (i < targetCount)
            {
                target_objs[i].GetComponent<Image>().color = Color.blue;

            }
            //active targets are green
            else if (i == targetCount)
            {                
                if(type == 1)
                {
                    target_objs[i].GetComponent<Image>().color = Color.green;
                    target_objs[i].AddComponent(typeof(Blink));// as Blink;
                    activeBlinker = target_objs[i]; //target_objs[i].GetComponent<Blink>();
                }
            }
            //not yet active targets are red
            else
            {
                target_objs[i].GetComponent<Image>().color = Color.red;
            }
        }
    }

    }

