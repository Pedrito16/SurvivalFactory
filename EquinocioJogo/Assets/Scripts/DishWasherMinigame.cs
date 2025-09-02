using UnityEngine;

public class DishWasherMinigame : MonoBehaviour
{
    [SerializeField] Transform camLock;
    [SerializeField] float originalFOV;
    [SerializeField] float newFov = 60;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LockCamera();
        }
    }
    void LockCamera()
    {
        Camera.main.transform.position = camLock.position;
        Camera.main.fieldOfView = newFov;
        HorrorPlayerControllerJuicy.canMove = false;
    }
}
