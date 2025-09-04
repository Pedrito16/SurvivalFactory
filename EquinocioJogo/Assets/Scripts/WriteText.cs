using UnityEngine;
using System.Collections;
using TMPro;
public class WriteText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToWriteInUI;
    void Start()
    {
        WriteLine("HAmburguer comida comidinha delicinha");
    }
    public void WriteLine(string text)
    {
        StartCoroutine(Escrever(text));
    }
    public void WriteDialogue(string[] dialogueToWrite, float timeBetweenTexts)
    {
        StartCoroutine(WriteArray(dialogueToWrite, timeBetweenTexts));
    }
    IEnumerator WriteArray(string[] dialogueToWrite, float timeBetweenTexts)
    {
        for (int i = 0; i < dialogueToWrite.Length; i++)
        {
            char[] letters = dialogueToWrite[i].ToCharArray();
            for (int j = 0; j < letters.Length; j++)
            {
                textToWriteInUI.text += letters[j];
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(timeBetweenTexts);
        }
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
