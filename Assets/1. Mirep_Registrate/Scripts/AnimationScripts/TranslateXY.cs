using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateXY : MonoBehaviour
{
    float currentTime;
    float startTime;
    float animationLength = 1.0f;
    float elapsedTime;
    float translationAmount = 0.1f;
    float translationSpeed = 0.5f;
    Vector3 startPosition;
    // Start is called before the first frame update
    private void OnEnable()
    {
        startTime = Time.time;
        currentTime = Time.time;
        elapsedTime = 0.0f;
        startPosition = this.gameObject.transform.position;

    }

    private void OnDisable()
    {
        this.gameObject.transform.position = startPosition;
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (elapsedTime < animationLength)
        {
            this.gameObject.transform.position = this.gameObject.transform.right * (translationAmount * Mathf.Sin(translationSpeed * elapsedTime * 2 * Mathf.PI / animationLength)) + startPosition;
        }
        else if(elapsedTime < 2.0f*animationLength)
        {
            this.gameObject.transform.position = this.gameObject.transform.up * (translationAmount * Mathf.Sin(translationSpeed * elapsedTime * 2 * Mathf.PI / animationLength)) + startPosition;

        }
        else if (elapsedTime > 2.0f*animationLength + 0.5f)
        {
            this.gameObject.SetActive(false);
        }
        elapsedTime = currentTime - startTime;
    }
}
