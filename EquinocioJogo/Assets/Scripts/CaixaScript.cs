using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CaixaScript : MonoBehaviour, IInteractable
{
    [SerializeField] float demonYOffset = 0.1f;
    [SerializeField] Transform rightFlap;
    [SerializeField] Transform leftFlap;
    [SerializeField] Transform demon;
    NavMeshAgent agent;
    Vector3 demonPos;
    void Start()
    {
        agent = demon.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        demonPos = new Vector3(transform.position.x - 0.5f, 1 * -5, transform.position.z);
        demon.position = demonPos;
        rightFlap.transform.rotation = Quaternion.Euler(Vector3.right * -90);
        leftFlap.transform.rotation = Quaternion.Euler(Vector3.right * -90);
    }
    public void Interact()
    {
        StartCoroutine(StartMiniCutscene());
    }
    IEnumerator StartMiniCutscene()
    {
        demonPos = new Vector3(transform.position.x, 1 * demonYOffset, transform.position.z);
        demon.transform.DOMove(demonPos, 1f);
        rightFlap.DORotateQuaternion(Quaternion.Euler(Vector3.right * 38), 1);
        leftFlap.DORotateQuaternion(Quaternion.Euler(Vector3.right * -228), 1);
        yield return new WaitForSeconds(3f);
        WhenBoxOpen();
        agent.enabled = true;
    }
    void WhenBoxOpen()
    {
        Quests.instance.PassQuest();
    }
    void Update()
    {
        
    }
}
