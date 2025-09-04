using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using DG.Tweening;
public class CameraLock : MonoBehaviour
{
    [SerializeField] Transform originalPos;
    [SerializeField] Transform camLock;
    [SerializeField] float originalFOV;
    [SerializeField] float newFov = 60;
    public bool ConditionToActivate;
    public UnityEvent onLock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ConditionToActivate)
            {
                print("Colidiu e tem condição");
                LockCamera();
            }
            else print("Condition to activate not met");
        }
    }
    void LockCamera()
    {
        Camera camera = Camera.main;
        originalPos = camera.transform;

        StartCoroutine(MoveTo(camera.transform, camLock, 0.5f));
        camera.transform.DORotate(camLock.eulerAngles, 0.5f);
        camera.fieldOfView = newFov;

        HorrorPlayerControllerJuicy.instance.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        onLock?.Invoke();
    }
    public void UnlockCamera()
    {
        Camera camera = Camera.main;
        StartCoroutine(MoveTo(camera.transform, originalPos, 0.5f));
        camera.transform.localRotation = Quaternion.Euler(Vector3.zero);
        camera.fieldOfView = originalFOV;
        HorrorPlayerControllerJuicy.instance.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
