using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    float currentTime;
    float startTime;
    float animationLength = 2.0f;
    float elapsedTime;
    float rotationSpeed = 1.0f;
    float rotationAmount = 45f;
    Vector3 startRotation;
    Vector3 nextRotation;
    // Start is called before the first frame update
    private void OnEnable()
    {
        startTime = Time.time;
        currentTime = Time.time;
        elapsedTime = 0.0f;
        startRotation = this.gameObject.transform.rotation.eulerAngles;

    }

    private void OnDisable()
    {
        setRotation(startRotation);
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (elapsedTime < 2.0f)
        {
            nextRotation = Vector3.up * (rotationAmount * Mathf.Sin(rotationSpeed * elapsedTime * 2 * Mathf.PI / animationLength)) + startRotation;
            setRotation(nextRotation);
        }
        else if (elapsedTime > 2.5f)
        {
            this.gameObject.SetActive(false);
        }
        elapsedTime = currentTime - startTime;
    }

    private void setRotation(Vector3 angles)
    {
        Vector3 angleDelta = angles - this.gameObject.transform.rotation.eulerAngles;
        this.gameObject.transform.Rotate(angleDelta);
    }
}
