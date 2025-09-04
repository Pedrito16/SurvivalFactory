using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TMPro;
using System;
public class WriteText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToWriteInUI;
    public static WriteText instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    public void WriteLine(string text)
    {
        StartCoroutine(Escrever(text));
    }
    public void WriteDialogue(string[] dialogueToWrite, float timeBetweenTexts, Action onDialogueEnd)
    {
        StartCoroutine(WriteArray(dialogueToWrite, timeBetweenTexts, onDialogueEnd));
    }
    IEnumerator WriteArray(string[] dialogueToWrite, float timeBetweenTexts, Action onDialogueEnd = null)
    {
        for (int i = 0; i < dialogueToWrite.Length; i++)
        {
            char[] letters = dialogueToWrite[i].ToCharArray();
            for (int j = 0; j < letters.Length; j++)
            {
                textToWriteInUI.text += letters[j];
                yield return new WaitForSeconds(0.04f);
            }
            yield return new WaitForSeconds(timeBetweenTexts);
            textToWriteInUI.text = "";
        }
        onDialogueEnd?.Invoke();
    }
    IEnumerator Escrever (string text)
    {
        char[] letters = text.ToCharArray();
        for(int i = 0; i < letters.Length; i++)
        {
            textToWriteInUI.text += letters[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2);
        textToWriteInUI.gameObject.SetActive(false);
    }
    
}
