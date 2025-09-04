using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public enum DemonState
{
    Walking,
    Idle
}
public class DemonWalk : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform location;

    [Header("Debug")]
    [SerializeField] Animator anim;
    [SerializeField] DemonState state;
    [SerializeField] bool isOnPosition;

    public static DemonWalk instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
    }
    private void Update()
    {
        if(location == null) return;
        float distance = Vector3.Distance(transform.position, location.position);

        anim.SetFloat("Movement", distance);

        if (state == DemonState.Walking)
        {
            agent.SetDestination(location.position);
            if(transform.position == location.position && !isOnPosition)
            {
                SwitchState(DemonState.Idle);
                Rotate();
                location = null;
                isOnPosition = true;
            }
        }
    }
    void Rotate()
    {
        print("Rotating");  
        transform.DORotateQuaternion(location.rotation, 0.5f);
    }
    public void SetLocation(Transform location)
    {
        this.location = location;
        SwitchState(DemonState.Walking);
    }
    void SwitchState(DemonState nextState)
    {
        switch (nextState)
        {
            case DemonState.Walking:
                state = DemonState.Walking;
                agent.isStopped = false;
                break;
            case DemonState.Idle:
                state = DemonState.Idle;
                agent.isStopped = true;
                break;
        }
    }
}
