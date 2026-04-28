using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class botaoSaida : MonoBehaviour
{
        public GameObject canvas;
        public Button button;
        public GameObject painel;
        public GameObject buttonDefault;
    void Start()
    {
        button.onClick.AddListener(EsconderImagem);
    }

    public void EsconderImagem()
    {
        if(canvas.activeSelf == true)
        {
            for(int x = 0; x < painel.transform.childCount; x++)
            {
                painel.transform.GetChild(x).gameObject.SetActive(true);
            }
            EventSystem.current.SetSelectedGameObject(buttonDefault);
            canvas.SetActive(false);
            Debug.Log("Canvas foi desativado");
        }
    }
}
