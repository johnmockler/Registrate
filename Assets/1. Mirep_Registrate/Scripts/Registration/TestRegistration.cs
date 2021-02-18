using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TestRegistration : MonoBehaviour
{

    private Vector3[] testPoints;
    private System.Random rand;
    private Alignment3D align;
    private TextMesh statusmsg;

    [SerializeField]
    public GameObject status;

    // Start is called before the first frame update
    void Start()
    {
        statusmsg = status.GetComponent<TextMesh>();
        rand = new System.Random();
        //testPoints = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0.215f, 0, 0), new Vector3(0.215f, 0.279f, 0), new Vector3(0, 0.279f, 0) };
        //testPoints = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, -0.215f, 0), new Vector3(0.279f, -0.215f, 0), new Vector3(0.279f, 0 , 0) };
        testPoints = new Vector3[] { new Vector3(1, 1, 0), new Vector3(-1, 1, 0), new Vector3(-1, -1, 0), new Vector3(1, -1, 0), new Vector3(1, -1, 0), new Vector3(1, -1, 0) };


        for (int i = 0; i < testPoints.Length; i++)
        {
            //shift points by 1
            testPoints[i].x = testPoints[i].x + 2.0f+ 0.15f * map((float)rand.NextDouble());// + 2.0f;
            testPoints[i].y = testPoints[i].y + 0.15f * map((float)rand.NextDouble());// + 2.0f; 
            testPoints[i].z = testPoints[i].z + 0.15f * map((float)rand.NextDouble());// + 1.0f;

        }
        align = new Alignment3D(testPoints);
        statusmsg.text = align.computeRegistration().ToString();


    }

    //map a double number from 0 to 1 to -1 to 1
    float map(float num)
    {
        return num * 2.0f - 1.0f;

    }
}
