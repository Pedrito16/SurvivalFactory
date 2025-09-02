using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DishMinigame : MonoBehaviour
{
    [SerializeField] Transform plateHierarchy;
    [SerializeField] List<Transform> plates;
    [SerializeField] Transform platePlace;
    CameraLock camLock;
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
    }
}
