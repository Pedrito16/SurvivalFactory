using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class DishMinigame : MonoBehaviour
{
    [SerializeField] Transform plateHierarchy;
    [SerializeField] List<Transform> plates;
    [SerializeField] Transform platePlace;
    [SerializeField] Transform cleanPlatePlace;
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
        canClick = true;
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
