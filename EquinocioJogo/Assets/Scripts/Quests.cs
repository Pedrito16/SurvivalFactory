using UnityEngine;

public class Quests : MonoBehaviour
{
    [SerializeField] Quest[] mainQuests;
    [SerializeField] Transform[] demonWalkLocations;
    [SerializeField] int currentQuestIndex = -1;
    public static Quests instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PassQuest();
    }
    void Update()
    {
        
    }
    public void PassQuest()
    {
        if (currentQuestIndex >= mainQuests.Length) return;
        currentQuestIndex++;
        QuestController.instance.SetNewQuest(mainQuests[currentQuestIndex], demonWalkLocations[currentQuestIndex]);
    }
}
