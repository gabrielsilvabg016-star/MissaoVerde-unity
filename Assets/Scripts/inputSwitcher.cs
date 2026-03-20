using UnityEngine;
using UnityEngine.EventSystems;

public class InputSwitcher : MonoBehaviour
{
    public GameObject primeiroBotao;

    private bool Mouse = true;

    void Update()
    {
        DetectMouse();
        DetectTeclado();
    }

    void DetectMouse()
    {
        //movimento do mouse
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            if(!Mouse)
            {
                Mouse = true;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(!Mouse)
            {
                Mouse = true;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
    void DetectTeclado()
    {
        //pressionamento de uma tecla
        if(Input.anyKeyDown)
        {
            if(Input.GetMouseButtonDown(0))
                return;

            if(Mouse)
            {
                Mouse = false;

                if(EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(primeiroBotao);
                }
            }
        }

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if(Mouse)
            {
                Mouse = false;

                if(EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(primeiroBotao);
                }
            }
        }
    }
}
