using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using System.ComponentModel;

public class DishMinigame : MonoBehaviour
{
    [SerializeField] Transform plateHierarchy;
    [Description("Objeto que é pai dos pratos")]

    [SerializeField] List<Transform> plates;

    [SerializeField] Transform platePlace;
    [Description("Lugar onde o prato vai ficar na frente do jogador")]
    [SerializeField] Transform cleanPlatePlace;
    [Description("Lugar onde o prato vai ficar quando for limpo")]

    [SerializeField] public Transform particlePosition;
    [SerializeField] LayerMask plateLayer;
    CameraLock camLock;
    float stackOffset = 0;
    public static DishMinigame instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        camLock = GetComponent<CameraLock>();
        camLock.onLock.AddListener(PassarPrato);

        foreach (Transform child in plateHierarchy)
        {
            plates.Add(child);
        }

        camLock.ConditionToActivate = CheckIfCanDoMinigame();
        plates.Remove(plateHierarchy);
    }
    public bool CheckIfCanDoMinigame()
    {
        return plates.Count > 0;
    }
    void PassarPrato()
    {
        Transform lastPlate = plates[0];
        print(lastPlate.name);
        lastPlate.transform.DOMove(platePlace.position, 0.25f);
        lastPlate.transform.DORotateQuaternion(platePlace.rotation, 0.25f);
    }
    public void DevolverPrato()
    {
        stackOffset += 0.05f;

        Transform lastPlate = plates[0];
        Vector3 cleanPlatePos = cleanPlatePlace.position;
        Vector3 plateNewPos = new Vector3(cleanPlatePos.x, cleanPlatePos.y + stackOffset, cleanPlatePos.z);

        lastPlate.transform.DOMove(plateNewPos, 0.25f);
        lastPlate.transform.DORotateQuaternion(cleanPlatePlace.rotation, 0.25f);
        plates.RemoveAt(0);
        if (plates.Count <= 0)
        {
            camLock.UnlockCamera();
            Destroy(camLock);
            return; 
        }
        Invoke("PassarPrato", 0.5f);
    }
    public void PArticleSpawn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, plateLayer))
        {
            particlePosition.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.1f);
            particlePosition.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }

        //codigo velho:
        //float cleanPlatePosZ = Vector3.Distance(Camera.main.transform.position, cleanPlatePlace.position);
        //Vector3 mousePos = Input.mousePosition;
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //particlePosition.position = worldPos;
    }
}
