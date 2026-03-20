using UnityEngine;
using UnityEngine.EventSystems;

public class MostrarMSG : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public GameObject img;

    void Start()
    {
        if(img != null)
        {
            img.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(img.activeSelf == false)
        {
            img.SetActive(true);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(img.activeSelf == false)
        {
            img.SetActive(true);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        img.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.SetActive(false);
    }

}
