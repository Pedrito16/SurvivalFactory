using UnityEngine;

public class Plate : MonoBehaviour
{
    //Material material;
    [SerializeField] MeshRenderer sujeiraRenderer;
    public float timeToWash = 5;
    bool oneTime = false;
    DishMinigame minigame;
    private void Start()
    { 
       //material = GetComponent<Renderer>().material;
        minigame = DishMinigame.instance;
    }
    private void OnMouseDown()
    {
        minigame.particlePosition.GetComponent<ParticleSystem>().Play();   
    }
    private void OnMouseDrag()
    {
        print("dragando");
        if (!minigame.isOnMinigame) return;
        Color alpha = sujeiraRenderer.material.color;
        alpha.a = timeToWash / 5;
        sujeiraRenderer.material.color = alpha;

        timeToWash -= Time.deltaTime;

        //material.SetFloat("_TotalTime", timeToWash / 5); //para gradiente

        minigame.PArticleSpawn();
        if(timeToWash <= 0 && !oneTime)
        {
            minigame.DevolverPrato();
            oneTime = true;
        }
    }
}
