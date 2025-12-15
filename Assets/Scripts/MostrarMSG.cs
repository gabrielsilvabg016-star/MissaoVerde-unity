using UnityEngine;
using UnityEngine.EventSystems;

public class MostrarMSG : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerExit(PointerEventData eventData)
    {
        img.SetActive(false);
    }

}
