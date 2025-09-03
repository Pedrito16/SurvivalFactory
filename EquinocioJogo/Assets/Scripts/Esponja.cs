using UnityEngine;
using UnityEngine.EventSystems;

public class Esponja : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] bool isHoldingSponje;
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("to ativando porra");
        //canvasGroup.alpha = 0.5f;
        isHoldingSponje = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.alpha = 1;
        isHoldingSponje = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("Clicou");
    }
}
