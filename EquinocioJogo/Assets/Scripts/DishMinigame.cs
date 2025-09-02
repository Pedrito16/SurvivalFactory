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

    [SerializeField] Plate activePlate;
    CameraLock camLock;
    float stackOffset = 0;
    
    bool canClick = false;
    void Start()
    {
        camLock = GetComponent<CameraLock>();
        camLock.onLock.AddListener(PassarPrato);
        plates = plateHierarchy.GetComponentsInChildren<Transform>().ToList();
        plates.Remove(plateHierarchy);
    }
    void PassarPrato()
    {
        Transform lastPlate = plates[0];
        print(lastPlate.name);
        lastPlate.transform.DOMove(platePlace.position, 0.25f);
        lastPlate.transform.DORotateQuaternion(platePlace.rotation, 0.25f);
        activePlate = lastPlate.GetComponent<Plate>();
    }
    private void Update()
    {
        if(canClick && Input.GetMouseButtonDown(0))
        {
           DevolverPrato();
        }
    }
    void DevolverPrato()
    {
        stackOffset += 0.05f;

        Transform lastPlate = plates[0];
        Vector3 cleanPlatePos = cleanPlatePlace.position;
        Vector3 plateNewPos = new Vector3(cleanPlatePos.x, cleanPlatePos.y + stackOffset, cleanPlatePos.z);

        lastPlate.transform.DOMove(plateNewPos, 0.25f);
        lastPlate.transform.DORotateQuaternion(cleanPlatePlace.rotation, 0.25f).OnComplete(() => canClick = false);
        plates.RemoveAt(0);
        Invoke("PassarPrato", 0.5f);
    }
}
