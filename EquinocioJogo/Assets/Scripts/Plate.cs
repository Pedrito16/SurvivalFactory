using UnityEngine;

public class Plate : MonoBehaviour
{
    public float timeToWash = 5;
    bool oneTime = false;
    private void OnMouseDrag()
    {
        print("to lavando ui ui ui");
        timeToWash -= Time.deltaTime;

        DishMinigame.instance.PArticleSpawn();
        if(timeToWash <= 0 && !oneTime)
        {
            DishMinigame.instance.DevolverPrato();
            oneTime = true;
        }
    }
}
