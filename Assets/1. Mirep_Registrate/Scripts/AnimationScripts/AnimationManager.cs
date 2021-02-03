using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    public GameObject translateZ;

    [SerializeField]
    public GameObject translateXY;

    [SerializeField]
    public GameObject rotateY;
    // Start is called before the first frame update
    void Start()
    {
        translateZ.SetActive(false);
        translateXY.SetActive(false);
        rotateY.SetActive(false);
    }

    // Update is called once per frame
    public void TranslateZ()
    {
        translateZ.SetActive(true);
        translateXY.SetActive(false);
        rotateY.SetActive(false);

    }
    public void TranslateXY()
    {
        translateXY.SetActive(true);
        rotateY.SetActive(false);
        translateZ.SetActive(false);


    }
    public void RotateY()
    {
        rotateY.SetActive(true);
        translateXY.SetActive(false);
        translateZ.SetActive(false);

    }
}
