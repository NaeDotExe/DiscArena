using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Debug.Log("SHAKE");

        Vector3 startPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(startPos.x + x, startPos.y + y, startPos.z);

            Debug.Log(transform.localPosition);

            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = startPos;
    }
}
