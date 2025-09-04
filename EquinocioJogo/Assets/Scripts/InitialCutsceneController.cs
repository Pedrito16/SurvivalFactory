using UnityEngine;
using UnityEngine.Playables;

public class InitialCutsceneController : MonoBehaviour
{
    [SerializeField] Quaternion initialRotation;
    [SerializeField] PlayableDirector director;
    void Start()
    {
        initialRotation = Camera.main.transform.rotation;
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
        HorrorPlayerControllerJuicy.instance.canMove = false;
    }
    void Update()
    {
        
    }
    public void ReactivePlayer()
    {
        HorrorPlayerControllerJuicy.instance.canMove = true;
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
        Camera.main.transform.position = Vector3.zero;
        Destroy(director);
    }
}
