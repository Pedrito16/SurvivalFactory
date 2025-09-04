using System.Collections;
using UnityEngine;

public class TV : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject video;
    [SerializeField] AudioSource source;
    [SerializeField] GameObject box;
    public void Interact()
    {
        print("Interagiu com a TV");
        video.SetActive(true);
        StartCoroutine(activeWithDelay());
    }
    IEnumerator activeWithDelay()
    {
        yield return new WaitForSeconds(1f);
        source.Play();
        Quests.instance.PassQuest();
        box.SetActive(false);
    }
    void Start()
    {
        video.SetActive(false);
        source.Stop(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
