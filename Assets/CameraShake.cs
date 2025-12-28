using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;
    public float dampingSpeed = 1.0f;
    public float rotationMagnitude = 5.0f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomPoint = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = Vector3.Lerp(transform.localPosition, randomPoint, Time.deltaTime * dampingSpeed);

            Quaternion randomRotation = Quaternion.Euler(
                initialRotation.eulerAngles.x + Random.Range(-rotationMagnitude, rotationMagnitude),
                initialRotation.eulerAngles.y + Random.Range(-rotationMagnitude, rotationMagnitude),
                initialRotation.eulerAngles.z + Random.Range(-rotationMagnitude, rotationMagnitude)
            );

            transform.localRotation = Quaternion.Lerp(transform.localRotation, randomRotation, Time.deltaTime * dampingSpeed);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
    }
}


