using UnityEngine;
using TMPro;
using System.Collections;
public class QuestController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questText;
    public static QuestController instance;
    Quest quest;
    Transform demonWalkPos;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        questText.text = "";
    }
    public void SetNewQuest(Quest quest, Transform demonWalkPos)
    {
        this.quest = quest;
        this.demonWalkPos = demonWalkPos;
        if (quest.dialogueText.Length > 0)
            WriteText.instance.WriteDialogue(quest.dialogueText, 3, SetOthers);
    }
    public void SetOthers()
    {
        if (demonWalkPos != null)
            DemonWalk.instance.SetLocation(demonWalkPos);

        if (quest.questText != "")
            questText.text = quest.questText;
    }
}
