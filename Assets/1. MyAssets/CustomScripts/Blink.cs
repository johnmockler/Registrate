using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BlinkGameObject", 0, 0.33f);
        
    }

    // switch between being visible and invisible
    void BlinkGameObject()
    {
        active = !active;
        this.gameObject.SetActive(active);
    }
}
