using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShak : MonoBehaviour
{
    public Transform cameraTransform;
    public float duration = 0.5f;
    public float magnitude = 0.2f;

    private Vector3 originalPos;

    public void ShakeCamera()
    {
        if (cameraTransform != null)
        {
            originalPos = cameraTransform.localPosition;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraTransform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPos;
    }
}
