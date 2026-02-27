using UnityEngine;
using UnityEngine.UI;


public class botaoSaida : MonoBehaviour
{
        public GameObject canvas;
        public Button button;
        public GameObject painel;
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
            canvas.SetActive(false);
            Debug.Log("Canvas foi desativado");
        }
    }
}
