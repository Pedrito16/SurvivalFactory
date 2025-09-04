using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class NextScene : MonoBehaviour, IInteractable
{
    [SerializeField] private Image fade;

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneRoutine(sceneName));
    }

    private IEnumerator ChangeSceneRoutine(string sceneName)
    {
        if(fade == null)
        {
            SceneManager.LoadScene(sceneName);
            yield break;
        }
        fade.DOFade(1, 2); // Faz o fade out em 2 segundos
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void FadeIn()
    {
        fade.DOFade(0, 2);
    }

    public void FadeOut()
    {
        fade.DOFade(1, 2);
    }

    public void Interact()
    {
        StartCoroutine(ChangeSceneRoutine("DriveScene"));
    }
}
