using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using DG.Tweening;
public class CameraLock : MonoBehaviour
{
    [SerializeField] Transform camLock;
    [SerializeField] float originalFOV;
    [SerializeField] float newFov = 60;
    public UnityEvent onLock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LockCamera();
        }
    }
    void LockCamera()
    {
        Camera camera = Camera.main;

        StartCoroutine(MoveTo(camera.transform, camLock, 0.25f));
        camera.transform.DORotate(camLock.eulerAngles, 0.25f);
        camera.fieldOfView = newFov;

        HorrorPlayerControllerJuicy.canMove = false;
        onLock?.Invoke();
    }
    IEnumerator MoveTo(Transform initial, Transform final, float duration)
    {
        while(duration > 0)
        {
            initial.position = Vector3.Lerp(initial.position, final.position, Time.deltaTime / duration);
            duration -= Time.deltaTime;
            yield return null;
        }
        initial.position = final.position;
    }
}
