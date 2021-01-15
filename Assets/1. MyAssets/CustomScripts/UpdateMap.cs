using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateMap : MonoBehaviour
{
    GameObject[] target_objs;
    AppState appStatus;
    int previousTargetCount;
    int currentTargetCount;
    
    void Awake()
    {
        target_objs = new GameObject[Constants.NUM_TARGETS];
        for (int i = 0; i < Constants.NUM_TARGETS; i++)
        {
            target_objs[i] = this.gameObject.transform.Find(Constants.BRACKET_TARGETS[i]).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        appStatus = AppState.instance;
        previousTargetCount = appStatus.getTargetCount();
        updateTargets(previousTargetCount);


    }
    // Update is called once per frame
    void Update()
    {
        currentTargetCount = appStatus.getTargetCount();

        if (currentTargetCount != previousTargetCount)
        {
            updateTargets(currentTargetCount);
        }

        previousTargetCount = currentTargetCount;

    }

    void updateTargets(int targetCount)
    {
        for (int i=0; i < Constants.NUM_TARGETS; i++)
        {
            Blink blinkScript = target_objs[i].GetComponent<Blink>();
            if (blinkScript != null)
            {
                Destroy(blinkScript);
            }

            //completed targets are blue
            if (i < targetCount)
            {
                target_objs[i].GetComponent<Image>().color = Color.blue;

            }
            //active targets are green
            else if (i == targetCount)
            {
                //will this fuck with the memory tho?
                target_objs[i].GetComponent<Image>().color = Color.green;
                target_objs[i].AddComponent(typeof(Blink));// as Blink;
            }
            //not yet active targets are red
            else
            {
                target_objs[i].GetComponent<Image>().color = Color.red;
            }

        }
    }

    }